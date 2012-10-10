using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMOR_2.Function;

namespace MMOR_2.Methods
{
    class NelderMead:ISolution
    {
        private Vector x = new Vector(2);

        public NelderMead(Vector startVector)
        {
            x = startVector;
        }

        public Vector solve(IFunction f, double precision)
        {
            Simplex smpl = Simplex.getRegularSimplex(x);

            double alpha=1.0;
            double beta=0.5;
            double gamma = 2.0;
            bool flag = false;

            double f_h, f_g, f_l, f_r, f_e, f_s, tempD;
            Vector  x_h = new Vector(x.Size), 
                    x_g = new Vector(x.Size), 
                    x_l = new Vector(x.Size), 
                    x_r = new Vector(x.Size),
                    x_e = new Vector(x.Size), 
                    x_s = new Vector(x.Size), 
                    x_c = new Vector(x.Size),
                    tempV = new Vector(x.Size);

            double[] fValues = new double[smpl.Size];

            for (int i = 0; i < smpl.Size; i++)
                fValues[i] = f.value(smpl[i]); // Вычисление значений функции на начальном симплексе

            while (!QuitCase(smpl,precision))	// Проверка на условие выхода
            {
                // Шаг 1. Сортировка 
                Sort(smpl, fValues);
                x_h = smpl[smpl.Size - 1]; f_h = fValues[smpl.Size - 1];
                x_g = smpl[smpl.Size - 2]; f_g = fValues[smpl.Size - 2];
                x_l = smpl[0]; f_l = fValues[0];

                if (Double.IsInfinity(f_l))
                {
                    for (int i = 0; i < x.Size; i++)
                    {
                        x_l[i] = (x_l[i] < 0) ? Double.NegativeInfinity : (x_l[i] == 0) ? 0 : Double.PositiveInfinity;
                    }
                    return x_l;
                }

                // Шаг 2. Вычисление центра тяжести симплекса
                for (int i = 0; i < x_c.Size; i++) // начальный вектор (0,0,...,0)
                    x_c[i] = 0;

                for (int i = 0; i < x_c.Size; i++) 
                    x_c += smpl[i];
                x_c = x_c / x_c.Size;

                // 3Шаг . Отражение
                x_r = x_c * (1 + alpha) - x_h * alpha; f_r = f.value(x_r);

                // Шаг 4.
                if (f_r <= f_l)
                {	// Шаг 4a.
                    x_e = x_c * (1 - gamma) + x_r * gamma;
                    f_e = f.value(x_e);
                    if (f_e < f_l)
                    {
                        smpl[smpl.Size-1] = x_e;
                        fValues[smpl.Size - 1] = f_e;
                    }
                    else
                    {
                        smpl[smpl.Size - 1] = x_r;
                        fValues[smpl.Size - 1] = f_r;
                    }
                }
                if ((f_l < f_r) && (f_r <= f_g))
                {	// Шаг 4.b
                    smpl[smpl.Size - 1] = x_r;
                    fValues[smpl.Size - 1] = f_r;
                }
                flag = false;
                if ((f_h >= f_r) && (f_r > f_g))
                {	// Шаг 4c.
                    flag = true;
                    tempD = f_h;
                    tempV = x_h;
                    smpl[smpl.Size - 1] = x_r;
                    fValues[smpl.Size - 1] = f_r;
                    x_r = tempV;
                    f_r = tempD;
                }
                // Шаг 4d.
                if (f_r > f_h) flag = true;
                if (flag)
                {	// Шаг 5. Сжатие
                    x_s = x_h * beta + x_c * (1 - beta);
                    f_s = f.value(x_s);
                    if (f_s < f_h)
                    {	// 6.
                        tempD = f_h;
                        tempV = x_h;
                        smpl[smpl.Size - 1] = x_s;
                        fValues[smpl.Size - 1] = f_s;
                        x_s = tempV;
                        f_s = tempD;
                    }
                    else
                    {	// Шаг 7. Глобальное сжатие
                        for (int i = 0; i < smpl.Size; i++)
                            smpl[i] = x_l + (smpl[i] - x_l) / 2;
                    }
                }

            }
            return x_l;
        }

        private void Sort(Simplex smpl, double[] f)
        {
            double f1;
            Vector v1;

            for (int i = 1; i < smpl.Size; i++)
                for (int j = i; j >= 1; j--)
                    if (f[j - 1] > f[j])
                    {
                        f1 = f[j];
                        v1 = smpl[j];
                        
                        f[j] = f[j - 1];
                        smpl[j] = smpl[j - 1];
                        f[j - 1] = f1;
                        smpl[j - 1] = v1;
                    }
                    else break;
        }

        private bool QuitCase(Simplex s, double e)
        {
            for (int i = 0; i < s.Size-1; i++)
                for (int j = i+1; j < s.Size; j++)
                {
                    if (Vector.Dist(s[i], s[j]) > e)
                        return false;
                }
            return true;
        }

        private class Simplex
        {
            private Vector[] f;

            public Simplex(Vector startVector)
            {
                f = new Vector[startVector.Size + 1];
                f[0] = startVector;
            }

            public Vector this[int i]
            {
                set { f[i] = value; }
                get { return f[i]; }
            }

            public int Size { get { return f.Length; } }

            public static Simplex getRegularSimplex(Vector startVector)
            {
                Simplex smpl = new Simplex(startVector);
                double r = 1.0/(smpl.Size - 1)/Math.Sqrt(2) * (Math.Sqrt(smpl.Size) + smpl.Size-2);
                double s = 1.0/(smpl.Size - 1)/Math.Sqrt(2) * (Math.Sqrt(smpl.Size) - 1);
                for (int i = 1; i < smpl.Size; i++)
                {
                    Vector v = new Vector(startVector.Size);
                    for (int j = 0; j < v.Size; j++)
                    {
                        if (i - 1 == j)
                            v[j] = startVector[j] + r;
                        else
                            v[j] = startVector[j] + s;
                    }
                    smpl[i] = v;
                }

                return smpl;
            }

        }
    }
}
