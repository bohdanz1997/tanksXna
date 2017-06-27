using Tanks.Nodes;
using Tanks.Enums;
using Tanks.Tools.Extensions;
using Tanks.Tools;
using Ash.Tools;

namespace Tanks.Systems
{
    class FieldSystem : ListIteratingSystem<MovementFSMNode>
    {
        GameField gameField;
        int cellSize;

        public FieldSystem(GameField gameField, int cellSize)
        {
            this.gameField = gameField;
            this.cellSize = cellSize;
            NodeUpdate += Update;
        }

        private void Update(MovementFSMNode node, float time)
        {
            var position = node.position;
            var cellPosition = position.position.Divide(cellSize);            
            var motionFSM = node.fsm.list["MotionFSM"];

            switch (gameField.GetCell(cellPosition).Ground)
            {
                case Ground.Sand: motionFSM.ChangeState(States.Slow); break;
                case Ground.Ground: motionFSM.ChangeState(States.Normal); break;
                case Ground.Asphalt: motionFSM.ChangeState(States.Fast); break;
            }
        }
    }
}
