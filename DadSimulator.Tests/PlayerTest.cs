using DadSimulator.Animation;
using DadSimulator.Collider;
using DadSimulator.GraphicObjects;
using DadSimulator.Interactable;
using DadSimulator.IO;
using DadSimulator.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NUnit.Framework;
using System.Collections.Generic;

namespace DadSimulator.Tests
{
    class MovementInput : IUserCommand
    {
        public List<Directions> CurrentDirections;
        private bool m_actionKeyPressed;

        public MovementInput()
        {
            m_actionKeyPressed = false;
            CurrentDirections = new List<Directions>();
        }

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
            return m_actionKeyPressed;
        }

        public void SetActionKey(bool isPressed)
        {
            m_actionKeyPressed = isPressed;
        }
    }

    class InteractableTest : IInteractable
    {
        public bool IsCommandExecuted { get; private set; }
        public bool IsStateNameCommandCalled { get; private set; }

        private Vector2 m_position;

        public InteractableTest(Vector2 position)
        {
            m_position = position;
            IsStateNameCommandCalled = false;
            IsCommandExecuted = false;
        }

        public void ExecuteCommand()
        {
            IsCommandExecuted = true;
        }

        public string GetCommand()
        {
            IsStateNameCommandCalled = true;
            return "Test Command";
        }

        public string GetName()
        {
            IsStateNameCommandCalled = true;
            return "Test Interactable";
        }

        public Vector2 GetPosition()
        {
            return m_position;
        }

        public string GetState()
        {
            IsStateNameCommandCalled = true;
            return "Test State";
        }
    }

    class UiTest : IUiEngine
    {
        public bool DrawLineCalled { get; private set; }
        public bool DrawRectangleCalled { get; private set; }
        public bool DrawRectangleInteractableCalled { get; private set; }

        public UiTest()
        {
            DrawLineCalled = false;
            DrawRectangleCalled = false;
            DrawRectangleInteractableCalled = false;
        }

        public void DrawLine(Vector2 point1, Vector2 point2, float thickness, Color color)
        {

            DrawLineCalled = true;
        }

        public void DrawRectangle(Rectangle rect, Color color)
        {
            DrawRectangleCalled = true;
        }

        public void DrawRectangleInteractable(Vector2 positionInteractable, RelativePosition relativePosition, Color color, string headline, string textInBox)
        {
            DrawRectangleInteractableCalled = true;
        }

        public void DrawText(Vector2 positionTopLeft, Color color, string text, bool isHeadline, float scaling = 1)
        {
        }

        public void DrawRectangleInteractable(Vector2 positionInteractable, RelativePosition relativePosition, string headline, string textInBox, Color colorBox, Color colorBorder, int borderSize = 2)
        {
            DrawRectangleInteractableCalled = true;
        }
    }

    class InteractableList : IInteractableCollection
    {
        public List<IInteractable> Interactables;

        public InteractableList()
        {
            Interactables = new List<IInteractable>();
        }

        public List<IInteractable> GetInteractables()
        {
            return Interactables;
        }
    }


    class SpritesheetTest : ISpritesheet
    {
        public Color Color { get; set; }
        public float FPS { get; set; }
        public Vector2 Position { get; set; }
        public SpritesheetTest()
        {
            Color = Color.White;
            FPS = 1.0f;
            Position = Vector2.Zero;
        }

        public void Draw(SpriteBatch batch)
        {
        }

        public void Initialize()
        {
        }

        public void SetAnimation(string animationName)
        {
        }

        public void Update(double elapsedTime)
        {
        }
    }


    public class PlayerTest
    {
        private Texture2D m_texture;
        private ISpritesheet m_spritesheet;
        private ICollider m_collider;

        private const int m_textureWidth = 10;
        private const int m_textureHeight = 10;

        [SetUp]
        public void Setup()
        {
            m_texture = GraphicsLoader.LoadTemplate(Templates.Test);
            m_spritesheet = new SpritesheetTest();
            m_collider = new RectangleCollider(m_texture);
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
            var startPosition = Vector2.Zero;

            var objTopLeft = new Player(m_spritesheet, m_collider, startPosition, new MovementInput(), null, null, null);
            Assert.AreEqual(0, objTopLeft.Position.X);
            Assert.AreEqual(0, objTopLeft.Position.Y);
        }

        [Test]
        public void Move()
        {
            var movement = new MovementInput();
            var obj = new Player(m_spritesheet, m_collider, Vector2.Zero, movement, null, null, null);

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

        [Test]
        public void Interactable()
        {
            var startPositionPlayer = Vector2.Zero;
            var interactionPosition = new Vector2(100, 0);

            var movement = new MovementInput();
            var collisionChecker = new CollidableMap(new Misc.Size(10, 10));
            var interactable = new InteractableTest(interactionPosition);
            var interactableCollection = new InteractableList();
            interactableCollection.Interactables.Add(interactable); 
            var ui = new UiTest();

            var player = new Player(m_spritesheet, m_collider, startPositionPlayer, movement, collisionChecker, interactableCollection, ui);

            // If no action button is pressed, nothing should happen
            player.Update(0);
            Assert.IsFalse(interactable.IsCommandExecuted);
            Assert.IsFalse(interactable.IsStateNameCommandCalled);
            Assert.AreEqual(player.Position, startPositionPlayer);

            movement.CurrentDirections.Add(Directions.Right);
            for (int i = 1; i < (int)interactionPosition.X; i++)
            {
                player.Update(i);
                if (interactable.IsStateNameCommandCalled)
                {
                    // Break when the position is close enough to interact with the object
                    break;
                }
            }
            
            // When the object is close, information should be requested
            Assert.IsTrue(interactable.IsStateNameCommandCalled);
            Assert.IsFalse(interactable.IsCommandExecuted); // No action key is pressed -> no command is executed

            // When an action key is pressed, the interaction should take place
            movement.CurrentDirections = new List<Directions>();
            movement.SetActionKey(true);
            player.Update(interactionPosition.X);
            Assert.IsTrue(interactable.IsCommandExecuted);
        }

        void UpdateDirection(ref Player obj, MovementInput movement, List<Directions> directions)
        {
            movement.CurrentDirections = directions;
            obj.Update(1);
        }
    }
}