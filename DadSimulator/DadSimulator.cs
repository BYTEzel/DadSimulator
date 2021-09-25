using DadSimulator.Collider;
using DadSimulator.GraphicObjects;
using DadSimulator.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace DadSimulator
{
    public class DadSimulator : Game, ITemplateLoader, ICollidableCollection
    {
        private GraphicsDeviceManager m_graphics;
        private SpriteBatch m_spriteBatch;
        private Player m_player;
        private LevelBackground m_levelBackground;
        private List<IGraphicObject> m_gameObjects;
        private List<ICollidable> m_collidableObjects;

        public DadSimulator()
        {
            InitGraphics();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            m_gameObjects = new List<IGraphicObject>();
            m_collidableObjects = new List<ICollidable>();
        }

        private void InitGraphics()
        {
            m_graphics = new GraphicsDeviceManager(this);
            m_graphics.IsFullScreen = true;
            m_graphics.PreferredBackBufferWidth = 1280;
            m_graphics.PreferredBackBufferHeight = 720;
            m_graphics.ApplyChanges();
        }

        protected override void Initialize()
        {            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(GraphicsDevice);
            m_player = new Player("player", Content.Load<Texture2D>("Smiley"), new Vector2(200, 200), 100f, new KeyboardMovement(), this);
            m_levelBackground = new LevelBackground("obstacle", Content.Load<Texture2D>("Test/test-blank"), new Vector2(500, 500));

            m_gameObjects.Add(m_levelBackground);
            m_gameObjects.Add(m_player);

            m_collidableObjects.Add(m_player);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var gameSeconds = gameTime.ElapsedGameTime.TotalSeconds;
            foreach (var gameObj in m_gameObjects)
            {
                gameObj.Update(gameSeconds);
            }

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            m_spriteBatch.Begin();
            foreach (var gameObj in m_gameObjects)
            {
                gameObj.Draw(m_spriteBatch);
            }
            m_spriteBatch.End();
            

            base.Draw(gameTime);
        }

        public Texture2D LoadTemplate(Templates name)
        {
            if (m_graphics?.GraphicsDevice == null)
            {
                Initialize();
            }

            string stringName = string.Empty;
            switch (name)
            {
                case Templates.Character:
                    stringName = "Smiley";
                    break;
                case Templates.Test:
                    stringName = "Test/test-blank";
                    break;
                default:
                    break;
            }

            return Content.Load<Texture2D>(stringName);
        }

        public List<ICollidable> GetCollectibleList()
        {
            return m_collidableObjects;
        }
    }
}
