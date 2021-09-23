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

        

        [TestCase(1, 1, 0, 1)]
        [TestCase(10, 10, 0, 100)]
        [TestCase(10, 10, 1, 64)]
        public void PointCloudSize(int width, int height, int padding, int numPoints)
        {
            var collider = new RectangleCollider(new Rectangle(0, 0, width, height), padding);
            Assert.AreEqual(numPoints, collider.GetPointCloud().PointsInOrigin.Count);
        }

        [Test]
        public void Padding()
        {
            // While padding, the resulting rect is expected to be larger than with padding = 0 (default)
            var rectWithoutPadding = new Rectangle(2, 2, 8, 8);            
            var collider = new RectangleCollider(rectWithoutPadding, 2);
            var alignedPoints = collider.GetPointCloud();
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
            Assert.Throws<System.ArgumentException>(() => new RectangleCollider(rectWithoutPadding, rectWithoutPadding.Width));
        }

        [Test]
        public void TextureInitialization()
        {
            var collider = new RectangleCollider(m_texture);
            var alignedPoints = collider.GetPointCloud();
            Assert.IsEmpty(alignedPoints.PointsInOrigin.Where(p => p.X < 0));
            Assert.IsEmpty(alignedPoints.PointsInOrigin.Where(p => p.X > m_texture.Width));
            Assert.IsEmpty(alignedPoints.PointsInOrigin.Where(p => p.Y < 0));
            Assert.IsEmpty(alignedPoints.PointsInOrigin.Where(p => p.Y > m_texture.Height));
        }


        
    }
}
