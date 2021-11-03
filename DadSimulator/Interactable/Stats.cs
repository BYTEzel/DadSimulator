using System.Collections.Generic;
using DadSimulator.Misc;

namespace DadSimulator.Interactable
{
    public enum StatName 
    { 
        Clothing, 
        Supplies 
    }


    public class Stats
    {
        private readonly Dictionary<StatName, RangedValue> m_stats;
        public const double Min = 0;
        public const double Max = 100;

        public Stats()
        {
            m_stats = new Dictionary<StatName, RangedValue>
            {
                { StatName.Clothing, new RangedValue(Min, Max, Max) },
                { StatName.Supplies, new RangedValue(Min, Max, Max) }
            };
        }

        public double Request(StatName name)
        {
            return m_stats[name].Value;
        }

        public string RequestFormated(StatName name)
        {
            var statString = string.Empty;
            switch (name)
            {
                case StatName.Clothing:
                    statString = "Clothing";
                    break;
                case StatName.Supplies:
                    statString = "Supplies";
                    break;
                default:
                    break;
            }

            return $"{statString}: {Request(name)}/{Max:0.}";
        }

        public string RequestFormated(List<StatName> names)
        {
            var returnString = string.Empty;
            foreach (var name in names)
            {
                returnString += $"\n{RequestFormated(name)}";
            }
            return returnString;
        }

        public void ChangeValue(StatName name, double diff)
        {
            m_stats[name].Change(diff);
        }

    }
}
