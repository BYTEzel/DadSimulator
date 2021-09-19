using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DadSimulator
{
    public class DadSimulator : Game
    {
        private GraphicsDeviceManager m_graphics;
        private SpriteBatch m_spriteBatch;
        private Texture2D m_char;
        private Vector2 m_charPosition;
        private float m_speed;

        public DadSimulator()
        {
            m_graphics = new GraphicsDeviceManager(this);
            m_graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            m_charPosition = new Vector2(
                m_graphics.PreferredBackBufferWidth / 2,
                m_graphics.PreferredBackBufferHeight / 2);
            m_speed = 100f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(GraphicsDevice);

            m_char = Content.Load<Texture2D>("Smiley");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up))
                m_charPosition.Y -= m_speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Down))
                m_charPosition.Y += m_speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Left))
                m_charPosition.X -= m_speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Right))
                m_charPosition.X += m_speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            m_spriteBatch.Begin();
            m_spriteBatch.Draw(m_char, m_charPosition, null, 
                Color.White, 0f, new Vector2(m_char.Width / 2, m_char.Height / 2), 
                Vector2.One, SpriteEffects.None, 0f); ;
            m_spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
