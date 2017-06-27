using Tanks.Nodes;
using Ash.Tools;

namespace Tanks.Systems
{
    class AnimationSystem : ListIteratingSystem<AnimationNode>
    {
        public AnimationSystem()
        {
            NodeUpdate += HandleUpdate;
        }

        private void HandleUpdate(AnimationNode node, float time)
        {
            node.display.animManager.Update(time);
        }
    }
}
