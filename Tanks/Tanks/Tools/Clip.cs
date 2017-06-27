using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tanks.Tools
{
    public class Clip
    {
        private Sprite _sprite;
        private Rectangle _sourceRect;
        private int _currentFrame;
        private bool _playing;
        private int _framesCount;
        private float _timer;
        private float _delay;
        private bool _reverse;
        private bool _repeat;

        public Clip(Sprite sprite, Vector2 offset, int framesCount = 1, float delay = 0, 
            bool playing = false, bool reverse = false, bool repeat = false)
        {
            _sprite = sprite;
            _framesCount = framesCount;
            _delay = delay;
            _reverse = reverse;
            _playing = playing;
            _repeat = repeat;
            Offset = offset;
            Reset();
            SetFrame(_currentFrame);
        }

        public void SetFrame(int frame)
        {
            if (frame < 0) frame = 0;
            else if (frame >= _framesCount)
                frame = _framesCount - 1;

            _currentFrame = frame;
            _sourceRect = new Rectangle(
                (int)(_currentFrame * _sprite.Width + Offset.X),
                (int)Offset.Y,
                (int)_sprite.Width,
                (int)_sprite.Height
            );
        }

        public void Update(float time)
        {
            if (_playing)
            {
                _timer += time;
                if (_timer > _delay)
                {
                    _timer = 0;
                    if (_reverse)
                    {
                        _currentFrame--;
                        if (_currentFrame < 0)
                        {
                            if (_repeat)
                            {
                                _currentFrame = _framesCount - 1;
                            }
                            else
                            {
                                _playing = false;
                                _currentFrame = 0;
                            }
                        }
                    }
                    else
                    {
                        _currentFrame++;
                        if (_currentFrame > _framesCount - 1)
                        {
                            if (_repeat)
                            {
                                _currentFrame = 0;
                            }
                            else
                            {
                                _playing = false;
                                _currentFrame = _framesCount - 1;
                            }
                        }
                    }
                    SetFrame(_currentFrame);
                }
            }
        }

        public void Reset()
        {
            SetFrame(_reverse ? _framesCount - 1 : 0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch, _sourceRect);
        }

        public void Dispose()
        {
            _sprite.Dispose();
        }


        public Vector2 Offset { get; set; }

        public float X
        {
            get { return _sprite.X; }
            set { _sprite.X = value; }
        }

        public float Y
        {
            get { return _sprite.Y; }
            set { _sprite.Y = value; }
        }

        public Vector2 Position
        {
            get { return _sprite.Position; }
            set { _sprite.Position = value; }
        }

        public float Width
        {
            get { return _sprite.Width; }
            set { _sprite.Width = value; }
        }

        public float Height
        {
            get { return _sprite.Height; }
            set { _sprite.Height = value; }
        }

        public Vector2 Size
        {
            get { return _sprite.Size; }
            set { _sprite.Size = value; }
        }

        public bool Playing
        {
            get { return _playing; }
            set { _playing = value; }
        }
    }
}
