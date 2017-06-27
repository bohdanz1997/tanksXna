using Ash.Tools;
using Tanks.Nodes;
using Tanks.Tools;
using Tanks.Tools.Extensions;

namespace Tanks.Systems
{
    class FieldSegmentSystem : ListIteratingSystem<TankCollisionNode>
    {
        GameField gameField;
        int cellSize;

        public FieldSegmentSystem(GameField gameField, int cellSize)
        {
            this.gameField = gameField;
            this.cellSize = cellSize;
            NodeUpdate += Update;
        }

        private void Update(TankCollisionNode node, float time)
        {
            var position = node.position;
            var motion = node.motion;

            if (position.InCell(cellSize))
            {
                var currentCell = position.position.Divide(cellSize);
                gameField.ClearCell(position.prevCell);
                gameField.SetCell(currentCell, motion.segment);
                position.prevCell = currentCell;
            }
        }
    }
}
