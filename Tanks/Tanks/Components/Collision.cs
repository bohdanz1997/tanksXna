using Microsoft.Xna.Framework;
using Tanks.Enums;

namespace Tanks.Components
{
    class Collision
    {
        public EntityType[] collidesWith;
        public bool collisionDetected;
        public RectangleF bounds;
        public Vector2 center;
        public int collisionRadius;

        public Collision(Vector2 size, int collisionRadius, EntityType[] types = null)
        {
            this.collisionRadius = collisionRadius;
            bounds = new RectangleF(0, 0, size.X, size.Y);
            center = bounds.Center;
            collisionDetected = false;
            collidesWith = types ?? new EntityType[0];
        }
    }
}
