using Ash.Core;
using Tanks.Nodes;
using Tanks.Tools.Extensions;
using Tanks.Enums;
using static Tanks.Tools.Extensions.General;
using Tanks._Game;
using System;
using Microsoft.Xna.Framework;

namespace Tanks.Systems
{
    class BulletCollisionSystem : SystemBase
    {
        Random rand;
        EntityFactory creator;
        int cellSize;

        NodeList bulletNodeList;
        NodeList collisionNodeList;

        public BulletCollisionSystem(EntityFactory creator, int cellSize)
        {
            rand = new Random();
            this.cellSize = cellSize;
            this.creator = creator;
        }

        public override void AddToGame(IGame game)
        {
            bulletNodeList = game.GetNodeList<BulletCollisionNode>();
            collisionNodeList = game.GetNodeList<CollisionNode>();
        }

        public override void RemoveFromGame(IGame game)
        {
            collisionNodeList = null;
            bulletNodeList = null;
        }

        public override void Update(float time)
        {
            bulletNodeList.ForEach<BulletCollisionNode>(bulletNode =>
            {
                var bulletPosition = bulletNode.position;
                var bulletCollision = bulletNode.collision;
                var bulletHealth = bulletNode.health;
                var bulletDamage = bulletNode.damage;
                var bulletActionAfterDeath = bulletNode.actionAfterDeath;
                bulletCollision.bounds.Location = bulletPosition.position;

                collisionNodeList.ForEach<CollisionNode>(node =>
                {
                    var position = node.position;
                    var collision = node.collision;
                    var health = node.health;
                    var damage = node.damage;
                    var actionAfterDeath = node.actionAfterDeath;

                    //не проводим обработку столкновения
                    //если нету столкновения на расстоянии collisionRadius
                    if (!bulletCollision.CheckCollision(position.position, collision.collisionRadius)) return;

                    //если типа node нету в списке типов для столкновения
                    if (!ArrayContains(bulletCollision.collidesWith, node.Entity.Name)) return;

                    //если две ссылки указывают на один и тот же Entity пули
                    if (bulletNode.Entity == node.Entity) return;

                    //если node это создатель пули 
                    if (bulletNode.bullet.owner == node.Entity) return;
                    
                    //если случайное число в пределах вероятности взрыва
                    //тогда отнимаем все hp у танка
                    if (Mathf.InRange(rand.Next(101), 0, health.chanceOfFire))
                    {
                        health.current = 0;
                    }
                    else
                    {
                        health.current -= bulletDamage.value;
                    }

                    //отнимаем hp у пули
                    bulletHealth.current -= damage.value;

                    if (EntityTypeEquals(EntityType.Wall, node.Entity.Name))
                    {
                        ChangeWallFrame(node);
                    }

                    if (!EntityTypeEquals(EntityType.Bullet, node.Entity.Name))
                    {
                        bulletActionAfterDeath.callback?.Invoke();
                    }

                    if (health.current <= 0)
                    {
                        actionAfterDeath.callback?.Invoke();
                    }                    
                });
            });
        }

        private void ChangeWallFrame(CollisionNode node)
        {
            switch ((int)node.health.current)
            {
                case 4: node.display.clip.SetFrame(0); break;
                case 3: node.display.clip.SetFrame(1); break;
                case 2: node.display.clip.SetFrame(2); break;
                case 1: node.display.clip.SetFrame(3); break;
            }
        }
    }
}
