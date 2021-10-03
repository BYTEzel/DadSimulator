using DadSimulator.Collider;
using DadSimulator.Interactable;
using DadSimulator.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DadSimulator.GraphicObjects
{
    public class Player : IGraphicObject
    {
        private const float m_speed = 100f;
        private const float m_interactionRadius = 20f;
        public Vector2 Position { get; private set; }
        private readonly IUserCommand m_movement;
        
        private readonly Texture2D m_texture;

        private readonly ICollider m_collider;

        private readonly ICollisionChecker m_collisionChecker;
        private readonly IInteractableCollection m_interactableCollection;

        private readonly IUiEngine m_ui;

        private readonly char m_actionKey;

        internal struct UiRectInteractable
        {
            public int XPosition;
            public Color RectColor;
            public string TextHeadline;
            public string TextBox;
        }

        private List<UiRectInteractable> m_uiRectsToDraw;


        public Player(Texture2D texture2D, Vector2 startPosition, 
            IUserCommand movement, ICollisionChecker collisionChecker, 
            IInteractableCollection interactableCollection, IUiEngine gui)
        {
            m_texture = texture2D;
            Position = startPosition;
            m_movement = movement;
            m_collisionChecker = collisionChecker;
            m_interactableCollection = interactableCollection;
            m_collider = new RectangleCollider(texture2D);
            m_ui = gui;
            m_uiRectsToDraw = new List<UiRectInteractable>();
            m_actionKey = m_movement.GetActionKey();
        }

        public void Initialize()
        {
        }

        public void Update(double elapsedTime)
        {
            m_uiRectsToDraw.Clear();

            var movements = m_movement.GetDirections();
            if (movements.Count > 0)
            {
                HandleCollisions(elapsedTime, movements);
            }

            if (m_interactableCollection != null)
            {
                var allInteractable = m_interactableCollection.GetInteractables();
                foreach (var interactable in allInteractable)
                {
                    var interactablePosition = interactable.GetLocation();
                    if (m_interactionRadius >= Vector2.Distance(Position, interactablePosition))
                    {
                        var name = interactable.GetName();
                        var text = $"State: {interactable.GetState()}";
                        var command = interactable.GetCommand();
                        if (!string.IsNullOrEmpty(command))
                        {
                            text += $"\n[{m_actionKey}] to {command}";
                        }
                        m_uiRectsToDraw.Add(new UiRectInteractable()
                        {
                            XPosition = (int)interactablePosition.X,
                            RectColor = Color.Black,
                            TextHeadline = name,
                            TextBox = text
                        });

                        if (m_movement.IsActionKeyPressed())
                        {
                            interactable.ExecuteCommand();
                        }
                    }
                }
            }
        }

        private void HandleCollisions(double elapsedTime, List<Directions> movements)
        {
            foreach (var mov in movements)
            {
                var deltaMovement = ComputeEstimatedShift(elapsedTime, mov);
                deltaMovement = CheckCollisionsWithEstimatedShiftAndCorrect(deltaMovement, mov);
                Position += deltaMovement;
            }
        }

        private Vector2 CheckCollisionsWithEstimatedShiftAndCorrect(Vector2 deltaMovement, Directions mov)
        {
            var correctedDelta = deltaMovement;

            if (m_collisionChecker != null)
            {
                var newPosition = Position + correctedDelta;

                var apc = new AlignedPointCloud()
                {
                    PointCloud = new PointCloud() { PointsInOrigin = m_collider.GetPointCloud().PointsInOrigin },
                    Shift = newPosition
                };

                if (m_collisionChecker.IsColliding(apc))
                {
                    correctedDelta = CorrectEstimatedShift(correctedDelta, mov);
                }
            }
            return correctedDelta;
        }

        private static Vector2 CorrectEstimatedShift(Vector2 deltaMovement, Directions mov)
        {
            switch (mov)
            {
                case Directions.Up:
                case Directions.Down:
                    deltaMovement.Y = 0;
                    break;
                case Directions.Right:
                case Directions.Left:
                    deltaMovement.X = 0;
                    break;
                default:
                    break;
            }

            return deltaMovement;
        }

        private Vector2 ComputeEstimatedShift(double elapsedTime, Directions mov)
        {
            var delta = Vector2.Zero;
            switch (mov)
            {
                case Directions.Up:
                    delta.Y = - m_speed * (float)elapsedTime;
                    break;
                case Directions.Right:
                    delta.X = m_speed * (float)elapsedTime;
                    break;
                case Directions.Down:
                    delta.Y = m_speed * (float)elapsedTime;
                    break;
                case Directions.Left:
                    delta.X = - m_speed * (float)elapsedTime;
                    break;
                default:
                    break;
            }

            return delta;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(m_texture, Position, null, Color.Red);
            foreach (var boxParams in m_uiRectsToDraw)
            {
                m_ui.DrawRectangleInteractable(
                    boxParams.XPosition,
                    boxParams.RectColor,
                    boxParams.TextHeadline,
                    boxParams.TextBox
                    );
            }

        }

    }
}
