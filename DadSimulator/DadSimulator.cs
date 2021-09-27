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
        //private Player m_player;
        //private LevelBackground m_levelBackground;
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
            //m_graphics.IsFullScreen = true;
            m_graphics.PreferredBackBufferWidth = 1920;
            m_graphics.PreferredBackBufferHeight = 1280;
            m_graphics.ApplyChanges();
        }

        protected override void Initialize()
        {            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(GraphicsDevice);
            var player = new Player("player", LoadTemplate(Templates.Character), new Vector2(200, 200), 100f, 
                new KeyboardMovement(), this, new TextureCollider(LoadTemplateContent(Templates.Character)));
            //var player = new Player("player", LoadTemplate(Templates.Test), new Vector2(200, 200), 100f,
            //    new KeyboardMovement(), this, new RectangleCollider(LoadTemplate(Templates.Test)));
            var levelBackground = new LevelBackground("obstacle", LoadTemplate(Templates.Test), new Vector2(500, 500));
            var levelObstacle = new LevelBounds("bounds", LoadTemplate(Templates.Test), new Vector2(400, 400), 
                new RectangleCollider(LoadTemplate(Templates.Test)));

            m_gameObjects.Add(levelBackground);
            m_gameObjects.Add(levelObstacle);
            m_gameObjects.Add(player);

            m_collidableObjects.Add(levelObstacle);
            m_collidableObjects.Add(player);

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
                case Templates.TestTextureTransparency:
                    stringName = "Test/smiley-test";
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

        public Color[,] LoadTemplateContent(Templates name)
        {
            var texture = LoadTemplate(name);
            Color[] colors1D = new Color[texture.Width * texture.Height];
            texture.GetData(colors1D);
            var colors2D = new Color[texture.Width, texture.Height];
            for (int row = 0; row < texture.Height; row++)
            {
                for (int column = 0; column < texture.Width; column++)
                {
                    // Assumes row major ordering of the array.
                    colors2D[row, column] = colors1D[row * texture.Width + column];
                }
            }
            return colors2D;
        }
    }
}
