using Ash.Core;
using System;
using Tanks.Components;
using Tanks.Enums;
using Tanks.Nodes;
using Tanks.Nodes.StateNodes;
using Tanks.Tools;
using Tanks.Tools.Extensions;

namespace Tanks.Systems.StateSystems
{
    class IdleSystem : SystemBase
    {
        Random r;
        GameField gameField;
        int cellSize;

        NodeList idleNodeList;
        NodeList targetNodeList;

        public IdleSystem(GameField gameField, int cellSize)
        {
            this.gameField = gameField;
            this.cellSize = cellSize;
            r = new Random();
        }

        public override void AddToGame(IGame game)
        {
            idleNodeList = game.GetNodeList<IdleNode>();
            targetNodeList = game.GetNodeList<TankCollisionNode>();
        }

        public override void RemoveFromGame(IGame game)
        {
            idleNodeList = null;
            targetNodeList = null;
        }

        public override void Update(float time)
        {
            idleNodeList.ForEach<IdleNode>(node =>
            {
                var position = node.position;
                var idle = node.idle;

                if (position.InCell(cellSize))
                {
                    TankCollisionNode target = null;
                    int minDistance = 100000;

                    //ищем самую ближнюю цель
                    targetNodeList.ForEach<TankCollisionNode>(targetNode =>
                    {
                        var distanceToTarget = position.position.DistanceTo(targetNode.position.position);

                        if (node.Entity == targetNode.Entity) return;
                        if (distanceToTarget > node.ai.visionDistance) return;
                        if (distanceToTarget > minDistance) return;
                        if (!General.ArrayContains(node.ai.typesForAttack, targetNode.Entity.Name)) return;

                        minDistance = distanceToTarget;
                        target = targetNode;
                        if (target.Entity.Has<AI>())
                        {

                        }
                    });

                    //если цель найдена
                    if (target != null)
                    {
                        var aiFSM = node.fsm.list["AiFSM"];
                        var distanceToTarget = position.position.DistanceTo(target.position.position);

                        //если цель в радиусе атаки и могу атаковать
                        if (node.ai.attackDistance > distanceToTarget
                        && node.ai.CanAttack(position.position, target.position.position, gameField, cellSize))
                        {
                            //переходим в режим атаки
                            aiFSM.ChangeState(States.Attack, () => Attack(node, target));
                        }
                        //иначе переходим в режим переследования
                        else
                        {
                            aiFSM.ChangeState(States.Pursuit, () => Pursuit(node, target));
                        }
                    }
                }

                idle.wayForMove = node.collision.collisionDetected ? 0 : idle.wayForMove - 1;
                if (idle.wayForMove <= 0)
                {
                    idle.wayForMove = r.Next(3, idle.maxCellsForMove) * cellSize;
                    var probability = r.Next(100);

                    //сгенерировать новое направление движения
                    switch (position.direction)
                    {
                        case Direction.Left:
                        case Direction.Right:
                            if (probability <= 40) position.nextDirection = Direction.Up;
                            else if (probability <= 80) position.nextDirection = Direction.Down;
                            else position.nextDirection = position.direction == Direction.Left ? Direction.Right : Direction.Left;
                            break;
                        case Direction.Up:
                        case Direction.Down:
                            if (probability <= 40) position.nextDirection = Direction.Left;
                            else if (probability <= 80) position.nextDirection = Direction.Right;
                            else position.nextDirection = position.direction == Direction.Up ? Direction.Down : Direction.Up;
                            break;
                    }
                }
            });
        }

        private void Pursuit(IdleNode node, TankCollisionNode target)
        {
            var path = node.ai.GetPath(node.position.position, target.position.position, gameField, cellSize);
            node.ai.path = path;
            node.ai.target = target;
        }

        private void Attack(IdleNode node, TankCollisionNode target)
        {
            node.ai.target = target;
            node.ai.path.Clear();
            node.position.nextDirection = node.ai.GetDirectionToTarget(node.position.position, target.position.position);
        }
    }
}
