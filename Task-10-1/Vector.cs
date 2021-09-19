using System;
using System.Diagnostics;

namespace Task_10_1
{
    public class Vector
    {
        private readonly double[] _a;

        public Vector(int dimension)
        {
            Dimension = dimension;

            _a = new double[Dimension];

            RandInitializer();
        }

        public Vector(double[] element)
        {
            Dimension = element.Length;
            _a = element;
        }

        public int Dimension { get; }

        public double this[int index] => 
            _a[index];

        ~Vector()
        {
            Debug.WriteLine($"Объект {ToString()}");
        }

        public override string ToString()
        {
            string s = $"{Dimension} -";

            foreach (var item in _a)
            {
                s += $"- {item}";
            }

            return s;
        }

        public static Vector operator * (Vector a, double b)
        {
            double[] val = new double[a.Dimension];

            for (int i = 0; i < val.Length; i++)
            {
                val[i] = a[i] * b;
            }

            return new Vector(val);

        }

        public static Vector operator *(double a, Vector b) =>
            b * a;

        public static double operator *(Vector a, Vector b)
        {
            double sum = 0;

            if (a.Dimension != b.Dimension)
                throw new ArgumentOutOfRangeException();

            for (int i = 0; i < a.Dimension; i++)
            {
                sum += a[i] * b[i];
            }

            return sum;
        }

       
        public Vector Copy()
        {
            double[] a = new double[Dimension];

            for (int i = 0; i < Dimension; i++)
            {
                a[i] = _a[i];
            }

            var vector = new Vector(a);

            return vector;
        }

        private void RandInitializer()
        {
            var rand = new Random();

            for (int i = 0; i < Dimension; i++)
            {
                _a[i] = rand.Next(100);
            }
        }

        public double Module()
        {
            double mod = 0;

            for (int i = 0; i < Dimension; i++)
            {
                mod += _a[i] * _a[i];
            }

            return Math.Sqrt(mod);
        }
    }
}
