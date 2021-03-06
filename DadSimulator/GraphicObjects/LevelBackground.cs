using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DadSimulator.GraphicObjects
{
    public class LevelBackground : IGraphicObject
    {
        protected Texture2D m_texture;

        /// <summary>
        /// Get the world position of the texture.
        /// </summary>
        public Vector2 Position;

        public LevelBackground(Texture2D texture2D, Vector2 position)
        {
            m_texture = texture2D;
            Position = position;
        }

        public virtual void Draw(SpriteBatch batch)
        {
            batch.Draw(m_texture, Position, null, Color.White);
        }

        public virtual void Initialize()
        {
        }

        public virtual void Update(double elapsedTime)
        {
            
        }
    }
}
