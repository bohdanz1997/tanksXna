using Ash.Core;
using Tanks.Nodes;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tanks.Systems
{
    class RenderSystem : SystemBase
    {
        SpriteBatch spriteBatch;
        NodeList renderList;
        NodeList cameraList;

        int cellSize;

        public RenderSystem(SpriteBatch spriteBatch, int cellSize)
        {
            this.cellSize = cellSize;
            this.spriteBatch = spriteBatch;
        }

        public override void AddToGame(IGame game)
        {
            renderList = game.GetNodeList<RenderNode>();
            cameraList = game.GetNodeList<CameraFocusNode>();
        }

        public override void RemoveFromGame(IGame game)
        {
            renderList = null;
            cameraList = null;
        }

        public override void Update(float time)
        {
            var cameraNode = (CameraFocusNode)cameraList.Head;
            var camera = cameraNode.camera;

            renderList.ForEach<RenderNode>(renderNode =>
            {
                var display = renderNode.display;
                var position = renderNode.position;

                display.clip.Position = position.position + display.offset;
                var nodeRect = new RectangleF(position.X, position.Y, cellSize, cellSize);
                
                if (camera.camera.GetBounds().Intersects(nodeRect))
                {
                    display.clip.Draw(spriteBatch);
                }
            });
        }
    }
}
