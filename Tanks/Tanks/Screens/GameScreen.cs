using Microsoft.Xna.Framework.Graphics;
using Tanks.Tools;
using Tanks._Game;

namespace Tanks.Screens
{
    class GameScreen : ScreenBase
    {
        Sheduler sheduler;
        GameCore game;

        public GameScreen(SpriteBatch window) : base(window)
        {
            sheduler = new Sheduler();
            game = new GameCore(window);
        }

        public override void Activate(ScreenType prevScreen)
        {
            base.Activate(prevScreen);
            game.Initialize();
            //if (prevScreen == ScreenType.Menu)
            //{
            //    game.Initialize();
            //}
        }

        public override void Update(float elapsed)
        {
            game.Update(elapsed);
            sheduler.Update();
            base.Update(elapsed);
        }
        
        public override void Draw()
        {
            base.Draw();
        }
    }
}
