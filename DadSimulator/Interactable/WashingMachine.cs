using DadSimulator.Collider;
using DadSimulator.GraphicObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DadSimulator.Interactable
{
    public class WashingMachine : IInteractable, ICollidable, IGraphicObject
    {
        private bool m_isRunning;
        private const char m_switchOn = '1';
        private readonly Texture2D m_texture;
        private AlignedPointCloud m_alignedPointCloudCollider, m_alignedPointCloudInteraction;

        public WashingMachine(Texture2D texture, Vector2 position, ICollider collisionDetection, ICollider interactionCollider)
        {
            m_isRunning = false;
            m_texture = texture;
            m_alignedPointCloudCollider = new AlignedPointCloud { Shift = position, PointCloud = collisionDetection.GetPointCloud() };
            m_alignedPointCloudInteraction = new AlignedPointCloud { Shift = position, PointCloud = interactionCollider.GetPointCloud() };
        }

        public void ExecuteCommand(char key)
        {
            if (m_switchOn == key)
            {
                SwitchMachineOn();
            }
        }

        private void SwitchMachineOn()
        {
            m_isRunning = true;
        }


        public List<Command> GetCommands()
        {
            if (m_isRunning)
            {
                return new List<Command>() { new Command() { Description = "Switch on", Key = m_switchOn } };
            }
            return new List<Command>();
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
            batch.Draw(m_texture, m_alignedPointCloudCollider.Shift, null, Color.Red);
        }

        public void SetShift(Vector2 shift)
        {
            m_alignedPointCloudCollider.Shift = shift;
        }

        public AlignedPointCloud GetInteractableAlignedPointCloud()
        {
            return m_alignedPointCloudInteraction;
        }

        public AlignedPointCloud GetColliderAlignedPointCloud()
        {
            return m_alignedPointCloudCollider;
        }
    }
}
