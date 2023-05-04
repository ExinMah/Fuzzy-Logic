using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fuzzy_Logic.FuzzyRules;

namespace Fuzzy_Logic.MembershipFunctions
{
    public abstract class BaseMembershipFunction : FuzzyRuleToken, IMembershipFunction
    {
        public BaseMembershipFunction(String name) : base(name, FuzzyRuleTokenType.Function)
        {
            
        }
        
        // Private Properties
        private Double _premiseModifier = 0;
        
        // Public Properties
        public Double PremiseModifier
        {
            get { return _premiseModifier; }
            set
            {
                if (value > _premiseModifier) _premiseModifier = value;
            }
        }

        public Double Modification
        {
            get { return PremiseModifier; }
            set { PremiseModifier = value; }
        }
        
        // Abstract Methods
        public abstract Double Fuzzify(Double inputValue);
        public abstract Double Min();
        public abstract Double Max();
    }
}