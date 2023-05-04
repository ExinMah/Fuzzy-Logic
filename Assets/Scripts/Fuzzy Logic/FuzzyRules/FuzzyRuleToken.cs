using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fuzzy_Logic.FuzzyRules
{
    public class FuzzyRuleToken : IFuzzyRuleToken
    {
        public FuzzyRuleToken() {}

        public FuzzyRuleToken(String value, FuzzyRuleTokenType type)
        {
            Name = value;
            Type = type;
        }
        
        public String Name { get; set; }
        public FuzzyRuleTokenType Type { get; set; }
    }
}