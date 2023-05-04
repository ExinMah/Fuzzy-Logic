using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace Fuzzy_Logic.MembershipFunctions
{
    // A Collection of membership functions
    public class MembershipFunction : Collection<IMembershipFunction>
    {
        public IMembershipFunction AddTrapezoid(String name, Double a, Double b, Double c, Double d)
        {
            var membershipFunction = new TrapezoidMembershipFunction(name, a, b, c, d);
            this.Add(membershipFunction);
            return membershipFunction;
        }
    }
}