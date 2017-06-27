using Tanks.Nodes.StateNodes;
using Tanks.Enums;
using Ash.Tools;

namespace Tanks.Systems.StateSystems
{
    class TurnSystem : ListIteratingSystem<TurnNode>
    {
        public TurnSystem()
        {
            NodeUpdate += Update;
        }
        
        private void Update(TurnNode node, float time)
        {
            var turn = node.turn;
            var position = node.position;
            var display = node.display;

            if (turn.nextDirection == Direction.None)
                turn.nextDirection = position.nextDirection;
            
            if (!display.animManager.Current.Playing)
            {
                var moveFSM = node.fsm.list["MoveFSM"];                
                moveFSM.ChangeState(States.Stand, () => Stand(node));                
            }
        }

        private void Stand(TurnNode node)
        {
            var turn = node.turn;
            var position = node.position;
            var display = node.display;

            position.direction = turn.nextDirection;
            display.animManager.Current.Reset();

            if (turn.nextDirection == Direction.Up)
                display.animManager.Change(Animations.StandUp);
            else if (turn.nextDirection == Direction.Down)
                display.animManager.Change(Animations.StandDown);
            else if (turn.nextDirection == Direction.Left)
                display.animManager.Change(Animations.StandLeft);
            else if (turn.nextDirection == Direction.Right)
                display.animManager.Change(Animations.StandRight);

            turn.nextDirection = Direction.None;
        }
    }
}
