using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DadSimulator.GraphicObjects
{
    public class LevelBackground : IGraphicObject
    {
        protected Texture2D m_texture;
        protected string m_name;

        /// <summary>
        /// Get the world position of the texture.
        /// </summary>
        public Vector2 Position;

        public LevelBackground(string name, Texture2D texture2D, Vector2 position)
        {
            m_name = name;
            m_texture = texture2D;
            Position = position;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(m_texture, Position, null, Color.White);
        }

        public void Initialize()
        {
        }

        public void Update(double elapsedTime)
        {
            
        }

        public string GetName()
        {
            return m_name;
        }
    }
}
