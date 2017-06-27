using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tanks.Components
{
    class Controls
    {
        public Keys up, down, left, right;
        public Vector2 acceleration;

        public Controls(Keys up, Keys down, Keys left, Keys right, Vector2 acceleration)
            : this(acceleration)
        {
            this.left = left;
            this.right = right;
            this.up = up;
            this.down = down;
        }

        public Controls(Vector2 acceleration)
        {
            this.acceleration = acceleration;
        }        
    }
}
