using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMOR_2.Function
{
    class MyFunction:IFunction
    {
        /// <summary>
        /// 8th variant. Kuznietsov.
        /// </summary>
        /// <param name="x">vector of variables</param>
        /// <returns>value of function</returns>
        public double solve(List<double> x)
        {
            return 3 * x[0] * x[0] * x[0] - x[0] + x[1] * x[1] * x[1] - 3 * x[1] * x[1] - 1;
        }
    }
}
