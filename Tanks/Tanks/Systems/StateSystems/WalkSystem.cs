using Tanks.Nodes.StateNodes;
using Microsoft.Xna.Framework;
using Tanks.Tools.Extensions;
using Tanks.Enums;
using Ash.Tools;

namespace Tanks.Systems.StateSystems
{
    class WalkSystem : ListIteratingSystem<WalkNode>
    {
        int cellSize;

        public WalkSystem(int cellSize)
        {
            this.cellSize = cellSize;
            NodeUpdate += Update;
        }
        
        private void Update(WalkNode node, float time)
        {
            if (node.position.InCell(cellSize))
            {
                var nextDirection = node.position.nextDirection;
                var motion = node.motion;
                var controls = node.controls;
                
                if (node.position.IsTurn(nextDirection))
                {
                    var moveFSM = node.fsm.list["MoveFSM"];                    
                    moveFSM.ChangeState(States.Stand, () => motion.velocity = Vector2.Zero);
                }
                else
                {
                    switch (nextDirection)
                    {
                        case Direction.Up: motion.velocity.Y = -controls.acceleration.Y; break;
                        case Direction.Down: motion.velocity.Y = controls.acceleration.Y; break;
                        case Direction.Left: motion.velocity.X = -controls.acceleration.X; break;
                        case Direction.Right: motion.velocity.X = controls.acceleration.X; break;
                    }
                }
            }
        }
    }
}
