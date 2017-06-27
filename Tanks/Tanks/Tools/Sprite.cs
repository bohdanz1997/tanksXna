using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tanks.Enums;

namespace Tanks.Tools
{
    public class Sprite
    {
        private Texture2D _texture;
        private float _layer;

        public Sprite(Texture2D texture, Vector2 size, float layer)
        {
            _texture = texture;
            _layer = layer / 10f;
            Width = size.X;
            Height = size.Y;
            Scale = new Vector2(1);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle sourceRect)
        {
            spriteBatch.Draw(
                _texture, 
                new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height), 
                sourceRect, 
                Color.White, 
                Rotation, 
                Origin,
                SpriteEffects.None, 
                _layer
            );
        }

        public void Dispose()
        {
            _texture.Dispose();
        }


        public float X { get; set; }
        public float Y { get; set; }
        public Vector2 Position
        {
            get { return new Vector2(X, Y); }
            set { X = value.X; Y = value.Y; }
        }

        public float Width { get; set; }
        public float Height { get; set; }
        public Vector2 Size
        {
            get { return new Vector2(Width, Height); }
            set { Width = value.X; Height = value.Y; }
        }

        public Vector2 Origin { get; set; }
        public Vector2 Scale { get; set; }
        public float Rotation { get; set; }
    }
}
