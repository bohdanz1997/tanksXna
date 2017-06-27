using Ash.Core;

namespace Tanks.Components
{
    class Bullet
    {
        public Entity owner;

        public Bullet(Entity owner)
        {
            this.owner = owner;
        }
    }
}
