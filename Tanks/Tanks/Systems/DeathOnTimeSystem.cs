using Tanks.Nodes;
using Tanks._Game;
using Ash.Tools;

namespace Tanks.Systems
{
    class DeathOnTimeSystem : ListIteratingSystem<DeathOnTimeNode>
    {
        EntityFactory creator;
        
        public DeathOnTimeSystem(EntityFactory creator)
        {
            this.creator = creator;
            NodeUpdate += Update;
        }

        private void Update(DeathOnTimeNode node, float time)
        {
            var deathOnTime = node.deathOnTime;
            deathOnTime.timeRemaining -= time;
            if (deathOnTime.timeRemaining <= 0)
            {
                deathOnTime.timeRemaining = deathOnTime.maxTime;
                deathOnTime.callback?.Invoke();
                creator.DestroyEntity(node.Entity);
            }
        }
    }
}
