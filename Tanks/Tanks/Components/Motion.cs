using Microsoft.Xna.Framework;

namespace Tanks.Components
{
    class Motion
    {
        public Vector2 velocity;
        public Vector2 damping;
        public Vector2 maxDamping;
        public int segment;

        public Motion(Vector2 velocity, int segment = 0, int mDampingX = 0, int mDampingY = 0)
        {
            this.segment = segment;
            this.velocity = velocity;
            maxDamping = new Vector2(mDampingX, mDampingY);
        }
    }
}
