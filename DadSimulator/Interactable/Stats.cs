using System.Collections.Generic;
using DadSimulator.Misc;

namespace DadSimulator.Interactable
{
    public enum StatName { Clothing }
    public class Stats
    {
        private readonly Dictionary<StatName, RangedValue> m_stats;


        public Stats()
        {
            const double min = 0, max = 100;
            m_stats = new Dictionary<StatName, RangedValue>
            {
                { StatName.Clothing, new RangedValue(min, max, max) }
            };
        }

    }
}
