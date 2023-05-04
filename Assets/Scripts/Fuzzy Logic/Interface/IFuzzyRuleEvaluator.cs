using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fuzzy_Logic.FuzzyRules;

namespace Fuzzy_Logic.FuzzyRules
{
    public interface IFuzzyRuleEvaluator
    {
        Double Evaluate(List<FuzzyRuleCondition> ruleConditions);
    }
}