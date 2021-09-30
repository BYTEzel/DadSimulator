using Microsoft.Xna.Framework;
using System;

namespace DadSimulator.Collider
{
    public struct Size
    {
        public uint Width;
        public uint Height;
    }

    public class CollidableMap : ICollisionChecker
    {
        private Color[,] m_map;
        private const byte m_border = 255;
        private const byte m_noBorder = 0;

        public CollidableMap(Size size)
        {
            m_map = new Color[size.Width, size.Height];
        }

        public CollidableMap(Color[,] map)
        {
            m_map = map;
        }


        public void AddAlignedPointCloud(AlignedPointCloud apc)
        {
            foreach (var pointOrigin in apc.PointCloud.PointsInOrigin)
            {
                var pointAligned = new Point(
                    pointOrigin.X + (int)apc.Shift.X,
                    pointOrigin.Y + (int)apc.Shift.Y);

                CheckAndSetPointInMap(pointAligned);
            }
        }

        private void CheckAndSetPointInMap(Point pointAligned)
        {
            if (IsInMap(pointAligned))
            {
                m_map[pointAligned.X, pointAligned.Y].A = m_border;
            }
            else
            {
                throw new ArgumentOutOfRangeException("apc", "Points in the point cloud are out of map bounds.");
            }
        }

        public void AddRectangle(Rectangle rectangle)
        {
            for (int x = rectangle.X; x < rectangle.X + rectangle.Width; x++)
            {
                for (int y = rectangle.Y; y < rectangle.Y + rectangle.Height; y++)
                {
                    CheckAndSetPointInMap(new Point(x, y));
                }
            }
        }

        /// <summary>
        /// Checks, if all points in the point cloud are intersecting with the borders 
        /// provided in the internal map.
        /// </summary>
        /// <param name="apc">Points to be checked for intersecting with a border.</param>
        /// <returns>True, if a point intersects, false if no intersection occurs or if points are out of bounds.</returns>
        public bool IsColliding(AlignedPointCloud apc)
        {
            bool intersect = false;
            foreach (var pointOrigin in apc.PointCloud.PointsInOrigin)
            {
                var pointAligned = new Point(
                    pointOrigin.X + (int)apc.Shift.X,
                    pointOrigin.Y + (int)apc.Shift.Y);

                if (IsInMap(pointAligned) && m_map[pointAligned.X, pointAligned.Y].A > m_noBorder)
                {
                    intersect = true;
                    break;
                }
            }
            return intersect;
        }

        private bool IsInMap(Point point)
        {
            return IsInRange(point.X, 0, m_map.GetLength(0)) && IsInRange(point.Y, 0, m_map.GetLength(1));
        }

        private bool IsInRange(int number, int lowerBound, int upperBound)
        {
            return number >= lowerBound && number < upperBound;
        }

        public void AddTextureContent(Color[,] content, Vector2 shift)
        {
            for (int x = 0; x < content.GetLength(0); x++)
            {
                for (int y = 0; y < content.GetLength(1); y++)
                {
                    var coordinate = new Point(x + (int)shift.X, y + (int)shift.Y);
                    if (IsInMap(coordinate))
                    {
                        m_map[coordinate.X, coordinate.Y].A = Math.Max(content[x, y].A, m_map[coordinate.X, coordinate.Y].A);
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("shift", "Coordinates are out of bounds");
                    }
                }
            }
        }

        public bool IsColliding(ICollidable collidable)
        {
            return IsColliding(collidable.GetColliderAlignedPointCloud());
        }
    }
}
