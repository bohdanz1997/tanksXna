using Ash.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tanks.Nodes;

namespace Tanks.Systems
{
    class HudRenderSystem : SystemBase
    {
        SpriteBatch spriteBatch;

        NodeList hudNodeList;
        NodeList gameNodeList;

        public HudRenderSystem(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        private void Update(HudNode node, float time)
        {
        }

        public override void AddToGame(IGame game)
        {
            gameNodeList = game.GetNodeList<GameNode>();
            hudNodeList = game.GetNodeList<HudNode>();
        }

        public override void RemoveFromGame(IGame game)
        {
            gameNodeList = null;
            hudNodeList = null;
        }

        public override void Update(float time)
        {
            var hudNode = (HudNode)hudNodeList.Head;
            var gameNode = (GameNode)gameNodeList.Head;
            var gameState = gameNode.state;
            
            hudNode.hud.text = $@"Money: {gameState.money}
Wave:  {gameState.waveIndex + 1}
Score: {gameState.score}";

            spriteBatch.DrawString(hudNode.hud.font, hudNode.hud.text, hudNode.position.position - new Vector2(1), Color.White);
            spriteBatch.DrawString(hudNode.hud.font, hudNode.hud.text, hudNode.position.position, Color.Black);
        }
    }
}
