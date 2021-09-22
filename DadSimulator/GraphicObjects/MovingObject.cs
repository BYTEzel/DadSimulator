using DadSimulator.Collider;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DadSimulator.GraphicObjects
{
    public class MovingObject : StationaryObject
    {
        private readonly float m_speed;
        private IMovementCommand m_movement;
        

        public MovingObject(Texture2D texture2D, Vector2 startPosition, RelativePositionReference relativePosition, 
            float speed, IMovementCommand movement, ICollider collider = null) 
            : base(texture2D, startPosition, relativePosition, collider)
        {
            m_speed = speed;
            m_movement = movement;
            m_collider = collider;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(double elapsedTime)
        {
            var movements = m_movement.GetDirections();
            
            foreach (var mov in movements)
            {
                switch (mov)
                {
                    case Directions.Up:
                        m_position.Y -= m_speed * (float)elapsedTime;
                        break;
                    case Directions.Right:
                        m_position.X += m_speed * (float)elapsedTime;
                        break;
                    case Directions.Down:
                        m_position.Y += m_speed * (float)elapsedTime;
                        break;
                    case Directions.Left:
                        m_position.X -= m_speed * (float)elapsedTime;
                        break;
                    default:
                        break;
                }
            }

            base.Update(elapsedTime);
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }
    }
}
