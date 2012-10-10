using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMOR_2.Methods
{
    class Interval
    {
        public double LeftBound { get; set; }
        public double RightBound { get; set; }

        public Interval(double from, double to)
        {
            LeftBound = from;
            RightBound = to;
        }
    }
}
