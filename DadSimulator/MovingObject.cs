using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DadSimulator
{
    class MovingObject : StationaryObject
    {
        private readonly float m_speed;
        private IMovementCommand m_movement;

        public MovingObject(Texture2D texture2D, Vector2 startPosition, RelativePosition relativePosition, 
            float speed, IMovementCommand movement) 
            : base(texture2D, startPosition, relativePosition)
        {
            m_speed = speed;
            m_movement = movement;
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
