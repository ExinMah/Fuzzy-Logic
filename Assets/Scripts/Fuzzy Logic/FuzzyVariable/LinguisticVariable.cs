using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fuzzy_Logic.FuzzyRules;
using Fuzzy_Logic.MembershipFunctions;

namespace Fuzzy_Logic
{
    public class LinguisticVariable : FuzzyRuleToken
    {
        // Constructors
        public LinguisticVariable(string name) : base(name, FuzzyRuleTokenType.Variable)
        {
            _membershipFunctions = new MembershipFunction();
        }
        
        // Private Properties
        private MembershipFunction _membershipFunctions;
        private Double _inputValue = 0;
        
        // Public Properties
        public MembershipFunction MembershipFunctions
        {
            get { return _membershipFunctions; }
            set { _membershipFunctions = value; }
        }

        public double InputValue
        {
            get { return _inputValue; }
            set { _inputValue = value; }
        }
    }
}