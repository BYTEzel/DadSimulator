using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DadSimulator.GraphicObjects
{
    public enum RelativePositionReference { TopLeft, Centered };

    public class StationaryObject : IGraphicObject
    {
        protected Vector2 m_position;
        protected Texture2D m_texture;
        protected Vector2 m_relPosition;

        /// <summary>
        /// Get the world position of the texture.
        /// </summary>
        public Vector2 Position { get => m_position; }
        /// <summary>
        /// Get the position of the texture in the local coordinate system.
        /// </summary>
        public Vector2 RelativePosition { get => m_relPosition; }

        public StationaryObject(Texture2D texture2D, Vector2 position, RelativePositionReference relativePosition)
        {
            m_texture = texture2D;
            m_position = position;

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
    }
}
