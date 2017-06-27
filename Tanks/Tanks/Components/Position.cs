using Microsoft.Xna.Framework;
using Tanks.Enums;

namespace Tanks.Components
{
    class Position
    {
        public Vector2 position;
        public Direction direction;
        public Direction nextDirection;
        public Vector2 prevCell;

        public Position(float x, float y, Direction dir = 0)
        {
            position = new Vector2(x, y);
            direction = dir;
        }

        public float X
        {
            get { return position.X; }
            set { position.X = value; }
        }

        public float Y
        {
            get { return position.Y; }
            set { position.Y = value; }
        }
    }
}
