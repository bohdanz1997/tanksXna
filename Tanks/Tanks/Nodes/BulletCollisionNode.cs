using Tanks.Components;
using Ash.Core;

namespace Tanks.Nodes
{
    class BulletCollisionNode : Node
    {
        public Bullet bullet;
        public Position position;
        public Collision collision;
        public Damage damage;
        public Health health;
        public ActionAfterDeath actionAfterDeath;
    }

    class CollisionNode : Node
    {
        public Position position;
        public Collision collision;
        public Damage damage;
        public Health health;
        public ActionAfterDeath actionAfterDeath;
        public Display display;
    }
}
