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
        public Entity CreateBullet(Entity owner, int level = 0)
        {
            var myvars = vars.Bullet[level];
            var entity = CreateEntity(AntEnum.Parse<EntityType>(myvars[0].ToString()));
            var clip = new Clip(new Sprite(textureManager.Get(Images.Instance.GameTextures), cellSize, Layers.Bullet), new Vector2(level * 120, 138), 4);
            var position = owner.Get<Position>();
            var bulletPosition = new Position(position.X, position.Y);
            var team = General.EntityTypeEquals(EntityType.GreenTank, owner.Name) ? EntityType.RedTank : EntityType.GreenTank;
            var velocity = Vector2.Zero;
            var speed = myvars[1];

            var offset = new Vector2(level == 0 || level == 3 ? 0 : level == 1 || level == 4 ? 360 : 720, 0);

            switch (position.direction)
            {
                case Direction.Up: clip.SetFrame(0); velocity.Y = -speed; break;
                case Direction.Down: clip.SetFrame(2); velocity.Y = speed; break;
                case Direction.Left: clip.SetFrame(3); velocity.X = -speed; break;
                case Direction.Right: clip.SetFrame(1); velocity.X = speed; break;
            }

            return entity
                .Add(bulletPosition)
                .Add(new Health(myvars[2]))
                .Add(new Damage(myvars[2]))
                .Add(new Motion(velocity))
                .Add(new Collision(cellSize, 5, new[] { EntityType.Wall, EntityType.SteelWall, EntityType.Tree, EntityType.Bullet, team }))
                .Add(new Bullet(owner))
                .Add(new Display(clip, -3, -3))
                .Add(new DeathOnTime(10000))
                .Add(new ActionAfterDeath(() => CreateBuletExplosion(bulletPosition.position, offset)));
        }

    }
}
