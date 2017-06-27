using Tanks.Components;
using Tanks.Components.StateComponents;
using Ash.Core;

namespace Tanks.Nodes.StateNodes
{
    class PursuitNode : Node
    {
        public Position position;
        public Collision collision;
        public Pursuit pursuit;
        public Motion motion;
        public Display display;
        public FSM fsm;
        public AI ai;
    }
}
