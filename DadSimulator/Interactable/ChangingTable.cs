using DadSimulator.Animation;
using DadSimulator.GraphicObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DadSimulator.Interactable
{
    public class ChangingTable : IInteractable, IGraphicObject
    {
        private readonly Spritesheet m_spritesheet;

        public ChangingTable(Spritesheet spritesheet, Vector2 position, Vector2 interactablePosition, Stats stats)
        {
            m_spritesheet = spritesheet;
        }

        public void Draw(SpriteBatch batch)
        {
            throw new NotImplementedException();
        }

        public void ExecuteCommand()
        {
            throw new NotImplementedException();
        }

        public string GetCommand()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public Vector2 GetPosition()
        {
            throw new NotImplementedException();
        }

        public string GetState()
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Update(double elapsedTime)
        {
            throw new NotImplementedException();
        }
    }
}
