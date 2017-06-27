using Tanks.Components;
using Tanks.Components.StateComponents;
using Ash.Core;

namespace Tanks.Nodes.StateNodes
{
    class StandNode : Node
    {
        public Position position;
        public Display display;
        public Stand stand;
        public Motion motion;
        public FSM fsm;
    }
}
