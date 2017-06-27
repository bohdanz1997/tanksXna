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
        public Entity CreateGreenOverlay(Entity entity)
        {
            var sprite = new Sprite(textureManager.Get(Images.Instance.Overlay), new Vector2(40), Layers.Overlay);
            return CreateEntity()
                .Add(entity.Get<Health>())
                .Add(entity.Get<Position>())
                .Add(new Display(new Clip(sprite, new Vector2(0)), -5, -5));
        }

        public Entity CreateRedOverlay(Entity entity)
        {
            var sprite = new Sprite(textureManager.Get(Images.Instance.Overlay), new Vector2(40), Layers.Overlay);
            return CreateEntity()
                .Add(entity.Get<Health>())
                .Add(entity.Get<Position>())
                .Add(new Display(new Clip(sprite, new Vector2(40, 0)), -5, -5));
        }

        public Entity CreateGreenHealthBar(Entity entity)
        {
            var sprite1 = new Sprite(textureManager.Get(Images.Instance.HealthBack), new Vector2(28, 4), Layers.HealthBar2);
            var sprite2 = new Sprite(textureManager.Get(Images.Instance.HealthFront), new Vector2(26, 2), Layers.HealthBar1);

            CreateEntity()
                .Add(entity.Get<Health>())
                .Add(entity.Get<Position>())
                .Add(new Display(new Clip(sprite1, Vector2.Zero), 0, -5));
            
            return CreateEntity()
                .Add(entity.Get<Health>())
                .Add(entity.Get<Position>())
                .Add(new HealthBar(26))
                .Add(new Display(new Clip(sprite2, new Vector2(0, 0)), 1, -4));
        }

        public Entity CreateRedHealthBar(Entity entity)
        {
            var sprite1 = new Sprite(textureManager.Get(Images.Instance.HealthBack), new Vector2(28, 4), Layers.HealthBar2);
            var sprite2 = new Sprite(textureManager.Get(Images.Instance.HealthFront), new Vector2(26, 2), Layers.HealthBar1);

            CreateEntity()
                .Add(entity.Get<Health>())
                .Add(entity.Get<Position>())
                .Add(new Display(new Clip(sprite1, Vector2.Zero), 0, -5));

            return CreateEntity()
                .Add(entity.Get<Health>())
                .Add(entity.Get<Position>())
                .Add(new HealthBar(26))
                .Add(new Display(new Clip(sprite2, new Vector2(0, 2)), 1, -4));
        }
    }
}
