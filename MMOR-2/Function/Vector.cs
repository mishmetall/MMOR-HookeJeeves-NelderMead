using System.Collections.Generic;
using System;

namespace MMOR_2.Function
{
    class Vector:ICloneable
    {
        private double[] x;

        public Vector()
        {
            x = new double[1];
        }

        public Vector(int size)
        {
            x = new double[size];
        }

        public double this[int i]
        {
            set { x[i] = value; }
            get { return x[i]; }
        }

        public int Size { get { return x.Length; } }

        public static Vector operator +(Vector v1, Vector v2)
        {
            if (v1.Size != v2.Size)
                throw new VectorException();

            Vector sum = new Vector(v1.Size);
            for (int i = 0; i < v1.Size; i++)
            {
                sum[i] = v1[i] + v2[i];
            }

            return sum;
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            if (v1.Size != v2.Size)
                throw new VectorException();

            Vector diff = new Vector(v1.Size);
            for (int i = 0; i < v1.Size; i++)
            {
                diff[i] = v1[i] - v2[i];
            }

            return diff;
        }

        public static Vector operator *(double n, Vector v1)
        {
            Vector prod = new Vector(v1.Size);
            for (int i = 0; i < v1.Size; i++)
            {
                prod[i] = v1[i]*n;
            }

            return prod;
        }

        public static Vector operator *(Vector v1, double n)
        {
            return n*v1;
        }

        public static Vector operator /(Vector v1, double n)
        {
            Vector res = new Vector(v1.Size);
            for (int i = 0; i < v1.Size; i++)
            {
                res[i] = v1[i] / n;
            }

            return res;
        }

        public object Clone()
        {
            Vector clone = new Vector(Size);
            for(int i=0;i<Size;i++)
                clone[i] = x[i];
            return clone;
        }

        internal static double Dist(Vector v1, Vector v2)
        {
            if (v1.Size != v2.Size)
                throw new VectorException();

            double sum = 0;
            for (int i = 0; i < v1.Size; i++)
                sum += (v1[i] - v2[i]) * (v1[i] - v2[i]);

            return Math.Sqrt(sum);
        }
    }

    class VectorException:Exception
    {

    }
}