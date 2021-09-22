using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DadSimulator.Collider
{
    public struct PointCloud
    {
        public string Id;
        public List<Point> PointsInOrigin;
        public Vector2 Shift;
    }
}
