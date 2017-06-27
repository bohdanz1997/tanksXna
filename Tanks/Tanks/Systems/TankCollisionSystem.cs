using Tanks.Nodes;
using System;
using Tanks.Tools.Extensions;
using Tanks.Enums;
using Tanks.Tools;
using Tanks._Game;
using Ash.Tools;

namespace Tanks.Systems
{
    class TankCollisionSystem : ListIteratingSystem<TankCollisionNode>
    {
        EntityFactory creator;
        GameField gameField;
        int cellSize;
        
        public TankCollisionSystem(EntityFactory creator, GameField gameField, int cellSize)
        {
            this.gameField = gameField;
            this.cellSize = cellSize;
            this.creator = creator;
            NodeUpdate += Update;
        }
        
        private void Update(TankCollisionNode node, float time)
        {
            var controls = node.controls;
            var position = node.position;
            var collision = node.collision;
            var motion = node.motion;

            var fieldPos = position.position.Divide(cellSize);

            //фикс бага позиции связаный с округлением до int
            if (position.position.X < 0) fieldPos.X = -1;
            if (position.position.Y < 0) fieldPos.Y = -1;

            collision.collisionDetected = false;

            switch (position.direction)
            {
                case Direction.Up:
                    if (CheckFieldCell(fieldPos.X, fieldPos.Y, motion.segment))
                    {
                        position.Y += Math.Abs(controls.acceleration.Y);
                        collision.collisionDetected = true;
                    }
                    break;
                case Direction.Down:
                    if (position.Y / cellSize > fieldPos.Y)
                        if (CheckFieldCell(fieldPos.X, fieldPos.Y + 1, motion.segment))
                        {
                            position.Y -= Math.Abs(controls.acceleration.Y);
                            collision.collisionDetected = true;
                        }
                    break;
                case Direction.Left:
                    if (CheckFieldCell(fieldPos.X, fieldPos.Y, motion.segment))
                    {
                        position.X += Math.Abs(controls.acceleration.X);
                        collision.collisionDetected = true;
                    }
                    break;
                case Direction.Right:
                    if (position.X / cellSize > fieldPos.X)
                        if (CheckFieldCell(fieldPos.X + 1, fieldPos.Y, motion.segment))
                        {
                            position.X -= Math.Abs(controls.acceleration.X);
                            collision.collisionDetected = true;
                        }
                    break;
            }
        }

        private bool CheckFieldCell(float x, float y, int seg)
        {
            return (!gameField.IsCellEmpty(x, y) && gameField.GetCell(x, y).Type != seg);
        }
    }
}
