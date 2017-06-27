using Tanks.Managers;
using Microsoft.Xna.Framework;
using Tanks.Enums;
using Tanks.Tools;

namespace Tanks.Components
{
    class Display
    {
        public Clip clip => animManager.Current;
        public AnimationManager animManager;
        public Vector2 offset;

        public Display(AnimationManager aManager, float offsetX = 0, float offsetY = 0)
        {
            animManager = aManager;
            offset = new Vector2(offsetX, offsetY);
        }

        public Display(Clip clip, float offsetX = 0, float offsetY = 0)
            : this(new AnimationManager().AddAndChange(Animations.None, clip), offsetX, offsetY) { }
    }
}
