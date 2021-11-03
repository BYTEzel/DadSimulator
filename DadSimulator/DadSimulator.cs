using DadSimulator.Animation;
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
            m_screenSize = new Size(1920, 1200);
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
            const float zoom = 8.0f;
            m_camera = new Camera.Camera(m_screenSize, zoom);
            m_uiEngine = new UiEngine(GraphicsDevice, m_spriteBatch, m_font, m_font, zoom);

            var gameStats = new Stats();

            var origin = Vector2.Zero;
            var grassBackgroundTexture = LoadTemplate(Templates.LevelGrassBackground);
            var collisionMap = new CollidableMap(new Size(grassBackgroundTexture.Width, grassBackgroundTexture.Height));
            var levelBackgroundGrass = new LevelBackgroundDayNight(grassBackgroundTexture, origin, m_gameTimer);
            var levelFloor = new LevelBackground(LoadTemplate(Templates.LevelFloor), origin);
            var levelWalls = new LevelBounds(this, Templates.LevelWalls, origin, collisionMap);
            var levelInterior = new LevelBounds(this, Templates.LevelInterior, origin, collisionMap);
            var levelInteriorBackground = new LevelBackground(LoadTemplate(Templates.LevelInteriorBackground), origin);
            var levelWindowsImages = new LevelBackground(LoadTemplate(Templates.LevelWindowsImages), origin);
                
            var player = new Player(
                new Spritesheet(LoadTemplate(Templates.Character), 4, 
                    new List<string>()
                    { "idle-down", "walk-down", "idle-up", "walk-up", "idle-right", "idle-left", "walk-right", "walk-left"}),
                new RectangleCollider(new Rectangle(2, 4, 14, 12)),
                new Vector2(686, 480), 
                new KeyboardUserCommand(), collisionMap, this, m_uiEngine); 
                
            var washMaschine = new WashingMachine(
                new Spritesheet(LoadTemplate(Templates.WashingMachine), 4, 
                new List<string>() { "idle", "washing"}),
                new Vector2(592, 480), new Vector2(600, 488), 
                m_gameTimer, gameStats);

            var changingTable = new ChangingTable(
                new Spritesheet(LoadTemplate(Templates.ChangingTable), 1,
                new List<string>() { "stash-100", "stash-75", "stash-50", "stash-25", "stash-0" }),
                new Vector2(47 * 16, 41 * 16), new Vector2(47 * 16 + 8, 41 * 16 + 8),
                gameStats);

            m_gameObjects.Add(levelBackgroundGrass);
            m_gameObjects.Add(levelFloor);
            m_gameObjects.Add(levelInteriorBackground);
            m_gameObjects.Add(levelInterior);
            m_gameObjects.Add(levelWalls);
            m_gameObjects.Add(levelWindowsImages);
            m_gameObjects.Add(washMaschine);
            m_gameObjects.Add(changingTable);
            m_gameObjects.Add(player);

            m_interactables.Add(washMaschine);
            m_interactables.Add(changingTable);

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
                    stringName = "Persons/dad";
                    break;
                case Templates.Test:
                    stringName = "Test/block";
                    break;
                case Templates.TestTextureTransparency:
                    stringName = "Test/collider-test";
                    break;
                case Templates.LevelWalls:
                    stringName = "Level/walls";
                    break;
                case Templates.LevelGrassBackground:
                    stringName = "Level/level-background";
                    break;
                case Templates.LevelFloor:
                    stringName = "Level/floor";
                    break;
                case Templates.LevelInterior:
                    stringName = "Level/interior";
                    break;
                case Templates.LevelWindowsImages:
                    stringName = "Level/windows-images";
                    break;
                case Templates.LevelInteriorBackground:
                    stringName = "Level/interior-background";
                    break;
                case Templates.WashingMachine:
                    stringName = "Interactables/washing-machine";
                    break;
                case Templates.ChangingTable:
                    stringName = "Interactables/changing-table";
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
