using Tanks.Managers;
using Microsoft.Xna.Framework.Content;
using Tanks.Tools;
using Microsoft.Xna.Framework.Graphics;

namespace Tanks._Game
{
    public static class Global
    {
        public static Camera Camera;
        public static InputManager InputManager;
        public static ContentManager ContentManager;
        public static ResourceManager<SpriteFont> FontManager;
        public static ResourceManager<Texture2D> TextureManager;
    }
}
