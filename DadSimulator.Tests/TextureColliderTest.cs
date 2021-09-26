using DadSimulator.Collider;
using NUnit.Framework;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using DadSimulator.IO;
using DadSimulator.Collider;
using Microsoft.Xna.Framework.Graphics;

namespace DadSimulator.Tests
{
    public class TextureColliderTest
    {
        private Color[,] m_textureWithTransparency, m_textureWithoutTransparency;

        [SetUp]
        public void Setup()
        {
            using (var sim = new DadSimulator())
            {
                sim.RunOneFrame();
                ITemplateLoader loader = sim;
                m_textureWithTransparency = loader.LoadTemplateContent(Templates.TestTextureTransparency);
                m_textureWithoutTransparency = loader.LoadTemplateContent(Templates.Test);
            }
        }

        [Test]
        public void TransparencyHandling()
        {
            var collider = new TextureCollider(m_textureWithTransparency);
            var pc = collider.GetPointCloud();
            Assert.Less(pc.PointsInOrigin.Count, m_textureWithTransparency.GetLength(0) * m_textureWithTransparency.GetLength(1));
            Assert.Greater(pc.PointsInOrigin.Count, 0);
        }

        [Test]
        public void ImageWithoutTransparency()
        {
            var collider = new TextureCollider(m_textureWithoutTransparency);
            Assert.AreEqual(
                m_textureWithoutTransparency.GetLength(0) * m_textureWithoutTransparency.GetLength(1), 
                collider.GetPointCloud().PointsInOrigin.Count);
        }


        
    }
}
