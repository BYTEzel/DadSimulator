using DadSimulator.Collider;
using DadSimulator.Interactable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace DadSimulator.GraphicObjects
{
    public class Player : LevelBounds
    {
        private readonly float m_speed;
        private IMovementCommand m_movement;
        private ICollidableCollection m_collidableCollection;
        private IInteractableCollection m_interactableCollection;

        public Player(string name, Texture2D texture2D, Vector2 startPosition, 
            float speed, IMovementCommand movement, ICollidableCollection collidableCollection, 
            IInteractableCollection interactableCollection, ICollider collider)
            : base(name, texture2D, startPosition, collider)
        {
            m_speed = speed;
            m_movement = movement;
            m_collidableCollection = collidableCollection;
            m_interactableCollection = interactableCollection;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(double elapsedTime)
        {
            var movements = m_movement.GetDirections();
            if (movements.Count > 0)
            {
                HandleCollisions(elapsedTime, movements);
                var allInteractable = m_interactableCollection.GetInteractables();
                foreach (var interactable in allInteractable)
                {
                    var intersectResult = Collision.Intersection(m_alignedPointCloud, interactable.GetInteractableAlignedPointCloud());
                /*
                    if (IntersectionType.Equal == intersectResult.Type || IntersectionType.Intersection == intersectResult.Type)
                    {
                        var name = interactable.GetName();
                        var state = interactable.GetState();
                    }
                */
                }
            }
            base.Update(elapsedTime);
        }

        private void HandleCollisions(double elapsedTime, System.Collections.Generic.List<Directions> movements)
        {
            var estimatedShift = new Vector2(Position.X, Position.Y);
            var allColliders = m_collidableCollection.GetCollectibleList().Where(x => x.GetName() != m_name);

            foreach (var mov in movements)
            {
                estimatedShift = ComputeEstimatedShift(elapsedTime, estimatedShift, mov);
                estimatedShift = CheckCollisionsWithEstimatedShiftAndCorrect(estimatedShift, allColliders, mov);
            }

            Position = estimatedShift;
        }

        private Vector2 CheckCollisionsWithEstimatedShiftAndCorrect(Vector2 estimatedShift, System.Collections.Generic.IEnumerable<ICollidable> allColliders, Directions mov)
        {
            foreach (var collider in allColliders)
            {
                var estimatedAlignedPointCloud = new AlignedPointCloud() { PointCloud = m_alignedPointCloud.PointCloud, Shift = estimatedShift };
                var intersectResult = Collision.Intersection(estimatedAlignedPointCloud, collider.GetColliderAlignedPointCloud());
                if (IntersectionType.Equal == intersectResult.Type || IntersectionType.Intersection == intersectResult.Type)
                {
                    estimatedShift = CorrectEstimatedShift(estimatedShift, mov, intersectResult);
                }
            }

            return estimatedShift;
        }

        private static Vector2 CorrectEstimatedShift(Vector2 estimatedShift, Directions mov, IntersectionResult intersectResult)
        {
            switch (mov)
            {
                case Directions.Up:
                    estimatedShift.Y = (int)estimatedShift.Y + intersectResult.AlignedBoundingBox.Rectangle.Height + 1;
                    break;
                case Directions.Right:
                    estimatedShift.X = (int)estimatedShift.X - (intersectResult.AlignedBoundingBox.Rectangle.Width + 1);
                    break;
                case Directions.Down:
                    estimatedShift.Y = (int)estimatedShift.Y - (intersectResult.AlignedBoundingBox.Rectangle.Height + 1);
                    break;
                case Directions.Left:
                    estimatedShift.X = (int)estimatedShift.X + intersectResult.AlignedBoundingBox.Rectangle.Width + 1;
                    break;
                default:
                    break;
            }

            return estimatedShift;
        }

        private Vector2 ComputeEstimatedShift(double elapsedTime, Vector2 estimatedShift, Directions mov)
        {
            switch (mov)
            {
                case Directions.Up:
                    estimatedShift.Y -= m_speed * (float)elapsedTime;
                    break;
                case Directions.Right:
                    estimatedShift.X += m_speed * (float)elapsedTime;
                    break;
                case Directions.Down:
                    estimatedShift.Y += m_speed * (float)elapsedTime;
                    break;
                case Directions.Left:
                    estimatedShift.X -= m_speed * (float)elapsedTime;
                    break;
                default:
                    break;
            }

            return estimatedShift;
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }
    }
}
