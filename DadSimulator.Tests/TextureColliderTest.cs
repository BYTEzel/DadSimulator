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
        private Color[,] m_texture;

        [SetUp]
        public void Setup()
        {
            using (var sim = new DadSimulator())
            {
                sim.RunOneFrame();
                ITemplateLoader loader = sim;
                m_texture = loader.LoadTemplateContent(Templates.TestTextureTransparency);
            }
        }

        [Test]
        public void TransparencyHandling()
        {
            var collider = new TextureCollider(m_texture);
            var pc = collider.GetPointCloud();
            Assert.Less(pc.PointsInOrigin.Count, m_texture.GetLength(0) * m_texture.GetLength(1));
            Assert.Greater(0, pc.PointsInOrigin.Count);
        }


        
    }
}
