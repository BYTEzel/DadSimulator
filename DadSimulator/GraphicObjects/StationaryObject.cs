﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DadSimulator.Collider;

namespace DadSimulator.GraphicObjects
{
    public enum RelativePositionReference { TopLeft, Centered };

    public class StationaryObject : IGraphicObject, ICollidable
    {
        protected Vector2 m_position;
        protected Texture2D m_texture;
        protected Vector2 m_relPosition;
        protected string m_name;
        protected AlignedPointCloud m_alignedPointCloud;

        /// <summary>
        /// Get the world position of the texture.
        /// </summary>
        public Vector2 Position { get => m_position; }
        /// <summary>
        /// Get the position of the texture in the local coordinate system.
        /// </summary>
        public Vector2 RelativePosition { get => m_relPosition; }

        public StationaryObject(string name, Texture2D texture2D, Vector2 position, RelativePositionReference relativePosition, ICollider collider = null)
        {
            m_name = name;
            m_texture = texture2D;
            m_position = position;

            if (collider == null)
            {
                collider = new RectangleCollider(texture2D);
            }
            m_alignedPointCloud = new AlignedPointCloud { Shift = position, PointCloud = collider.GetPointCloud() };

            switch (relativePosition)
            {
                case RelativePositionReference.Centered:
                    m_relPosition = new Vector2(m_texture.Width / 2, m_texture.Height / 2);
                    break;
                case RelativePositionReference.TopLeft:
                default:
                    m_relPosition = Vector2.Zero;
                    break;
            }
        }

        public virtual void Draw(SpriteBatch batch)
        {
            batch.Draw(m_texture, m_position, null, Color.White, 0f, m_relPosition,
                Vector2.One, SpriteEffects.None, 0f);
        }

        public virtual void Initialize()
        {
        }

        public virtual void Update(double elapsedTime)
        {
            
        }

        public string GetName()
        {
            throw new System.NotImplementedException();
        }

        public void SetShift(Vector2 shift)
        {
            throw new System.NotImplementedException();
        }

        public AlignedPointCloud GetAlignedPointCloud()
        {
            throw new System.NotImplementedException();
        }
    }
}
