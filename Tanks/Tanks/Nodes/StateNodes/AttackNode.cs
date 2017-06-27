using Tanks.Components;
using Tanks.Components.StateComponents;
using Ash.Core;

namespace Tanks.Nodes.StateNodes
{
    class AttackNode : Node
    {
        public Position position;
        public Attack attack;
        public FSM fsm;
        public AI ai;
        public Motion motion;
        public Gun gun;
    }
}
