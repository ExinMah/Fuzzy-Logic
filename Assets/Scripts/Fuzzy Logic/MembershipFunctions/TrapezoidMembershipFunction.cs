using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Fuzzy_Logic.FuzzyRules;
using Fuzzy_Logic.MembershipFunctions;

namespace Fuzzy_Logic.MembershipFunctions
{
    public class TrapezoidMembershipFunction : BaseMembershipFunction
    {
        private Double _a = 0;
        private Double _b = 0;
        private Double _c = 0;
        private Double _d = 0;

        /// <param name="name">The name for the membership function.</param>
        /// <param name="a">The left most x value at 0.</param>
        /// <param name="b">The mid left x value at 1.</param>
        /// <param name="c">The mid right x value at 1.</param>
        /// <param name="d">The right most x value at 1.</param>
        public TrapezoidMembershipFunction(String name, Double a, Double b, Double c, Double d)
            : base(name)
        {
            _a = a;
            _b = b;
            _c = c;
            _d = d;
        }
        
        public Double A { get { return _a; } }
        public Double B { get { return _b; } }
        public Double C { get { return _c; } }
        public Double D { get { return _d; } }

        public override Double Fuzzify(Double inputValue)
        {
            if (_a <= inputValue && inputValue < _b)
                return (inputValue - _a) / (_b - _a);
            else if (_b <= inputValue && inputValue <= _c)
                return 1;
            else if (_c < inputValue && inputValue <= _d)
                return (_d - inputValue) / (_d - _c);
            else
                return 0;
        }

        public override Double Min() { return _a; }
        public override Double Max() { return _d; }
    }
}