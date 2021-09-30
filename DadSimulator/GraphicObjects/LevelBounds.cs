using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DadSimulator.Collider;
using DadSimulator.IO;

namespace DadSimulator.GraphicObjects
{
    public class LevelBounds : IGraphicObject
    {
        protected Texture2D m_texture;
        private ICollisionChecker m_collisionChecker;

        /// <summary>
        /// Get the world position of the texture.
        /// </summary>
        public Vector2 Position;

        public LevelBounds(ITemplateLoader loader, Templates graphic, Vector2 position, ICollisionChecker collisionChecker)
        {
            Position = position;
            m_collisionChecker = collisionChecker;
            m_texture = loader.LoadTemplate(graphic);
            m_collisionChecker.AddTextureContent(loader.LoadTemplateContent(graphic), Position);
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
