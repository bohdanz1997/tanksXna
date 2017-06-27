using Ash.Tools;
using Tanks._Game;
using Tanks.Components.StateComponents;
using Tanks.Enums;
using Tanks.Nodes;
using Tanks.Tools.Extensions;

namespace Tanks.Systems
{
    class TrackSystem : ListIteratingSystem<TrackNode>
    {
        private EntityFactory creator;
        private int cellSize;
        
        public TrackSystem(EntityFactory creator, int cellSize)
        {
            this.cellSize = cellSize;
            this.creator = creator;
            NodeUpdate += Update;
        }

        private void Update(TrackNode node, float time)
        {
            var position = node.position;
            var controls = node.controls;
            var motion = node.motion;

            if (position.InCell(cellSize))
            {
                //если стоим на месте и поворачиваем
                if (node.Entity.Has<Stand>() && position.IsTurn(position.nextDirection))
                {
                    CreateTracerFromDirection(position.direction, position.nextDirection, node);
                }
                //если не поворачиваем
                else if (!position.IsTurn(position.nextDirection))
                {
                    //если едем по горизонтали
                    if (motion.velocity.X != 0)
                    {
                        creator.CreateTrack(node.Entity, Animations.TraceHorisontal);
                    }
                    //если едем по вертикали
                    else if (motion.velocity.Y != 0)
                    {
                        creator.CreateTrack(node.Entity, Animations.TraceVertical);
                    }
                }
            }
        }

        private void CreateTracerFromDirection(Direction direction, Direction nextDirection, TrackNode node)
        {
            switch (direction)
            {
                case Direction.Up:
                    if (nextDirection == Direction.Left)
                        creator.CreateTrack(node.Entity, Animations.TraceUpLeft);
                    else if (nextDirection == Direction.Right)
                        creator.CreateTrack(node.Entity, Animations.TraceUpRight);
                    break;
                case Direction.Down:
                    if (nextDirection == Direction.Left)
                        creator.CreateTrack(node.Entity, Animations.TraceDownLeft);
                    else if (nextDirection == Direction.Right)
                        creator.CreateTrack(node.Entity, Animations.TraceDownRight);
                    break;
                case Direction.Left:
                    if (nextDirection == Direction.Up)
                        creator.CreateTrack(node.Entity, Animations.TraceDownRight);
                    else if (nextDirection == Direction.Down)
                        creator.CreateTrack(node.Entity, Animations.TraceUpRight);
                    break;
                case Direction.Right:
                    if (nextDirection == Direction.Up)
                        creator.CreateTrack(node.Entity, Animations.TraceDownLeft);
                    else if (nextDirection == Direction.Down)
                        creator.CreateTrack(node.Entity, Animations.TraceUpLeft);
                    break;
            }
        }
    }
}
