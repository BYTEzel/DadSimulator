using DadSimulator.Animation;
using DadSimulator.GraphicObjects;
using DadSimulator.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DadSimulator.Interactable
{
    public class WashingMachine : IInteractable, IGraphicObject
    {
        private bool m_isRunning;
        private readonly Spritesheet m_spritesheet;
        private Vector2 m_interactionPosition;
        private readonly TimeSpan m_washTime;
        private TimeSpan m_startTime;
        private readonly Timer m_timer;
        private readonly Stats m_stats;

        public WashingMachine(Spritesheet spritesheet, Vector2 position, Vector2 interactionPosition, Timer timer, Stats stats)
        {
            m_isRunning = false;
            m_spritesheet = spritesheet;
            m_spritesheet.Position = position;
            m_spritesheet.FPS = 2;
            m_interactionPosition = interactionPosition;
            SetAnimationState();
            m_washTime = new TimeSpan(0, 15, 0);
            m_timer = timer;
            m_stats = stats;
            m_stats.ChangeValue(StatName.Clothing, -10);
        }

        public void ExecuteCommand()
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


        public string GetCommand()
        {
            return m_isRunning ? null : "Switch on";
        }

        public string GetName()
        {
            return "Washing machine";
        }

        public string GetState()
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

        public void Initialize()
        {
        }

        public void Update(double elapsedTime)
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
            m_spritesheet.Update(elapsedTime);
        }

        public void Draw(SpriteBatch batch)
        {
            m_spritesheet.Draw(batch);
        }


        public Vector2 GetPosition()
        {
            return m_interactionPosition;
        }
    }
}
