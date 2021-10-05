using DadSimulator.Collider;
using DadSimulator.Misc;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DadSimulator.GraphicObjects
{
    class PlayerMovement : IPosition
    {
        public float Speed;
        public Vector2 Position { get; private set; }


        private readonly IUserCommand m_movement;
        private readonly ICollider m_collider;
        private readonly ICollisionChecker m_collisionChecker;


        public PlayerMovement(Vector2 startPos, IUserCommand movement, ICollider collider, ICollisionChecker checker)
        {
            Speed = 100f;
            Position = startPos;
            m_movement = movement;
            m_collider = collider;
            m_collisionChecker = checker;
        }

        public Vector2 GetPosition()
        {
            return Position;
        }

        public void MovePlayer(double elapsedTime)
        {
            var movements = m_movement.GetDirections();
            if (movements.Count > 0)
            {
                HandleCollisions(elapsedTime, movements);
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

        private Vector2 ComputeEstimatedShift(double elapsedTime, Directions mov)
        {
            var delta = Vector2.Zero;
            switch (mov)
            {
                case Directions.Up:
                    delta.Y = -Speed * (float)elapsedTime;
                    break;
                case Directions.Right:
                    delta.X = Speed * (float)elapsedTime;
                    break;
                case Directions.Down:
                    delta.Y = Speed * (float)elapsedTime;
                    break;
                case Directions.Left:
                    delta.X = -Speed * (float)elapsedTime;
                    break;
                default:
                    break;
            }

            return delta;
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

    }
}
