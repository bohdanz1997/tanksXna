using Tanks.Nodes.StateNodes;
using Microsoft.Xna.Framework;
using Tanks.Enums;
using Ash.Tools;

namespace Tanks.Systems.StateSystems
{
    class StandSystem : ListIteratingSystem<StandNode>
    {
        public StandSystem()
        {
            NodeUpdate += Update;
        }
        
        private void Update(StandNode node, float time)
        {
            var direction = node.position.direction;
            var nextDirection = node.position.nextDirection;
            node.motion.velocity = Vector2.Zero;

            if (nextDirection != Direction.None)
            {
                var moveFSM = node.fsm.list["MoveFSM"];

                if (nextDirection == direction)
                {
                    moveFSM.ChangeState(States.Walk);
                }
                else
                {
                    moveFSM.ChangeState(States.Turn, () => Turn(node));
                }
            }
        }

        private void Turn(StandNode node)
        {
            var direction = node.position.direction;
            var nextDirection = node.position.nextDirection;

            switch (direction)
            {
                case Direction.Up:
                    if (nextDirection == Direction.Left)
                        node.display.animManager.Change(Animations.TurnUpLeft);
                    else if (nextDirection == Direction.Right)
                        node.display.animManager.Change(Animations.TurnUpRight);
                    else if (nextDirection == Direction.Down)
                        node.display.animManager.Change(Animations.TurnUpDown);
                    break;
                case Direction.Down:
                    if (nextDirection == Direction.Left)
                        node.display.animManager.Change(Animations.TurnDownLeft);
                    else if (nextDirection == Direction.Right)
                        node.display.animManager.Change(Animations.TurnDownRight);
                    else if (nextDirection == Direction.Up)
                        node.display.animManager.Change(Animations.TurnDownUp);
                    break;
                case Direction.Left:
                    if (nextDirection == Direction.Up)
                        node.display.animManager.Change(Animations.TurnLeftUp);
                    else if (nextDirection == Direction.Down)
                        node.display.animManager.Change(Animations.TurnLeftDown);
                    else if (nextDirection == Direction.Right)
                        node.display.animManager.Change(Animations.TurnLeftRight);
                    break;
                case Direction.Right:
                    if (nextDirection == Direction.Up)
                        node.display.animManager.Change(Animations.TurnRightUp);
                    else if (nextDirection == Direction.Down)
                        node.display.animManager.Change(Animations.TurnRightDown);
                    else if (nextDirection == Direction.Left)
                        node.display.animManager.Change(Animations.TurnRightLeft);
                    break;
            }
            node.display.clip.Playing = true;
        }
    }
}
