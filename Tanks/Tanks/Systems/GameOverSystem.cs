using Ash.Core;
using Tanks.Nodes;
using Tanks._Game;

namespace Tanks.Systems
{
    class GameOverSystem : SystemBase
    {
        NodeList entities;
        NodeList gameOver;
        
        EntityFactory creator;

        public GameOverSystem(EntityFactory creator)
        {
            this.creator = creator;
        }

        private void Update(PositionNode node, float time)
        {

        }

        public override void AddToGame(IGame game)
        {
            entities = game.GetNodeList<PositionNode>();
            gameOver = game.GetNodeList<GameOverNode>();
        }

        public override void RemoveFromGame(IGame game)
        {
            entities = null;
            gameOver = null;
        }

        public override void Update(float time)
        {

        }
    }
}
