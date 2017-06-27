using Tanks.Managers;
using Tanks.Nodes;
using Tanks.Enums;
using Ash.Tools;

namespace Tanks.Systems
{
    class PlayerControlSystem : ListIteratingSystem<PlayerControlNode>
    {
        InputManager inputManager;
        
        public PlayerControlSystem(InputManager inputManager)
        {
            this.inputManager = inputManager;
            NodeUpdate += HandleNodeUpdate;
        }

        private void HandleNodeUpdate(PlayerControlNode node, float time)
        {
            var controls = node.controls;
            var position = node.position;

            if (inputManager.IsKeyDown(controls.up))
            {
                position.nextDirection = Direction.Up;
            }
            else if (inputManager.IsKeyDown(controls.down))
            {
                position.nextDirection = Direction.Down;
            }
            else if (inputManager.IsKeyDown(controls.left))
            {
                position.nextDirection = Direction.Left;
            }
            else if (inputManager.IsKeyDown(controls.right))
            {
                position.nextDirection = Direction.Right;
            }
            else
            {
                position.nextDirection = Direction.None;
            }
        }
    }
}
