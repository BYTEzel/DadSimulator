using DadSimulator.Animation;
using DadSimulator.GraphicObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DadSimulator.Interactable
{
    public class WashingMachine : IInteractable, IGraphicObject
    {
        private bool m_isRunning;
        private readonly Spritesheet m_spritesheet;
        private Vector2 m_interactionPosition;

        public WashingMachine(Spritesheet spritesheet, Vector2 position, Vector2 interactionPosition)
        {
            m_isRunning = false;
            m_spritesheet = spritesheet;
            m_spritesheet.Position = position;
            m_spritesheet.FPS = 1;
            m_interactionPosition = interactionPosition;
            SetAnimationState();
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
            m_isRunning = true;
            SetAnimationState();
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
