using Ash.Core;
using Tanks.Components;

namespace Tanks.Nodes
{
    class MovementNode : Node
    {
        public Position position;
        public Motion motion;
    }

    class MovementFSMNode : Node
    {
        public Position position;
        public Motion motion;
        public FSM fsm;
    }
}
