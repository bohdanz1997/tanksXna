using Ash.Tools;
using Tanks._Game;
using Tanks.Nodes;

namespace Tanks.Systems
{
    class HealthControlSystem : ListIteratingSystem<HealthNode>
    {
        EntityFactory creator;
        
        public HealthControlSystem(EntityFactory creator)
        {
            this.creator = creator;
            NodeUpdate += Update;
        }

        private void Update(HealthNode node, float time)
        {
            var health = node.health;
            if (health.current <= 0)
            {
                creator.DestroyEntity(node.Entity);
            }
        }
    }
}
