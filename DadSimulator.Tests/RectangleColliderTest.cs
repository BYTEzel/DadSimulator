using DadSimulator.Collider;
using NUnit.Framework;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DadSimulator.Tests
{
    public class RectangleColliderTest
    {
        [Test]
        public void NoIntersection()
        {
            const int xy1 = 10;
            var xyPoint = new Point(xy1, xy1);

            var pc = new List<PointCloud>() { new PointCloud() { Id = "comparison", Shift = Vector2.Zero, PointsInOrigin = new List<Point>() { xyPoint, new Point(20, 40) } } };
            var collider = new RectangleCollider(new Rectangle(0, 0, 5, 5), Vector2.Zero);
            var intersectingPointClouds = collider.Intersection(pc);
            Assert.IsEmpty(intersectingPointClouds); // No overlap = empty
            // After shifting, it should have an overlap
            collider.SetShift(new Vector2(xy1, xy1));
            intersectingPointClouds = collider.Intersection(pc);
            Assert.AreEqual(1, intersectingPointClouds.Count());
            Assert.AreEqual(1, intersectingPointClouds.ElementAt(0).PointsInOrigin.Count);
            Assert.AreEqual(xyPoint.X, intersectingPointClouds.ElementAt(0).PointsInOrigin[0].X);
            Assert.AreEqual(xyPoint.Y, intersectingPointClouds.ElementAt(0).PointsInOrigin[0].Y);
            collider.SetShift(new Vector2(-1, -1));
            intersectingPointClouds = collider.Intersection(pc);
            Assert.IsEmpty(intersectingPointClouds); // No overlap = empty
        }

        [Test]
        public void Padding()
        {
            // While padding, the resulting rect is expected to be smaller than with padding = 0 (default)
            var rectWithoutPadding = new Rectangle(0, 0, 10, 10);            
            var collider = new RectangleCollider(rectWithoutPadding, Vector2.Zero, 2);
            var alignedPoints = collider.GetAlignedPoints();
            Assert.IsEmpty(alignedPoints.PointsInOrigin.Where(p => p.X <= rectWithoutPadding.X));
            Assert.IsEmpty(alignedPoints.PointsInOrigin.Where(p => p.X >= rectWithoutPadding.Width));
            Assert.IsEmpty(alignedPoints.PointsInOrigin.Where(p => p.Y <= rectWithoutPadding.Y));
            Assert.IsEmpty(alignedPoints.PointsInOrigin.Where(p => p.Y >= rectWithoutPadding.Height));
        }

        [Test]
        public void TooLargePadding()
        {
            // While padding, the resulting rect is expected to be smaller than with padding = 0 (default)
            var rectWithoutPadding = new Rectangle(0, 0, 10, 10);
            Assert.Throws<System.ArgumentException>(() => new RectangleCollider(rectWithoutPadding, Vector2.Zero, rectWithoutPadding.Width));
        }

        [Test]
        public void Intersection()
        {

        }

        [Test]
        public void PartialIntersection()
        {

        }
    }
}
