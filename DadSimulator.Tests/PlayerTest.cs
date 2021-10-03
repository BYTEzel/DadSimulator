using DadSimulator.Collider;
using DadSimulator.GraphicObjects;
using DadSimulator.IO;
using Microsoft.Xna.Framework.Graphics;
using NUnit.Framework;
using System.Collections.Generic;

namespace DadSimulator.Tests
{
    class MovementInput : IUserCommand
    {
        public List<Directions> CurrentDirections;

        public char GetActionKey()
        {
            return 'e';
        }

        public List<Directions> GetDirections()
        {
            return CurrentDirections;
        }

        public bool IsActionKeyPressed()
        {
            return false;
        }
    }

    public class PlayerTest
    {
        private Texture2D m_texture;

        private const int m_textureWidth = 10;
        private const int m_textureHeight = 10;

        [SetUp]
        public void Setup()
        {
            m_texture = GraphicsLoader.LoadTemplate(Templates.Test);
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

            var objTopLeft = new Player(m_texture, startPosition, new MovementInput(), null, null, null);
            Assert.AreEqual(0, objTopLeft.Position.X);
            Assert.AreEqual(0, objTopLeft.Position.Y);
        }

        [Test]
        public void Move()
        {
            var movement = new MovementInput();
            var obj = new Player(m_texture, Microsoft.Xna.Framework.Vector2.Zero, movement, null, null, null);

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