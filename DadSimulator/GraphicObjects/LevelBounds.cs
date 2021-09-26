using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DadSimulator.Collider;

namespace DadSimulator.GraphicObjects
{
    public class LevelBounds : IGraphicObject, ICollidable
    {
        protected Texture2D m_texture;
        protected string m_name;
        protected AlignedPointCloud m_alignedPointCloud;

        /// <summary>
        /// Get the world position of the texture.
        /// </summary>
        public Vector2 Position
        {
            get => m_alignedPointCloud.Shift;
            set => m_alignedPointCloud.Shift = value;
        }

        public LevelBounds(string name, Texture2D texture2D, Vector2 position, ICollider collider)
        {
            if (collider == null)
            {
                throw new System.ArgumentNullException("Collider must be provided.");
            }
            m_name = name;
            m_texture = texture2D;
            m_alignedPointCloud = new AlignedPointCloud { Shift = position, PointCloud = collider.GetPointCloud() };
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

        public string GetName()
        {
            return m_name;
        }

        public void SetShift(Vector2 shift)
        {
            m_alignedPointCloud.Shift = shift;
        }

        public AlignedPointCloud GetAlignedPointCloud()
        {
            return m_alignedPointCloud;
        }
    }
}
