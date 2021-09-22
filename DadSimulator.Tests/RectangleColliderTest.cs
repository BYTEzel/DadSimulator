using DadSimulator.Collider;
using NUnit.Framework;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using DadSimulator.IO;
using Microsoft.Xna.Framework.Graphics;

namespace DadSimulator.Tests
{
    public class RectangleColliderTest
    {
        private Texture2D m_texture;

        [SetUp]
        public void Setup()
        {
            using (var sim = new DadSimulator())
            {
                sim.RunOneFrame();
                ITemplateLoader loader = sim;
                m_texture = loader.LoadTemplate(Templates.Test);
            }
        }

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
            Assert.AreEqual(1, intersectingPointClouds.Count);
            Assert.AreEqual(1, intersectingPointClouds[0].PointsInOrigin.Count);
            Assert.AreEqual(xyPoint.X, intersectingPointClouds[0].PointsInOrigin[0].X);
            Assert.AreEqual(xyPoint.Y, intersectingPointClouds[0].PointsInOrigin[0].Y);
            collider.SetShift(new Vector2(-1, -1));
            intersectingPointClouds = collider.Intersection(pc);
            Assert.IsEmpty(intersectingPointClouds); // No overlap = empty
        }

        [Test]
        public void Padding()
        {
            // While padding, the resulting rect is expected to be larger than with padding = 0 (default)
            var rectWithoutPadding = new Rectangle(2, 2, 8, 8);            
            var collider = new RectangleCollider(rectWithoutPadding, Vector2.Zero, 2);
            var alignedPoints = collider.GetAlignedPoints();
            Assert.IsNotEmpty(alignedPoints.PointsInOrigin.Where(p => p.X < rectWithoutPadding.X));
            Assert.IsNotEmpty(alignedPoints.PointsInOrigin.Where(p => p.X < rectWithoutPadding.Width));
            Assert.IsNotEmpty(alignedPoints.PointsInOrigin.Where(p => p.Y < rectWithoutPadding.Y));
            Assert.IsNotEmpty(alignedPoints.PointsInOrigin.Where(p => p.Y < rectWithoutPadding.Height));
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
            var pc = CreatePointCloudBetween0and100();
            var collider = new RectangleCollider(new Rectangle(0, 0, 100, 100), Vector2.Zero);
            var pcIntersection = collider.Intersection(pc);

            // Completely compare all list entries
            Assert.AreEqual(pc.Count, pcIntersection.Count);
            for (int listIndex = 0; listIndex < pc.Count; listIndex++)
            {
                Assert.AreEqual(pc[listIndex].Id, pcIntersection[listIndex].Id);
                Assert.AreEqual(pc[listIndex].Shift, pcIntersection[listIndex].Shift);

                for (int pointIndex = 0; pointIndex < pc[listIndex].PointsInOrigin.Count; pointIndex++)
                {
                    Assert.AreEqual(pc[listIndex].PointsInOrigin[pointIndex].X,
                        pcIntersection[listIndex].PointsInOrigin[pointIndex].X);
                    Assert.AreEqual(pc[listIndex].PointsInOrigin[pointIndex].Y, 
                        pcIntersection[listIndex].PointsInOrigin[pointIndex].Y);
                }
            }
        }

        [Test]
        public void TextureInitialization()
        {
            var collider = new RectangleCollider(m_texture, Vector2.Zero);
            var alignedPoints = collider.GetAlignedPoints();
            Assert.IsEmpty(alignedPoints.PointsInOrigin.Where(p => p.X < 0));
            Assert.IsEmpty(alignedPoints.PointsInOrigin.Where(p => p.X > m_texture.Width));
            Assert.IsEmpty(alignedPoints.PointsInOrigin.Where(p => p.Y < 0));
            Assert.IsEmpty(alignedPoints.PointsInOrigin.Where(p => p.Y > m_texture.Height));
        }


        private static List<PointCloud> CreatePointCloudBetween0and100()
        {
            var pc = new List<PointCloud>()
            {
                new PointCloud()
                {
                    Id = "comparison-1", Shift = Vector2.Zero,
                    PointsInOrigin = new List<Point>()
                    {
                        new Point(10, 10),
                        new Point(20, 40)
                    }
                },
                new PointCloud()
                {
                    Id = "comparison-2", Shift = Vector2.Zero,
                    PointsInOrigin = new List<Point>()
                    {
                        new Point(30, 55),
                        new Point(42, 88)
                    }
                },
                new PointCloud()
                {
                    Id = "comparison-3", Shift = Vector2.Zero,
                    PointsInOrigin = new List<Point>()
                    {
                        new Point(2, 56),
                        new Point(3, 67)
                    }
                },
            };
            return pc;
        }
    }
}
