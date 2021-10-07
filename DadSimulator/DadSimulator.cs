using DadSimulator.Camera;
using DadSimulator.Collider;
using DadSimulator.GraphicObjects;
using DadSimulator.Interactable;
using DadSimulator.IO;
using DadSimulator.Misc;
using DadSimulator.UI;
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
        private SpriteFont m_font;
        private IUiEngine m_uiEngine;
        private readonly Timer m_gameTimer;
        private ICamera m_camera;
        private readonly Size m_screenSize;

        public DadSimulator()
        {
            m_screenSize = new Size()
            {
                Width = 1920,
                Height = 1200
            };
            InitGraphics();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            m_gameObjects = new List<IGraphicObject>();
            m_interactables = new List<IInteractable>();
            m_gameTimer = new Timer();
        }

        private void InitGraphics()
        {
            m_graphics = new GraphicsDeviceManager(this);
            m_graphics.ApplyChanges();

            //m_graphics.IsFullScreen = true;
            m_graphics.PreferredBackBufferWidth = (int)m_screenSize.Width;
            m_graphics.PreferredBackBufferHeight = (int)m_screenSize.Height;
            m_graphics.ApplyChanges();

            m_font = Content.Load<SpriteFont>("Content/Fonts/8-bit Operator+");
            m_spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Initialize()
        {            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            m_camera = new Camera.Camera(m_screenSize, 2);
            m_uiEngine = new UiEngine(GraphicsDevice, m_spriteBatch, m_font, m_font);

            var collisionMap = new CollidableMap(new Size() { Width = 640, Height = 480 });
            var levelBackgroundGrass = new LevelBackgroundDayNight(LoadTemplate(Templates.LevelGrassBackground), new Vector2(-500, -500), m_gameTimer);
            var levelBounds = new LevelBounds(this, Templates.LevelWalls, new Vector2(0, 0), collisionMap);
                
            var player = new Player(LoadTemplate(Templates.Character), new Vector2(200, 200), 
                new KeyboardUserCommand(), collisionMap, this, m_uiEngine); 
                
            var washMaschine = new WashingMachine(LoadTemplate(Templates.Test), 
                new Vector2(200, 50), new Vector2(201, 51));

            m_gameObjects.Add(levelBackgroundGrass);
            m_gameObjects.Add(levelBounds);
            m_gameObjects.Add(washMaschine);
            m_gameObjects.Add(player);

            m_interactables.Add(washMaschine);

            m_camera.Follow(player);
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
            m_gameTimer.Update(gameTime);
            m_camera.UpdatePosition();
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            m_spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: m_camera.Transform);
            foreach (var gameObj in m_gameObjects)
            {
                gameObj.Draw(m_spriteBatch);
            }

            m_uiEngine.DrawText(m_camera.GetCameraTopLeftPosition(), Color.White, m_gameTimer.GetGameDateAndTime(), true);

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
                case Templates.LevelGrassBackground:
                    stringName = "Backgrounds/level-background";
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
