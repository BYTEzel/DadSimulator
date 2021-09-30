using DadSimulator.Collider;
using DadSimulator.GraphicObjects;
using DadSimulator.Interactable;
using DadSimulator.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace DadSimulator
{
    public class DadSimulator : Game, ITemplateLoader, IInteractableCollection
    {
        private GraphicsDeviceManager m_graphics;
        private SpriteBatch m_spriteBatch;
        private List<IGraphicObject> m_gameObjects;
        private List<IInteractable> m_interactables;

        public DadSimulator()
        {
            InitGraphics();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            m_gameObjects = new List<IGraphicObject>();
            m_interactables = new List<IInteractable>();
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
            var collisionMap = new CollidableMap(new Size() { Height = 480, Width = 640 });

            var levelBounds = new LevelBounds(this, Templates.LevelWalls, new Vector2(0, 0), collisionMap);
                
            var player = new Player(LoadTemplate(Templates.Character), new Vector2(200, 200), 
                new KeyboardMovement(), collisionMap, this); 
                
            var washMaschine = new WashingMachine(LoadTemplate(Templates.Test), new Vector2(200, 50),
                new RectangleCollider(LoadTemplate(Templates.Test)),
                new RectangleCollider(new Rectangle(150, 0, 300, 300)));

            m_gameObjects.Add(levelBounds);
            m_gameObjects.Add(washMaschine);
            m_gameObjects.Add(player);


            m_interactables.Add(washMaschine);
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
                    stringName = "Test/block";
                    break;
                case Templates.Test:
                    stringName = "Test/block";
                    break;
                case Templates.TestTextureTransparency:
                    stringName = "Test/collider-test";
                    break;
                case Templates.LevelWalls:
                    stringName = "level-bounds";
                    break;
                default:
                    break;
            }

            return Content.Load<Texture2D>(stringName);
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
                    colors2D[column, row] = colors1D[row * texture.Width + column];
                }
            }
            return colors2D;
        }

        public List<IInteractable> GetInteractables()
        {
            return m_interactables;
        }
    }
}
