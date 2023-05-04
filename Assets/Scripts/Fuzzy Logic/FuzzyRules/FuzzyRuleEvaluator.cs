using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuzzy_Logic.FuzzyRules
{
    public class FuzzyRuleEvaluator : IFuzzyRuleEvaluator
    {
        public double Evaluate(List<FuzzyRuleCondition> ruleConditions)
        {
            Double value = 0;
            Boolean isFirstCondition = true;

            foreach (var condition in ruleConditions)
            {
                var conditionValue = condition.MembershipFunction.Fuzzify(condition.Variable.InputValue);

                if (condition.Operator.Type == FuzzyRuleTokenType.Not)
                {
                    conditionValue = 1 - conditionValue;
                }

                if (isFirstCondition)
                {
                    value = conditionValue;
                    isFirstCondition = false;
                }
                else
                {
                    {
                        switch (condition.Conjunction.Conjunction.Type)
                        {
                            case FuzzyRuleTokenType.And:
                                if (conditionValue < value) value = conditionValue;
                                break;
                            case FuzzyRuleTokenType.Or:
                                if (conditionValue > value) value = conditionValue;
                                break;
                        }
                    }
                }
            }

            return value;
        }
    }
}