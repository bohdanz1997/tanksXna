using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tanks
{
    public class Clip2
    {
        public Vector2 FrameSize { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public Vector2 Scale { get; set; }
        public int CurrentFrame { get; set; }
        public bool Playing { get; set; }
        public float Rotation { get; set; }
        public float Layer { get; set; }

        Vector2 offset;
        Vector2 framesRange;
        Rectangle srcRect;
        Texture2D texture;
        int framesCount;
        float timer;
        float delay;
        bool repeat;
        bool reverse;
        
        public Clip2(Texture2D texture, Vector2 offset, Vector2 frameSize, int framesCount = 1, 
            float delay = 1, bool reverse = false, int layer = 0, bool repeat = false)
        {
            Scale = new Vector2(1, 1);
            this.texture = texture;
            this.framesCount = framesCount;
            this.delay = delay;
            this.offset = offset;
            this.repeat = repeat;
            this.reverse = reverse;
            FrameSize = frameSize;
            framesRange = new Vector2(0, framesCount);
            Layer = layer / 10f;
            CurrentFrame = reverse ? (int)framesRange.Y - 1 : (int)framesRange.X;
            SetFrame(CurrentFrame);
        }

        public void Update(float time)
        {
            if (Playing)
            {
                timer += time;
                if (timer > delay)
                {
                    timer = 0;
                    if (reverse)
                    {
                        if (--CurrentFrame < (int)framesRange.X)
                        {
                            if (repeat)
                                CurrentFrame = (int)framesRange.Y - 1;
                            else
                                Playing = false;
                        }
                    }
                    else
                    {
                        if (++CurrentFrame >= (int)framesRange.Y)
                        {
                            if (repeat)
                                CurrentFrame = (int)framesRange.X;
                            else
                                Playing = false;
                        }
                    }
                    SetFrame(CurrentFrame);
                }
            }
        }

        public void Reset()
        {
            CurrentFrame = reverse ? (int)framesRange.Y - 1 : (int)framesRange.X;
            SetFrame(CurrentFrame);
        }

        public void SetFrame(int val)
        {
            if (val < 0) val = 0;
            else if (val >= framesCount)
                val = framesCount - 1;
            CurrentFrame = val;
            srcRect = new Rectangle(CurrentFrame * (int)FrameSize.X + (int)offset.X, (int)offset.Y, (int)FrameSize.X, (int)FrameSize.Y);
        }
        
        public void GoTo(int frame)
        {
            SetFrame(frame);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y, (int)FrameSize.X, (int)FrameSize.Y),
                srcRect, Color.White, Rotation, Origin, SpriteEffects.None, Layer);
        }

        public void Dispose()
        {
            texture.Dispose();
        }
    }
}
