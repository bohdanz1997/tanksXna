using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tanks.Tools
{
    public class Button
    {
        public Clip Image;
        bool hover;
        bool leave;

        public bool Active;
        public Rectangle Bounds;
        public Vector2 Position
        {
            get { return new Vector2(Bounds.X, Bounds.Y); }
            set
            {
                Bounds.X = (int)value.X;
                Bounds.Y = (int)value.Y;
                Image.Position = new Vector2(Bounds.X, Bounds.Y);
            }
        }
        public int Y
        {
            get { return Bounds.Y; }
            set { Bounds.Y = value;
                Image.Position = new Vector2(Image.Position.X, Bounds.Y);
            }
        }

        public event Action Click;

        public Button(Texture2D texture)
        {
            //Image = new Clip(texture, Vector2.Zero, new Vector2(texture.Width / 2, texture.Height), 2);
            //Bounds = new Rectangle(0, 0, (int)Image.FrameSize.X, (int)Image.FrameSize.Y);
            //Image.Layer = 1;
        }

        public void Update()
        {
            if (!Active) return;

            var mouseState = Mouse.GetState();
            var mousePosition = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

            if (Bounds.Intersects(mousePosition))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke();
                }
                if (!hover)
                {
                    hover = true;
                    leave = false;
                    Image.SetFrame(1);
                }
            }
            else if (!Bounds.Intersects(mousePosition) && !leave)
            {
                hover = false;
                leave = true;
                Image.SetFrame(0);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                Image.Draw(spriteBatch);
            }
        }
    }
}
