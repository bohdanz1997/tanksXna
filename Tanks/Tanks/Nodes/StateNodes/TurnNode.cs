using Tanks.Components;
using Tanks.Components.StateComponents;
using Ash.Core;

namespace Tanks.Nodes.StateNodes
{
    class TurnNode : Node
    {
        public Position position;
        public Display display;
        public Turn turn;
        public FSM fsm;
    }
}
