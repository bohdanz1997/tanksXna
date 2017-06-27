using Tanks.Managers;
using Tanks.Nodes;
using Tanks.Components.StateComponents;
using Tanks._Game;
using Ash.Tools;

namespace Tanks.Systems
{
    class GunControlSystem : ListIteratingSystem<GunControlNode>
    {
        EntityFactory creator;
        InputManager inputManager;
        
        public GunControlSystem(EntityFactory creator, InputManager inputManager)
        {
            this.inputManager = inputManager;
            this.creator = creator;
            NodeUpdate += HandleUpdate;
        }

        private void HandleUpdate(GunControlNode node, float time)
        {
            var position = node.position;
            var control = node.controls;
            var gun = node.gun;
            
            if (inputManager.KeyWasPressed(control.trigger))
            {
                if (!node.Entity.Has<Turn>())
                    creator.CreateBullet(node.Entity, (int)node.gun.bulletLevel);
            }
            //gun.shooting = inputManager.IsKeyDown(control.trigger) && control.triggerReleased;
            //if (gun.shooting)
            //{
            //    control.triggerReleased = false;
            //    creator.CreateBullet(gun, position);
            //}
        }
    }
}
