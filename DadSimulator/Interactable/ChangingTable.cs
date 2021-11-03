using DadSimulator.Animation;
using DadSimulator.Misc;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DadSimulator.Interactable
{
    public class ChangingTable : AnimatedInteractableBase
    {
        private const double supplyMalus = 10, clothingMalus = 10;
        private readonly TimeWarp m_timeWarp;

        public ChangingTable(Spritesheet spritesheet, Vector2 position, Vector2 interactablePosition, Stats stats, TimeWarp timeWarp)
            : base(spritesheet, position, interactablePosition, stats)
        {
            m_timeWarp = timeWarp;
        }

        public override void ExecuteCommand()
        {
            if (IsAbleToChange)
            {
                Change();
            }
        }

        private void Change()
        {
            if (m_timeWarp.WarpTime(new System.TimeSpan(0, 15, 0), new System.TimeSpan(0, 0, 20)))
            {
                m_stats.ChangeValue(StatName.Clothing, -clothingMalus);
                m_stats.ChangeValue(StatName.Supplies, -supplyMalus);
            }
        }

        private bool IsAbleToChange { get => m_stats.Request(StatName.Supplies) > supplyMalus && m_stats.Request(StatName.Clothing) > clothingMalus; }

        public override string GetCommand()
        {
            return "change diapers/clothes";
        }

        public override string GetName()
        {
            return "Changing table";
        }

        public override string GetState()
        {
            if (m_timeWarp.WarpInProgress)
            {
                return "Changing diapers/clothes...";
            }
            return m_stats.RequestFormated(new List<StatName> { StatName.Clothing, StatName.Supplies });
        }

        public override void Update(double elapsedTime)
        {
            base.Update(elapsedTime);
        }
    }
}
