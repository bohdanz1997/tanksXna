using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tanks.Managers;
using Tanks.Screens;
using Tanks.Tools;
using Tanks._Game;
using Tanks.Resources;

namespace Tanks
{
    public class TanksGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ScreenManager screenManager;
        
        Color fillCollor;

        public TanksGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Global.TextureManager = new ResourceManager<Texture2D>(Content);
            Global.FontManager = new ResourceManager<SpriteFont>(Content);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 810;
            graphics.PreferredBackBufferHeight = 480;
            fillCollor = Color.Black;
            IsMouseVisible = true;
        }
        
        protected override void Initialize()
        {
            base.Initialize();
            var screenSize = new Vector2(
                graphics.PreferredBackBufferWidth, 
                graphics.PreferredBackBufferHeight
            );
            Global.Camera = new Camera(screenSize);
            Global.Camera.Position = screenSize / 2;
            Global.InputManager = new InputManager();
            Global.ContentManager = Content;

            Config.Instantiate();
            Config.Instance.ScreenWidth = (int)screenSize.X;
            Config.Instance.ScreenHeight = (int)screenSize.Y;
            Config.Instance.CellSize = 30;
            Config.Instance.MoveUp = Keys.W;
            Config.Instance.MoveDown = Keys.S;
            Config.Instance.MoveLeft = Keys.A;
            Config.Instance.MoveRight = Keys.D;
            Config.Instance.GunTrigger = Keys.Space;

            Components.Add(Global.InputManager);
            
            screenManager = new ScreenManager()
                .AddScreen(ScreenType.Menu, new Menu(spriteBatch))
                .AddScreen(ScreenType.Game, new GameScreen(spriteBatch))
                .AddScreen(ScreenType.Pause, new Pause(spriteBatch))
                .Push(ScreenType.Game);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Fonts.Instantiate();
            Images.Instantiate();
            ResourceLoader.LoadResources<Texture2D, Images>(Global.TextureManager);
            ResourceLoader.LoadResources<SpriteFont, Fonts>(Global.FontManager);
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            GraphicsDevice.Clear(fillCollor);
            
            spriteBatch.Begin(
                sortMode: SpriteSortMode.BackToFront, 
                blendState: BlendState.AlphaBlend,
                samplerState: null, 
                depthStencilState: null, 
                rasterizerState: null, 
                effect: null, 
                transformMatrix: Global.Camera.GetTransformation()
            );

            screenManager.Update(gameTime.ElapsedGameTime.Milliseconds);
            screenManager.Draw();
            
            spriteBatch.End();

            base.Update(gameTime);
        }
    }
}
