using Tanks.Nodes;
using Microsoft.Xna.Framework;
using Ash.Tools;

namespace Tanks.Systems
{
    class HealthBarControlSystem : ListIteratingSystem<HealthBarNode>
    {
        public HealthBarControlSystem()
        {
            NodeUpdate += Update;
        }

        private void Update(HealthBarNode node, float time)
        {
            var display = node.display;
            var health = node.health;
            
            float percent = Mathf.ToPercent(health.current, health.max);
            float width = Mathf.FromPercent(percent, node.healthBar.maxWidth);

            display.clip.Width = Mathf.Round(width);
        }
    }
}
