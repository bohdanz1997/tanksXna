using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tanks.Tools;

namespace Tanks.Screens
{
    public class SplashItem
    {
        public Clip Image;
        public Vector2 Position;
        public bool Active;
        public Vector2 Velocity;
        public Vector2 Finish;

        public void Update()
        {
            if (Active)
            {
                Position.X += Velocity.X;
                Position.Y += Velocity.Y;
            }
        }

        public bool End()
        {
            return Math.Abs(Position.Y - Finish.Y) < 4 && Math.Abs(Position.X - Finish.X) < 4;
        }

        public void Draw(SpriteBatch window)
        {
            Image.Position = Position;
            Image.Draw(window);
        }
    }
    class Splash : ScreenBase
    {
        private List<SplashItem> items;
        private Stopwatch st;

        public Splash(SpriteBatch window) : base(window)
        {
            st = new Stopwatch();
            items = new List<SplashItem>();
            
        }

        public override void Update(float elapsed)
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Update();
                if (items[i].Active && items[i].End())
                {
                    items[i].Active = false;
                    if (i + 1 < items.Count)
                        items[i + 1].Active = true;
                    else
                        st.Start();
                }
            }
            if (st.IsRunning && st.ElapsedMilliseconds > 1000 ||
                Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                st.Reset();
                Manager.Pop();
                Manager.Push(ScreenType.Game);
            }
        }

        public override void Draw()
        {
            base.Draw();
            for (int i = 0; i < items.Count; i++)
                items[i].Draw(spriteBatch);
        }
    }
}
