using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DadSimulator
{
    public class DadSimulator : Game
    {
        private GraphicsDeviceManager m_graphics;
        private SpriteBatch m_spriteBatch;
        private MovingObject m_player;

        public DadSimulator()
        {
            m_graphics = new GraphicsDeviceManager(this);
            m_graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(GraphicsDevice);
            m_player = new MovingObject(Content.Load<Texture2D>("Smiley"), new Vector2(200, 200), RelativePosition.Centered, 100f);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            m_player.Update(gameTime.ElapsedGameTime.TotalSeconds);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            m_spriteBatch.Begin();
            m_player.Draw(m_spriteBatch);
            m_spriteBatch.End();
            

            base.Draw(gameTime);
        }
    }
}
