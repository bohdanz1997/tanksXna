using Ash.Core;
using Tanks.Nodes;
using Microsoft.Xna.Framework;

namespace Tanks.Systems
{
    class CameraFocusSystem : SystemBase
    {
        NodeList cameraNodeList;
        NodeList playerNodeList;

        public override void AddToGame(IGame game)
        {
            cameraNodeList = game.GetNodeList<CameraFocusNode>();
            playerNodeList = game.GetNodeList<TankCollisionNode>();
        }

        public override void RemoveFromGame(IGame game)
        {
            cameraNodeList = null;
            playerNodeList = null;
        }

        public override void Update(float time)
        {
            if (playerNodeList.Empty) return;
            var node = (TankCollisionNode)playerNodeList.Head;
            var cameraNode = (CameraFocusNode)cameraNodeList.Head;

            var cameraFocus = cameraNode.camera.camera;
            var cameraMotion = cameraNode.motion;
            var motion = node.motion;

            var screenCenter = cameraFocus.Position;
            var nodeCenter = node.position.position + node.collision.center;

            //двигаем камеру когда игрок едет по сторонам

            if (motion.velocity.X > 0 && screenCenter.X - nodeCenter.X < -100)
                cameraMotion.velocity.X = node.motion.velocity.X;
            else if (motion.velocity.X < 0 && screenCenter.X - nodeCenter.X > 100)
                cameraMotion.velocity.X = node.motion.velocity.X;
            else cameraMotion.velocity.X *= 0.9f;

            if (motion.velocity.Y > 0 && screenCenter.Y - nodeCenter.Y < -50)
                cameraMotion.velocity.Y = node.motion.velocity.Y;
            else if (motion.velocity.Y < 0 && screenCenter.Y - nodeCenter.Y > 50)
                cameraMotion.velocity.Y = node.motion.velocity.Y;
            else cameraMotion.velocity.Y *= 0.9f;
            
            cameraFocus.Move(cameraMotion.velocity);

            //Запрещаем камере выйти за границы карты
            if (cameraFocus.Position.X <= cameraFocus.Viewport2.X)
                cameraFocus.Position = new Vector2(cameraFocus.Viewport2.X, cameraFocus.Position.Y);
            else if (cameraFocus.Position.X > cameraNode.camera.maxSize.X - cameraFocus.Viewport2.X)
                cameraFocus.Position = new Vector2(cameraNode.camera.maxSize.X - cameraFocus.Viewport2.X, cameraFocus.Position.Y);

            if (cameraFocus.Position.Y <= cameraFocus.Viewport2.Y)
                cameraFocus.Position = new Vector2(cameraFocus.Position.X, cameraFocus.Viewport2.Y);
            else if (cameraFocus.Position.Y > cameraNode.camera.maxSize.Y - cameraFocus.Viewport2.Y)
                cameraFocus.Position = new Vector2(cameraFocus.Position.X, cameraNode.camera.maxSize.Y - cameraFocus.Viewport2.Y);
        }
    }
}
