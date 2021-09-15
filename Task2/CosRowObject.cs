using System;

namespace Task2
{
    public class CosRowObject
    {
        private const int count = 6;

        private double cosTrueValue;

        private readonly double _argument;

        private int[] _iteration = new int[count];

        private double[] funValue = new double[count];

        private double[] _accuracy = { 0.1, 0.01, 0.001, 0.0001, 0.00001, 0.000001 };

        public CosRowObject(double argument)
        {
            _argument = argument;

            cosTrueValue = Math.Cos(_argument);
        }

        public double Argument => _argument;

        public int[] Iteration => _iteration;

        public double[] FunValue => funValue;

        public double[] Accuracy => _accuracy;

        public void ComputeAll()
        {

            for (int i = 0; i < _accuracy.Length; i++)
            {
                Compute(_accuracy[i], i);
            }
           
        }

        private void Compute(double accuracy, int itr)
        {
            double sum = 100;

            int itter = 0;

            while (Math.Abs(sum - cosTrueValue) > accuracy)
            {
                if (itter == 12)
                    break;

                if (itter == 0)
                    sum = 0;

                sum += Math.Pow(-1, itter) * (Math.Pow(_argument, 2*itter) / F(2 * itter) );

                itter++;
            }

            _iteration[itr] = itter;

            funValue[itr] = Math.Round(sum, 5);
        }


        private long F(int f)
        {
            long m = 1;

            for (int i = 1; i <= f; i++)
            {
                m *= i;
            }

            return m;
        }

    }
}
