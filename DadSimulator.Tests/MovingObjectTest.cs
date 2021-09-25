using DadSimulator.Collider;
using DadSimulator.GraphicObjects;
using DadSimulator.IO;
using Microsoft.Xna.Framework.Graphics;
using NUnit.Framework;
using System.Collections.Generic;

namespace DadSimulator.Tests
{
    class MovementInput : IMovementCommand
    {
        public List<Directions> CurrentDirections;
        public List<Directions> GetDirections()
        {
            return CurrentDirections;
        }
    }

    class GameObjects : ICollidableCollection
    {
        public List<ICollidable> GetCollectibleList()
        {
            return new List<ICollidable>();
        }
    }

    public class MovingObjectTest
    {
        private Texture2D m_texture;

        private const int m_textureWidth = 10;
        private const int m_textureHeight = 10;

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
        public void TestLoader()
        {
            Assert.AreEqual(m_textureWidth, m_texture.Width);
            Assert.AreEqual(m_textureHeight, m_texture.Height);
        }

        [Test]
        public void StartPosition()
        {
            var startPosition = Microsoft.Xna.Framework.Vector2.Zero;
            var objCentered = new MovingObject("centered", m_texture, startPosition, RelativePositionReference.Centered, 100f, null, new GameObjects());
            Assert.AreEqual(m_textureHeight / 2, objCentered.RelativePosition.X);
            Assert.AreEqual(m_textureWidth / 2, objCentered.RelativePosition.Y);

            var objTopLeft = new MovingObject("top-left", m_texture, startPosition, RelativePositionReference.TopLeft, 100f, null, new GameObjects());
            Assert.AreEqual(0, objTopLeft.RelativePosition.X);
            Assert.AreEqual(0, objTopLeft.RelativePosition.Y);

            Assert.AreEqual(objCentered.Position, objTopLeft.Position);
        }

        [Test]
        public void Move()
        {
            var movement = new MovementInput();
            var obj = new MovingObject("obj", m_texture, Microsoft.Xna.Framework.Vector2.Zero, RelativePositionReference.Centered, 100f, movement, new GameObjects());

            UpdateDirection(ref obj, movement, new List<Directions> { Directions.Up });
            Assert.AreEqual(0, obj.Position.X);
            Assert.Greater(0, obj.Position.Y);

            UpdateDirection(ref obj, movement, new List<Directions>{ Directions.Down });
            Assert.AreEqual(0, obj.Position.X);
            Assert.AreEqual(0, obj.Position.Y);

            UpdateDirection(ref obj, movement, new List<Directions> { Directions.Right });
            Assert.Less(0, obj.Position.X);
            Assert.AreEqual(0, obj.Position.Y);

            UpdateDirection(ref obj, movement, new List<Directions> { Directions.Left });
            Assert.AreEqual(0, obj.Position.X);
            Assert.AreEqual(0, obj.Position.Y);

            UpdateDirection(ref obj, movement, new List<Directions> { Directions.Down, Directions.Up, Directions.Right, Directions.Left });
            Assert.AreEqual(0, obj.Position.X);
            Assert.AreEqual(0, obj.Position.Y);

        }

        void UpdateDirection(ref Player obj, MovementInput movement, List<Directions> directions)
        {
            movement.CurrentDirections = directions;
            obj.Update(1);
        }
    }
}