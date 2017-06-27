using Tanks.Components;
using Tanks.Components.StateComponents;
using Ash.Core;

namespace Tanks.Nodes.StateNodes
{
    class WalkNode : Node
    {
        public Position position;
        public Walk walk;
        public Controls controls;
        public FSM fsm;
        public Motion motion;
    }
}
