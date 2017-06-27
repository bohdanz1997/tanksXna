using Microsoft.Xna.Framework;

namespace Tanks.Components
{
    class Quake
    {
        public float delay;
        public float maxDelay;
        public int currentPlanItem;
        public Vector2[] quakePlan =
        {
            new Vector2(1, 1),
            new Vector2(-1, -1),
            new Vector2(1, -1),
            new Vector2(-1, 1)
        };

        public Quake(float delay)
        {
            this.delay = delay;
            maxDelay = delay;
        }
    }
}
