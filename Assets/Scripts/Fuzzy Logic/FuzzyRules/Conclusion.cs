using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Fuzzy_Logic.MembershipFunctions;

namespace Fuzzy_Logic.FuzzyRules
{
    // The then-part of the rule
    public class Conclusion : FuzzyRuleClause
    {
        public Conclusion(LinguisticVariable variable, IFuzzyRuleToken @operator, IMembershipFunction function) : base(variable, @operator, function)
        {
            
        }
    }
}