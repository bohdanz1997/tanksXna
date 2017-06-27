using System;
using Ash.Core;
using Tanks.Resources;
using Tanks.Components;
using Microsoft.Xna.Framework;
using Tanks.Enums;
using Tanks.Tools;

namespace Tanks._Game
{
    partial class EntityFactory
    {
        public Entity CreateRedSpawner(float x, float y)
        {
            var sprite = new Sprite(textureManager.Get(Images.Instance.TileSet), cellSize, Layers.Crater);
            return CreateEntity()
                .Add(new Spawner())
                .Add(new Position(x, y))
                .Add(new Display(new Clip(sprite, new Vector2(600, 0))));
        }

        public Entity CreateGreenSpawner(float x, float y)
        {
            var sprite = new Sprite(textureManager.Get(Images.Instance.TileSet), cellSize, Layers.Crater);
            return CreateEntity()
                .Add(new Spawner())
                .Add(new Position(x, y))
                .Add(new Display(new Clip(sprite, new Vector2(630, 0))));
        }
    }
}
