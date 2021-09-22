using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DadSimulator.Collider
{
    public class RectangleCollider : ICollider
    {
        private Rectangle m_rect;
        private PointCloud m_pointCloud;

        public RectangleCollider(Rectangle rect, Vector2 shift, int padding = 0)
        {
            AssignCtorValues(rect, shift, padding);
        }

        public RectangleCollider(Texture2D texture, Vector2 shift, int padding = 0)
        {
            var rect = new Rectangle(Point.Zero, new Point(texture.Width, texture.Height));
            AssignCtorValues(rect, shift, padding);
        }

        private void AssignCtorValues(Rectangle rect, Vector2 shift, int padding)
        {
            AssignRectangle(rect, shift, padding);
            m_pointCloud = new PointCloud();
            m_pointCloud.Shift = shift;
            ComputePointCloud();
        }

        private void AssignRectangle(Rectangle rect, Vector2 shift, int padding)
        {
            if (padding >= rect.Height && padding >= rect.Width)
            {
                throw new System.ArgumentException("Padding is larger than the target rectangle.");
            }
            m_rect = new Rectangle(rect.X + (int)shift.X - padding, rect.Y + (int)shift.Y - padding, rect.Width + padding, rect.Height + padding);
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

        public PointCloud GetAlignedPoints()
        {
            return m_pointCloud;
        }

        public List<PointCloud> Intersection(IEnumerable<PointCloud> pointClouds)
        {
            var intersectingPcs = new List<PointCloud>();
            var shiftedRect = new Rectangle(m_rect.X + (int)m_pointCloud.Shift.X, m_rect.Y + (int)m_pointCloud.Shift.Y, m_rect.Width, m_rect.Height);
            foreach (var pc in pointClouds)
            {
                var intersectingPc = new PointCloud();
                intersectingPc.PointsInOrigin = new List<Point>();
                intersectingPc.Shift = pc.Shift;
                intersectingPc.Id = pc.Id;

                foreach (var point in pc.PointsInOrigin)
                {
                    if (shiftedRect.Contains(point))
                    {
                        intersectingPc.PointsInOrigin.Add(point);
                    }
                }

                if (intersectingPc.PointsInOrigin.Count > 0)
                {
                    intersectingPcs.Add(intersectingPc);
                }
            }

            return intersectingPcs;
        }

        public void SetShift(Vector2 shift)
        {
            m_pointCloud.Shift = shift;
        }
    }
}
