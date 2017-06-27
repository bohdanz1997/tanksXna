using Ash.Tools;
using System;
using Tanks.Enums;
using Tanks.Nodes;
using Tanks.Tools.Extensions;

namespace Tanks.Systems
{
    class MovementSystem : ListIteratingSystem<MovementNode>
    {
        int cellSize;

        public MovementSystem(int cellSize)
        {
            this.cellSize = cellSize;
            NodeUpdate += HandleNodeUpdate;
        }

        private void HandleNodeUpdate(MovementNode node, float time)
        {
            var position = node.position;
            var motion = node.motion;

            position.position += motion.velocity;

            var roundPos = position.position.Divide(cellSize) * cellSize;

            switch (position.direction)
            {
                case Direction.Up:
                    if (Math.Abs(position.position.Y - roundPos.Y)
                        < Math.Abs(motion.velocity.Y))
                        position.position.Y = roundPos.Y;
                    break;
                case Direction.Down:
                    if (Math.Abs(position.position.Y - (roundPos.Y + cellSize))
                        < Math.Abs(motion.velocity.Y))
                        position.position.Y = roundPos.Y + cellSize;
                    break;
                case Direction.Left:
                    if (Math.Abs(position.position.X - roundPos.X)
                        < Math.Abs(motion.velocity.X))
                        position.position.X = roundPos.X;
                    break;
                case Direction.Right:
                    if (Math.Abs(position.position.X - (roundPos.X + cellSize))
                        < Math.Abs(motion.velocity.X))
                        position.position.X = roundPos.X + cellSize;
                    break;
            }
        }
    }
}
