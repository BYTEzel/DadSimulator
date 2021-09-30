using DadSimulator.Collider;
using DadSimulator.Interactable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace DadSimulator.GraphicObjects
{
    public class Player : IGraphicObject
    {
        private const float m_speed = 100f;
        public Vector2 Position { get; private set; }
        private IMovementCommand m_movement;
        
        private Texture2D m_texture;

        private ICollider m_collider;

        private ICollisionChecker m_collisionChecker;
        private IInteractableCollection m_interactableCollection;

        public Player(Texture2D texture2D, Vector2 startPosition, 
            IMovementCommand movement, ICollisionChecker collisionChecker, 
            IInteractableCollection interactableCollection)
        {
            m_texture = texture2D;
            Position = startPosition;
            m_movement = movement;
            m_collisionChecker = collisionChecker;
            m_interactableCollection = interactableCollection;
            m_collider = new RectangleCollider(texture2D);
        }

        public void Initialize()
        {
        }

        public void Update(double elapsedTime)
        {
            var movements = m_movement.GetDirections();
            if (movements.Count > 0)
            {
                HandleCollisions(elapsedTime, movements);
                if (m_interactableCollection != null)
                {
                    var allInteractable = m_interactableCollection.GetInteractables();
                    foreach (var interactable in allInteractable)
                    {
                        /*
                            var intersectResult = Collision.Intersection(m_alignedPointCloud, interactable.GetInteractableAlignedPointCloud());
                            if (IntersectionType.Equal == intersectResult.Type || IntersectionType.Intersection == intersectResult.Type)
                            {
                                var name = interactable.GetName();
                                var state = interactable.GetState();
                            }
                        */
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
        }

    }
}
