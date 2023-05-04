using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;
using Fuzzy_Logic.FuzzyRules;
using Fuzzy_Logic.Constants;
using Fuzzy_Logic.MembershipFunctions;

namespace Fuzzy_Logic
{
    public class FuzzyInferenceEngine : IFuzzyInferenceEngine
    {
        public FuzzyInferenceEngine(IDefuzzification defuzzification) : this(defuzzification, new FuzzyRuleEvaluator()) {}

        public FuzzyInferenceEngine(IDefuzzification defuzzification, IFuzzyRuleEvaluator fuzzyRuleEvaluator)
        {
            _fuzzyRuleEvaluator = fuzzyRuleEvaluator;
            _defuzzification = defuzzification;
        }

        protected FuzzyRuleCollection _rules;
        protected IFuzzyRuleEvaluator _fuzzyRuleEvaluator;
        protected IDefuzzification _defuzzification;

        protected virtual void SetVariableInputValues(Object inputValues)
        {
            var conditionVariables = _rules.SelectMany(r => r.Premise.Select(p => p.Variable));

            var inputVals = inputValues.PropertyValues();

            foreach (var variable in conditionVariables)
            {
                if (false == inputVals.Any(kv => kv.Key.ToLower() == variable.Name.ToLower() && (kv.Value is Int32 || kv.Value is Double || kv.Value is Decimal)))
                {
                    throw new ArgumentException(String.Format(ErrorMessages.InvalidInputValue, variable.Name));
                }
                else
                {
                    var inputValue = Convert.ToDouble(inputVals.First(kv => kv.Key.ToLower() == variable.Name.ToLower()).Value);
                    variable.InputValue = inputValue;
                }
            }
        }
        
        public Double Defuzzify(Object inputValues)
        {
            if (_rules.Any(r => false == r.IsValid()))
                throw new Exception(ErrorMessages.InvalidRules);

            //reset membership functions
            _rules.ForEach(r => r.Conclusion.MembershipFunction.PremiseModifier = 0);

            SetVariableInputValues(inputValues);

            var conclustionMembershipFunctions = _rules.Select(r => r.Conclusion.MembershipFunction).ToList();

            foreach (FuzzyRule fuzzyRule in _rules)
            {
                var premiseValue = _fuzzyRuleEvaluator.Evaluate(fuzzyRule.Premise);

                var ruleConclusionVar = fuzzyRule.Conclusion.Variable;
                var membershipFunction = ruleConclusionVar.MembershipFunctions.First(mf => mf.Name == fuzzyRule.Conclusion.MembershipFunction.Name);

                membershipFunction.PremiseModifier = premiseValue;
            }

            return _defuzzification.Defuzzify(conclustionMembershipFunctions);
        }

        public FuzzyRuleCollection Rules
        {
            get
            {
                _rules = _rules ?? new FuzzyRuleCollection();
                return _rules;
            }
        }
        
        public IDefuzzification Deffuzification
        {
            get { return _defuzzification; }
        }
    }
}