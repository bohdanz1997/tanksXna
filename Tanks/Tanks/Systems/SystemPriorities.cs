using Ash.Core;
using Ash.Tools;

namespace Tanks.Systems
{
    class SystemPriorities
    {
        public const int PreUpdate = 1;
        public const int Update = 2;
        public const int Move = 3;
        public const int ResolveCollisions = 4;
        public const int StateMachines = 5;
        public const int Animate = 6;
        public const int Render = 7;
    }

    class ExampleSystem : ListIteratingSystem<Node>
    {
        public ExampleSystem()
        {
            NodeUpdate += Update;
        }

        private void Update(Node node, float time)
        {

        }
    }
}
