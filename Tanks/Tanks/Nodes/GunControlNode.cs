using Ash.Core;
using Tanks.Components;

namespace Tanks.Nodes
{
    class GunControlNode : Node
    {
        public Gun gun;
        public GunControls controls;
        public Position position;
        public Player player;
    }
}
