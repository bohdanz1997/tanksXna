using Ash.Core;
using Tanks.Components;

namespace Tanks.Nodes
{
    class TankCollisionNode : Node
    {
        public Position position;
        public Collision collision;
        public Controls controls;
        public Motion motion;
        public Health health;
    }
}
