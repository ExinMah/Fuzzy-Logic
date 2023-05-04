using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace Fuzzy_Logic.FuzzyRules
{
    public class FuzzyRule
    {
        // Public Properties
        public Premise Premise { get; set; }
        public Conclusion Conclusion { get; set; }
        
        // Public Methods
        // Determines if the current list of tokens is a valid rule
        public virtual Boolean IsValid()
        {
            var premiseIsNotNull = null != Premise;
            var conclustionIsNotNull = null != Conclusion;

            if (premiseIsNotNull && conclustionIsNotNull)
            {
                var premiseHasMinOneCondition = 0 < Premise.Count;
                var premiseIsValid = Premise.All(c =>
                    null != c && 
                    null != c.Variable && 
                    null != c.Operator && 
                    null != c.MembershipFunction);
                var conclusionIsValid = null != Conclusion.Variable && 
                                        null != Conclusion.Operator && 
                                        null != Conclusion.MembershipFunction;
                
                return premiseHasMinOneCondition && premiseIsValid && conclusionIsValid;
            }
            else
            {
                return false;
            }
        }
    }
}