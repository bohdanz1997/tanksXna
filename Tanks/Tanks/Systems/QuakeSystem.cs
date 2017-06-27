using Tanks.Nodes;
using Ash.Core;

namespace Tanks.Systems
{
    class QuakeSystem : SystemBase
    {
        NodeList quakeNodeList;
        NodeList cameraNodeList;
        
        public override void AddToGame(IGame game)
        {
            quakeNodeList = game.GetNodeList<QuakeNode>();
            cameraNodeList = game.GetNodeList<CameraFocusNode>();
        }

        public override void RemoveFromGame(IGame game)
        {
            quakeNodeList = null;
            cameraNodeList = null;
        }

        public override void Update(float time)
        {
            if (quakeNodeList.Empty) return;

            var cameraNode = (CameraFocusNode)cameraNodeList.Head;
            var quakeNode = (QuakeNode)quakeNodeList.Head;
            
            var camera = cameraNode.camera;
            var quake = quakeNode.quake;
            var controls = quakeNode.controls;

            quake.delay -= time;
            if (quake.delay <= 0)
            {
                quake.delay = quake.maxDelay;
                camera.camera.Move(controls.acceleration * quake.quakePlan[quake.currentPlanItem]);
                quake.currentPlanItem = 
                    quake.currentPlanItem + 1 < quake.quakePlan.Length 
                    ? quake.currentPlanItem + 1 : 0;
            }
        }
    }
}
