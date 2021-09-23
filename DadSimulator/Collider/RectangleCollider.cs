using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DadSimulator.Collider
{
    public class RectangleCollider : ICollider
    {
        private Rectangle m_rect;
        private PointCloud m_pointCloud;

        public RectangleCollider(Rectangle rect, int padding = 0)
        {
            AssignCtorValues(rect, padding);
        }

        public RectangleCollider(Texture2D texture, int padding = 0)
        {
            var rect = new Rectangle(Point.Zero, new Point(texture.Width, texture.Height));
            AssignCtorValues(rect, padding);
        }

        private void AssignCtorValues(Rectangle rect, int padding)
        {
            AssignRectangle(rect, padding);
            m_pointCloud = new PointCloud();
            ComputePointCloud();
        }

        private void AssignRectangle(Rectangle rect, int padding)
        {
            if (padding >= rect.Height && padding >= rect.Width)
            {
                throw new System.ArgumentException("Padding is larger than the target rectangle.");
            }
            m_rect = new Rectangle(rect.X - padding, rect.Y - padding, rect.Width + padding, rect.Height + padding);
        }

        private void ComputePointCloud()
        {
            m_pointCloud.PointsInOrigin = new List<Point>();
            for (int x = m_rect.X; x < m_rect.Width; x++)
            {
                for (int y = m_rect.Y; y < m_rect.Height; y++)
                {
                    m_pointCloud.PointsInOrigin.Add(new Point(x, y));
                }
            }
        }

        public PointCloud GetPointCloud()
        {
            return m_pointCloud;
        }
    }
}
