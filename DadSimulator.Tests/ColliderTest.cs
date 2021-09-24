using DadSimulator.Collider;
using NUnit.Framework;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DadSimulator.Tests
{
    class ColliderTest
    {
        private AlignedPointCloud CreatePointsInOrigin(int x, int y, int width, int height)
        {
            var collider = new RectangleCollider(new Rectangle(x, y, width, height));
            return new AlignedPointCloud() { PointCloud = collider.GetPointCloud(), Shift = Vector2.Zero };
        }

        [TestCase(0, 0, 50, 50, 100, 100, 50, 50, IntersectionType.NoIntersection)]
        [TestCase(0, 0, 10, 10, 10, 10, 50, 50, IntersectionType.NoIntersection)]
        [TestCase(100, 100, 50, 50, 10, 10, 50, 50, IntersectionType.NoIntersection)]
        [TestCase(0, 0, 10, 10, 5, 5, 20, 20, IntersectionType.Intersection)]
        [TestCase(2, 2, 5, 5, 0, 0, 50, 50, IntersectionType.CompletelyContained)]
        [TestCase(0, 0, 10, 10, 0, 0, 10, 10, IntersectionType.Equal)]
        public void Intersection(int x1, int y1, int width1, int height1, int x2, int y2, int width2, int height2, IntersectionType expectedType)
        {
            var apc1 = CreatePointsInOrigin(x1, y1, width1, height1);
            var apc2 = CreatePointsInOrigin(x2, y2, width2, height2);
            Assert.AreEqual(expectedType, Collision.Intersection(apc1, apc2).Type);
        }
    }
}
