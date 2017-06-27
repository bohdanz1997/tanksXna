using System.Collections.Generic;
using Tanks.Managers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Tanks.Tools;
using Tanks._Game;

namespace Tanks.Screens
{
    public class ScreenBase
    {
        protected SpriteBatch spriteBatch;
        public ScreenManager Manager { get; set; }
        protected ContentManager content;
        protected ResourceManager<Texture2D> textureManager;
        protected InputManager inputManager;
        protected List<Button> widgets;

        public ScreenBase(SpriteBatch spriteBatch)
        {
            widgets = new List<Button>();
            this.spriteBatch = spriteBatch;
            content = Global.ContentManager;
            textureManager = Global.TextureManager;
            inputManager = Global.InputManager;
        }
        public virtual void Activate(ScreenType prevScreen)
        {
            foreach(var w in widgets)
            {
                w.Active = true;
            }
        }

        protected Button Add(Button b)
        {
            widgets.Add(b);
            return b;
        }

        public virtual void Deactivate()
        {
            foreach (var w in widgets)
            {
                w.Active = false;
            }
        }

        public virtual void Update(float time)
        {
            foreach (var w in widgets)
            {
                w.Update();
            }
        }

        public virtual void Draw()
        {
            foreach (var w in widgets)
            {
                w.Draw(spriteBatch);
            }
        }
    }
}
