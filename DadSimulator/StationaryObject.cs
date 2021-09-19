using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace DadSimulator
{
    enum RelativePosition { TopLeft, Centered };

    class StationaryObject : IGraphicObject
    {
        protected Texture2D m_texture;
        protected Vector2 m_position;
        protected Vector2 m_relPosition;

        public StationaryObject(Texture2D texture2D, Vector2 position, RelativePosition relativePosition)
        {
            m_texture = texture2D;
            m_position = position;

            switch (relativePosition)
            {
                case RelativePosition.Centered:
                    m_relPosition = new Vector2(m_texture.Width / 2, m_texture.Height / 2);
                    break;
                case RelativePosition.TopLeft:
                default:
                    m_relPosition = Vector2.One;
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
