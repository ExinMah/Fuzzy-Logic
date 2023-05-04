using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fuzzy_Logic.MembershipFunctions;

namespace Fuzzy_Logic
{
    public interface IDefuzzification
    {
        Double Defuzzify(List<IMembershipFunction> functions);
    }
}