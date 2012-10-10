using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMOR_2.Function;

namespace MMOR_2.Methods
{
    class HookeJeeves:ISolution
    {
        private Vector z = new Vector(2);
        private Vector y = new Vector(2);
        private Vector x = new Vector(2);
        private double step = 0.01;
        private double alpha = 2;

        public HookeJeeves(Vector startVector)
        {
            x = startVector;
            y = (Vector)x.Clone();
        }

        public HookeJeeves(Vector startVector, double step)
            : this(startVector)
        {
           this.step = step;
        }

        public HookeJeeves(Vector startVector, double step, double alpha)
            : this(startVector,step)
        {
            if (alpha > 0)
                this.alpha = alpha;
        }

        public Vector solve(IFunction f, double precision)
        {
            step = (precision < 1) ? precision * 2 : 1;

            int k=1;
            int j=k-1;
            do
            {
                do
                {
                    do
                    {
                        // Step 1
                        j++;
                        for (int i = 0; i < z.Size; i++)
                            z[i] = y[i] + (double)step;
                        if (f.value(z) < f.value(y))
                        {
                            y = (Vector)z.Clone();
                        }
                        else
                        {
                            for (int i = 0; i < z.Size; i++)
                                z[i] = y[i] - step;
                            if (f.value(z) < f.value(y))
                            {
                                y = (Vector)z.Clone();
                            }
                        }
                        // Step 2
                    } while (j < z.Size);
                    // Step 3
                    Vector xK = (Vector)x.Clone();
                    x = (Vector)y.Clone();
                    y = x + alpha * (x - xK);
                    k++;
                    j = 1;
                    if (Double.IsInfinity(f.value(x)))
                    {
                        for (int i = 0; i < x.Size; i++)
                        { 
                            x[i] = (x[i]<0) ? Double.NegativeInfinity : (x[i]==0) ? 0 : Double.PositiveInfinity;
                        }
                        return x;
                    }
                } while (f.value(y) < f.value(x));
                // Step 4
                step /= 2;
                y = (Vector)x.Clone();
                k++;
                j = 1;
            } while (step > precision);

            return x;
        }
    }
}
