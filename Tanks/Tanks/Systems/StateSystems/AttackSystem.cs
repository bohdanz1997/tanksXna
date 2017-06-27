using Ash.Tools;
using Microsoft.Xna.Framework;
using Tanks._Game;
using Tanks.Components;
using Tanks.Components.StateComponents;
using Tanks.Enums;
using Tanks.Nodes;
using Tanks.Nodes.StateNodes;
using Tanks.Tools.Extensions;

namespace Tanks.Systems.StateSystems
{
    class AttackSystem : ListIteratingSystem<AttackNode>
    {
        EntityFactory creator;
        int cellSize;

        public AttackSystem(EntityFactory creator, int cellSize)
        {
            NodeUpdate += Update;
            this.creator = creator;
            this.cellSize = cellSize;
        }

        private void Update(AttackNode node, float time)
        {
            var position = node.position;

            if (position.InCell(cellSize))
            {
                var target = node.ai.target as TankCollisionNode;

                if (target.Entity.Get<Health>().current <= 0)
                {
                    //переходим в режим бездействия
                    var aiFSM = node.fsm.list["AiFSM"];
                    aiFSM.ChangeState(States.Idle, () => Idle(node));
                    return;
                }

                if (position.position.DistanceTo(target.position.position) > node.ai.attackDistance
                || (position.X != target.position.X && position.Y != target.position.Y))
                {
                    //переходим в режим бездействия
                    var aiFSM = node.fsm.list["AiFSM"];
                    aiFSM.ChangeState(States.Idle, () => Idle(node));
                }
                node.gun.timeSinceLastShot -= time;
                if (!position.IsTurn(position.nextDirection))
                {
                    if (node.gun.timeSinceLastShot <= 0)
                    {
                        node.gun.timeSinceLastShot = node.gun.minimumShotInterval;
                        creator.CreateBullet(node.Entity, (int)node.gun.bulletLevel);
                    }
                }

                if (!node.Entity.Has<Turn>())
                {
                    node.motion.velocity = Vector2.Zero;
                }
            }          
        }

        private void Idle(AttackNode node)
        {
            node.ai.path.Clear();
            node.ai.target = null;
        }
    }
}
