using DadSimulator.Collider;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace DadSimulator.GraphicObjects
{
    public class MovingObject : StationaryObject
    {
        private readonly float m_speed;
        private IMovementCommand m_movement;
        private ICollidableCollection m_collidableCollection;

        public MovingObject(string name, Texture2D texture2D, Vector2 startPosition, RelativePositionReference relativePosition, 
            float speed, IMovementCommand movement, ICollidableCollection collidableCollection, ICollider collider = null) 
            : base(name, texture2D, startPosition, relativePosition, collider)
        {
            m_speed = speed;
            m_movement = movement;
            m_collidableCollection = collidableCollection;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(double elapsedTime)
        {
            var movements = m_movement.GetDirections();
            var estimatedShift = new Vector2(Position.X, Position.Y);
            var allColliders = m_collidableCollection.GetCollectibleList().Where(x => x.GetName() != m_name);

            foreach (var mov in movements)
            {
                estimatedShift = ComputeEstimatedShift(elapsedTime, estimatedShift, mov);
                estimatedShift = CheckCollisionsWithEstimatedShiftAndCorrect(estimatedShift, allColliders, mov);
            }

            Position = estimatedShift;
            base.Update(elapsedTime);
        }

        private Vector2 CheckCollisionsWithEstimatedShiftAndCorrect(Vector2 estimatedShift, System.Collections.Generic.IEnumerable<ICollidable> allColliders, Directions mov)
        {
            foreach (var collider in allColliders)
            {
                var estimatedAlignedPointCloud = new AlignedPointCloud() { PointCloud = m_alignedPointCloud.PointCloud, Shift = estimatedShift };
                var intersectResult = Collision.Intersection(estimatedAlignedPointCloud, collider.GetAlignedPointCloud());
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
                    estimatedShift.Y += intersectResult.AlignedBoundingBox.Rectangle.Height + 1;
                    break;
                case Directions.Right:
                    estimatedShift.X -= intersectResult.AlignedBoundingBox.Rectangle.Width + 1;
                    break;
                case Directions.Down:
                    estimatedShift.Y -= intersectResult.AlignedBoundingBox.Rectangle.Height + 1;
                    break;
                case Directions.Left:
                    estimatedShift.X += intersectResult.AlignedBoundingBox.Rectangle.Width + 1;
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
