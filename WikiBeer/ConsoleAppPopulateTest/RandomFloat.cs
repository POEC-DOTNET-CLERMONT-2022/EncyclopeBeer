using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppPopulateTest
{
    public class RandomFloat
    {
        Random random = new Random();

        public float RandABV()
        {
            double minValue = 4.50;
            double maxValue = 7.00;
            double range = maxValue - minValue;

            double sample = random.NextDouble();
            double scaled = (sample * range) + minValue;
            float degree = (float)scaled;

            return degree;
        }

        public float RandIBU()
        {
            double minValue = 1;
            double maxValue = 150;
            double range = maxValue - minValue;

            double sample = random.NextDouble();
            double scaled = (sample * range) + minValue;
            float degree = (float)scaled;

            return degree;
        }
    }
}
