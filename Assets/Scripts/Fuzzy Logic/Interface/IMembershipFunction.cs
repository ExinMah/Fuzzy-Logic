using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fuzzy_Logic.FuzzyRules;

namespace Fuzzy_Logic.MembershipFunctions
{
    public interface IMembershipFunction : IFuzzyRuleToken
    {
        Double Fuzzify(Double inputValue);
        Double PremiseModifier { get; set; }
        Double Modification { get; set; }
        Double Min();
        Double Max();
    }
}