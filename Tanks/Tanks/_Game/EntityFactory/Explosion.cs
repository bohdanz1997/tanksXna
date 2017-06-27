using Ash.Core;
using Ash.FSM;
using Tanks.Resources;
using Tanks.Components;
using Microsoft.Xna.Framework;
using Tanks.Components.StateComponents;
using Tanks.Enums;
using Tanks.Tools.Extensions;
using System.Collections.Generic;
using Tanks.Tools;

namespace Tanks._Game
{
    partial class EntityFactory
    {
        public Entity CreateBuletExplosion(Vector2 pos, Vector2 offset)
        {
            var sprite = new Sprite(textureManager.Get(Images.Instance.BulletExplosion), cellSize, Layers.Explosion);
            return CreateEntity()
                .Add(new Position(pos.X, pos.Y))
                .Add(new DeathOnTime(768))
                .Add(new Display(new Clip(sprite, offset, 12, 48, true)));
        }

        public Entity CreateEnemyExplosion(Vector2 pos)
        {
            var sprite = new Sprite(textureManager.Get(Images.Instance.EnemyExplosion), new Vector2(80), Layers.Explosion);
            return CreateEntity()
                .Add(new Position(pos.X, pos.Y))
                .Add(new DeathOnTime(768))
                .Add(new Display(new Clip(sprite, Vector2.Zero, 10, 64, true), -25, -25));
        }
    }
}
