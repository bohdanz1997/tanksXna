using Ash.Core;
using Microsoft.Xna.Framework;
using Tanks.Enums;
using Tanks.Nodes;
using Tanks.Nodes.StateNodes;
using Tanks.Tools;
using Tanks.Tools.Extensions;

namespace Tanks.Systems.StateSystems
{
    class PursuitSystem : SystemBase
    {
        NodeList pursuitNodeList;

        int cellSize;
        GameField gameField;

        public PursuitSystem(GameField gameField, int cellSize)
        {
            this.gameField = gameField;
            this.cellSize = cellSize;
        }

        public override void AddToGame(IGame game)
        {
            pursuitNodeList = game.GetNodeList<PursuitNode>();
        }

        public override void RemoveFromGame(IGame game)
        {
            pursuitNodeList = null;
        }
        
        public override void Update(float time)
        {
            pursuitNodeList.ForEach<PursuitNode>(node =>
            {
                var position = node.position;
                var target = node.ai.target as TankCollisionNode;
                var aiFSM = node.fsm.list["AiFSM"];

                var distanceToTarget = position.position.DistanceTo(target.position.position);

                //если могу атаковать
                if (node.ai.attackDistance > distanceToTarget
                && node.ai.CanAttack(position.position, target.position.position, gameField, cellSize))
                {
                    //переходим в режим атаки
                    aiFSM.ChangeState(States.Attack, () => Attack(node, target));                    
                }

                //если путь к цели уже пройден
                else if (node.ai.path.Count == 0)
                {
                    //создаем новый путь
                    //node.ai.nextPos = Vector2.Zero;
                    node.ai.path = node.ai.GetPath(position.position, target.position.position, gameField, cellSize);
                }

                //если путь к цели еще не пройден
                else if (node.ai.path.Count > 0)
                {
                    //если досигли позиции из пути
                    //if (position.position == node.ai.nextPos || node.ai.nextPos == Vector2.Zero)
                    //{
                    //    //берем следующую
                    //    var nextItem = node.ai.path.Pop();
                    //    position.nextDirection = nextItem.Direction;
                    //    node.ai.nextPos = nextItem.Point;
                    //    node.motion.velocity = Vector2.Zero;
                    //}

                    var curCell = position.position.Divide(cellSize);
                    if (position.direction == Direction.Left && position.position.X / cellSize > curCell.X)
                        curCell.X += 1;
                    if (position.direction == Direction.Up && position.position.Y / cellSize > curCell.Y)
                        curCell.Y += 1;

                    //если достигли следующей клетки
                    if (curCell != node.ai.nextPos)
                    {
                        var nextItem = node.ai.path.Pop();
                        position.nextDirection = nextItem.Direction;
                        node.motion.velocity = Vector2.Zero;
                    }
                    node.ai.nextPos = curCell;
                }
            });
        }

        private void Attack(PursuitNode node, TankCollisionNode target)
        {
            node.ai.path.Clear();
            //node.ai.nextPos = Vector2.Zero;
            node.position.nextDirection = node.ai.GetDirectionToTarget(node.position.position, target.position.position);
        }
    }
}
