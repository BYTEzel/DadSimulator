using System;

namespace DadSimulator.Misc
{
    public class RangedValue
    {
        public readonly double Min;
        public readonly double Max;
        public double Value { get; private set; }

        public RangedValue(double min, double max, double value)
        {
            ValidateValues(min, max, value);

            Min = min;
            Max = max;
            Value = value;
        }

        private static void ValidateValues(double min, double max, double value)
        {
            if (max < min)
            {
                throw new ArgumentException("Min value larger than Max, unable to create range");
            }

            if (value < min || value > max)
            {
                throw new ArgumentException("Value is not in the interval between min and max");
            }
        }

        public void Change(double difference)
        {
            Value += difference;

            Value = (Value > Max) ? Max : Value;
            Value = (Value < Min) ? Min : Value;
        }
    }
}
