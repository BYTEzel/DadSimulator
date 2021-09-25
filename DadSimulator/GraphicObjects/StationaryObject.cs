using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DadSimulator.Collider;

namespace DadSimulator.GraphicObjects
{
    public enum RelativePositionReference { TopLeft, Centered };

    public class StationaryObject : IGraphicObject, ICollidable
    {
        protected Texture2D m_texture;
        protected Vector2 m_relPosition;
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
        /// <summary>
        /// Get the position of the texture in the local coordinate system.
        /// </summary>
        public Vector2 RelativePosition { get => m_relPosition; }

        public StationaryObject(string name, Texture2D texture2D, Vector2 position, RelativePositionReference relativePosition, ICollider collider = null)
        {
            m_name = name;
            m_texture = texture2D;

            if (collider == null)
            {
                collider = new RectangleCollider(texture2D);
            }

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
            m_alignedPointCloud = new AlignedPointCloud { Shift = position + m_relPosition, PointCloud = collider.GetPointCloud() };
        }

        public virtual void Draw(SpriteBatch batch)
        {
            batch.Draw(m_texture, Position, null, Color.White, 0f, m_relPosition,
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
