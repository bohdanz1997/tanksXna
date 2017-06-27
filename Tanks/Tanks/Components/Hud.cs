using Microsoft.Xna.Framework.Graphics;

namespace Tanks.Components
{
    class Hud
    {
        public SpriteFont font;
        public string text;

        public Hud(SpriteFont font, string text)
        {
            this.font = font;
            this.text = text;
        }
    }
}
