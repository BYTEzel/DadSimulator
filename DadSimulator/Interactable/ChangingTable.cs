using DadSimulator.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DadSimulator.Interactable
{
    public class ChangingTable : AnimatedInteractableBase
    {

        public ChangingTable(Spritesheet spritesheet, Vector2 position, Vector2 interactablePosition, Stats stats)
            : base(spritesheet, position, interactablePosition, stats)
        {
        }

        public override void ExecuteCommand()
        {
            
        }

        public override string GetCommand()
        {
            return "Change diapers/clothes";
        }

        public override string GetName()
        {
            return "Changing table";
        }

        public override string GetState()
        {
            return m_stats.RequestFormated(new List<StatName> { StatName.Clothing, StatName.Supplies });
        }

        public override void Update(double elapsedTime)
        {
            base.Update(elapsedTime);
        }
    }
}
