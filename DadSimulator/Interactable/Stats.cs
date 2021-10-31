using System.Collections.Generic;
using DadSimulator.Misc;

namespace DadSimulator.Interactable
{
    public enum StatName { Clothing }
    public class Stats
    {
        private readonly Dictionary<StatName, RangedValue> m_stats;
        public const double Min = 0;
        public const double Max = 100;

        public Stats()
        {
            m_stats = new Dictionary<StatName, RangedValue>
            {
                { StatName.Clothing, new RangedValue(Min, Max, Max) }
            };
        }

        public double Request(StatName name)
        {
            return m_stats[name].Value;
        }

        public void ChangeValue(StatName name, double diff)
        {
            m_stats[name].Change(diff);
        }

    }
}
