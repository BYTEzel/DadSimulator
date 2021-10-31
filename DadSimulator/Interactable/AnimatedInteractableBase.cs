using DadSimulator.Animation;
using DadSimulator.GraphicObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DadSimulator.Interactable
{
    public class AnimatedInteractableBase : IInteractable, IGraphicObject
    {
        protected readonly Spritesheet m_spritesheet;
        protected readonly Vector2 m_interactionPosition;
        protected readonly Stats m_stats;

        public AnimatedInteractableBase(Spritesheet spritesheet, Vector2 position, Vector2 interactionPosition, Stats stats)
        {
            m_spritesheet = spritesheet;
            m_spritesheet.Position = position;
            m_interactionPosition = interactionPosition;
            m_stats = stats;
        }

        public virtual void Draw(SpriteBatch batch)
        {
            m_spritesheet.Draw(batch);
        }

        public virtual void ExecuteCommand()
        {
        }

        public virtual string GetCommand()
        {
            throw new NotImplementedException();
        }

        public virtual string GetName()
        {
            throw new NotImplementedException();
        }

        public Vector2 GetPosition()
        {
            return m_interactionPosition;
        }

        public virtual string GetState()
        {
            throw new NotImplementedException();
        }

        public virtual void Initialize()
        {
        }

        public virtual void Update(double elapsedTime)
        {
            m_spritesheet.Update(elapsedTime);
        }
    }
}
