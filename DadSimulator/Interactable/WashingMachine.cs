using DadSimulator.Animation;
using DadSimulator.Misc;
using Microsoft.Xna.Framework;
using System;

namespace DadSimulator.Interactable
{
    public class WashingMachine : AnimatedInteractableBase
    {
        private bool m_isRunning;
        private readonly TimeSpan m_washTime;
        private TimeSpan m_startTime;
        private readonly Timer m_timer;

        public WashingMachine(Spritesheet spritesheet, Vector2 position, Vector2 interactionPosition, Timer timer, Stats stats) : 
            base(spritesheet, position, interactionPosition, stats)
        {
            m_isRunning = false;
            m_spritesheet.Position = position;
            m_spritesheet.FPS = 2;
            SetAnimationState();
            m_washTime = new TimeSpan(0, 15, 0);
            m_timer = timer;
            m_stats.ChangeValue(StatName.Clothing, -10);
        }

        public override void ExecuteCommand()
        {
            SwitchMachineOn();
        }

        private void SetAnimationState()
        {
            m_spritesheet.SetAnimation(m_isRunning ? "washing" : "idle");
        }

        private void SwitchMachineOn()
        {
            m_startTime = m_timer.GetGameTime();
            m_isRunning = true;
        }


        public override string GetCommand()
        {
            return m_isRunning ? null : "Switch on";
        }

        public override string GetName()
        {
            return "Washing machine";
        }

        public override string GetState()
        {
            if (m_isRunning)
            {
                return "Washing";
            }
            else
            {
                return "Awaiting instructions";
            }
        }


        public override void Update(double elapsedTime)
        {
            if (m_isRunning)
            {
                if (m_timer.GetGameTime()- m_startTime > m_washTime)
                {
                    m_stats.ChangeValue(StatName.Clothing, 20);
                    m_isRunning = false;
                }
            }
            SetAnimationState();
            base.Update(elapsedTime);
        }
    }
}
