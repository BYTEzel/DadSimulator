using DadSimulator.GraphicObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DadSimulator.Interactable
{
    public class WashingMachine : IInteractable, IGraphicObject
    {
        private bool m_isRunning;
        private readonly Texture2D m_texture;
        private Vector2 m_position, m_interactionPosition;

        public WashingMachine(Texture2D texture, Vector2 position, Vector2 interactionPosition)
        {
            m_isRunning = false;
            m_texture = texture;
            m_position = position;
            m_interactionPosition = interactionPosition;
        }

        public void ExecuteCommand()
        {
            SwitchMachineOn();
        }

        private void SwitchMachineOn()
        {
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
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(m_texture, m_position, null, Color.Red);
        }


        public Vector2 GetLocation()
        {
            return m_interactionPosition;
        }
    }
}
