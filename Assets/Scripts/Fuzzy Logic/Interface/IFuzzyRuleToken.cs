using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fuzzy_Logic.FuzzyRules
{
    public interface IFuzzyRuleToken
    {
        String Name { get; set; }
        FuzzyRuleTokenType Type { get; set; }
    }
}