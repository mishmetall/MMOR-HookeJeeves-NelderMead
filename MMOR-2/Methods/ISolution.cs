using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMOR_2.Function;

namespace MMOR_2.Methods
{
    interface ISolution
    {
        public List<double> solve(IFunction f, Interval interval, double precision);
    }
}
