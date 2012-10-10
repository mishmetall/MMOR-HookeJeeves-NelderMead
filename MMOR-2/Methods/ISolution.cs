using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMOR_2.Function;

namespace MMOR_2.Methods
{
    interface ISolution
    {
        Vector solve(IFunction f, double precision);
    }
}
