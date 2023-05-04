using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace Fuzzy_Logic.FuzzyRules
{
    // The if-part of the rule
    public class Premise : List<FuzzyRuleCondition>
    {
        public Premise(FuzzyRuleCondition condition)
        {
            this.Add(condition);
        }

        public Premise(IEnumerable<FuzzyRuleCondition> conditions)
        {
            foreach (var condition in conditions)
            {
                Add(condition);
            }
        }
    }
}