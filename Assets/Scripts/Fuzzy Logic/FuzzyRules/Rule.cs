using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Fuzzy_Logic.FuzzyRules
{
    public static class Rule
    {
        public static FuzzyRule If(List<FuzzyRuleCondition> conditions)
        {
            var rule = new FuzzyRule();
            rule.Premise = new Premise(conditions);
            return rule;
        }

        public static FuzzyRule If(FuzzyRuleCondition condition)
        {
            var rule = new FuzzyRule();
            rule.Premise = new Premise(condition);
            return rule;
        }
    }
}