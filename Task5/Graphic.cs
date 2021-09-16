using System.Collections.Generic;

namespace Task5
{
    public class Graphic
    {
        public double X { get; set; }

        public double Y { get; set; }

        public List<int> Iterations { get; set; } = new List<int>();

        public List<double> Accuracies { get; set; } = new List<double>();

    }
}
