using Tanks.Components;
using Tanks.Components.StateComponents;
using Ash.Core;

namespace Tanks.Nodes.StateNodes
{
    class IdleNode : Node
    {
        public Position position;
        public Collision collision;
        public Idle idle;
        public FSM fsm;
        public AI ai;
    }
}
