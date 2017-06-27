using Ash.Core;
using Tanks.Components;

namespace Tanks.Nodes
{
    class CameraMovementNode : Node
    {
        public CameraFocus camera;
        public Position position;
        public Controls controls;
        public Motion motion;
    }
}
