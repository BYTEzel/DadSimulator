using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DadSimulator.Collider
{
    public class RectangleCollider : ICollider
    {
        private Rectangle m_rect;
        private PointCloud m_pointCloud;

        public RectangleCollider(Rectangle rect, Vector2 shift, int padding = 0)
        {
            m_pointCloud = new PointCloud();
            m_pointCloud.Shift = shift;
            ComputePointCloud();
            m_rect = new Rectangle((int)shift.X + padding, (int)shift.Y + padding, rect.Width - padding, rect.Height - padding);
        }

        private void ComputePointCloud()
        {
            m_pointCloud.PointsInOrigin = new List<Point>();
            for (int x = 0; x < m_rect.Width; x++)
            {
                for (int y = 0; y < m_rect.Height; y++)
                {
                    m_pointCloud.PointsInOrigin.Add(new Point(x, y));
                }
            }

        }

        public PointCloud GetAlignedPoints()
        {
            return m_pointCloud;
        }

        public IEnumerable<PointCloud> Intersection(IEnumerable<PointCloud> pointClouds)
        {
            var intersectingPcs = new List<PointCloud>();
            foreach (var pc in pointClouds)
            {
                var intersectingPc = new PointCloud();
                intersectingPc.PointsInOrigin = new List<Point>();
                intersectingPc.Shift = pc.Shift;
                intersectingPc.Id = pc.Id;

                foreach (var point in pc.PointsInOrigin)
                {
                    if (m_rect.Contains(point))
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
