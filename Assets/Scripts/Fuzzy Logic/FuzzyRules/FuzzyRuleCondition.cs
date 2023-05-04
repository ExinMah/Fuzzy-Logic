using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fuzzy_Logic.MembershipFunctions;

namespace Fuzzy_Logic.FuzzyRules
{
    public class FuzzyRuleCondition : FuzzyRuleClause
    {
        public FuzzyRuleCondition(LinguisticVariable variable, IFuzzyRuleToken @operator, IMembershipFunction function)
            : base(variable, @operator, function)
        {
        }
        public FuzzyRuleConditionConjunction Conjunction { get; set; }
    }
}