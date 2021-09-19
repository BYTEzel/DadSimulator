using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DadSimulator
{
    class MovingObject : StationaryObject
    {
        private float m_speed;

        public MovingObject(Texture2D texture2D, Vector2 startPosition, RelativePosition relativePosition, float speed) 
            : base(texture2D, startPosition, relativePosition)
        {
            m_speed = speed;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(double elapsedTime)
        {
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up))
                m_position.Y -= m_speed * (float)elapsedTime;

            if (kstate.IsKeyDown(Keys.Down))
                m_position.Y += m_speed * (float)elapsedTime;

            if (kstate.IsKeyDown(Keys.Left))
                m_position.X -= m_speed * (float)elapsedTime;

            if (kstate.IsKeyDown(Keys.Right))
                m_position.X += m_speed * (float)elapsedTime;

            base.Update(elapsedTime);
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }
    }
}
