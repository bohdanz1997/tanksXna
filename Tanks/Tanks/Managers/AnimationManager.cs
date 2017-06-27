using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Tanks.Tools;

namespace Tanks.Managers
{
    class AnimationManager
    {
        private Dictionary<Enum, Clip> _animations;
        private Enum _currentKeyAnimation;

        public Clip Current => _animations[_currentKeyAnimation];
        
        public AnimationManager()
        {
            _animations = new Dictionary<Enum, Clip>();
        }

        public AnimationManager Add(Enum key, Clip animation)
        {
            _animations.Add(key, animation);
            return this;
        }

        public AnimationManager AddAndChange(Enum key, Clip animation)
        {
            _animations.Add(key, animation);
            Change(key);
            return this;
        }

        public AnimationManager Change(Enum key)
        {
            _currentKeyAnimation = key;
            return this;
        }

        public AnimationManager Remove(Enum key)
        {
            _animations.Remove(key);
            return this;
        }

        public void Update(float time)
        {
            Current.Update(time);
        }

        public void Draw(SpriteBatch sb)
        {
            Current.Draw(sb);
        }
    }
}
