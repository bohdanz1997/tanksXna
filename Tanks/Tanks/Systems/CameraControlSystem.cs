using Ash.Tools;
using Microsoft.Xna.Framework.Input;
using Tanks.Nodes;

namespace Tanks.Managers
{
    class CameraControlSystem : ListIteratingSystem<CameraMovementNode>
    {
        InputManager inputManager;

        public CameraControlSystem(InputManager inputManager)
        {
            this.inputManager = inputManager;
            NodeUpdate += Update;
        }

        public void Update(CameraMovementNode node, float time)
        {
            var camera = node.camera;
            var controls = node.controls;
            var motion = node.motion;
            var cameraFocus = node.camera.camera;
            
            if (inputManager.IsKeyDown(controls.left))
            {
                motion.damping.X = motion.damping.X > motion.maxDamping.X ? motion.maxDamping.X : motion.damping.X + 0.1f;
                motion.velocity.X = -controls.acceleration.X * motion.damping.X;
            }
            else if (inputManager.IsKeyDown(controls.right))
            {
                motion.damping.X = motion.damping.X > motion.maxDamping.X ? motion.maxDamping.X : motion.damping.X + 0.1f;
                motion.velocity.X = controls.acceleration.X * motion.damping.X;
            }
            else
            {
                motion.damping.X = 0;
                motion.velocity.X *= 0.99f;
            }

            if (inputManager.IsKeyDown(controls.up))
            {
                motion.damping.Y = motion.damping.Y > motion.maxDamping.Y ? motion.maxDamping.Y : motion.damping.Y + 0.1f;
                motion.velocity.Y = -controls.acceleration.Y * motion.damping.Y;
            }
            else if (inputManager.IsKeyDown(controls.down))
            {
                motion.damping.Y = motion.damping.Y > motion.maxDamping.Y ? motion.maxDamping.Y : motion.damping.Y + 0.1f;
                motion.velocity.Y = controls.acceleration.Y * motion.damping.Y;
            }
            else
            {
                motion.damping.Y = 0;
                motion.velocity.Y *= 0.99f;
            }

            if (inputManager.IsKeyDown(Keys.F9))
            {
                cameraFocus.Rotation -= 0.01f;
            }
            if (inputManager.IsKeyDown(Keys.F10))
            {
                cameraFocus.Rotation += 0.01f;
            }
            if (inputManager.IsKeyDown(Keys.F11))
            {
                cameraFocus.Zoom -= 0.01f;
            }
            if (inputManager.IsKeyDown(Keys.F12))
            {
                cameraFocus.Zoom += 0.01f;
            }

            camera.camera.Move(motion.velocity);
        }
    }
}
