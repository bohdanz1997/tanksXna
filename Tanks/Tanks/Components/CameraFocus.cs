using Microsoft.Xna.Framework;
using Tanks.Tools;

namespace Tanks.Components
{
    class CameraFocus
    {
        public Camera camera;
        public Vector2 maxSize;

        public CameraFocus(Camera camera, Vector2 maxSize)
        {
            this.camera = camera;
            this.maxSize = maxSize;
        }
    }
}
