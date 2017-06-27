using System;
using Ash.Core;
using Ash.FSM;
using Tanks.Resources;
using Tanks.Components;
using Microsoft.Xna.Framework;
using Tanks.Enums;
using Tanks.Tools;
using Tanks.Tools.Extensions;

namespace Tanks._Game
{
    partial class EntityFactory
    {
        public Entity CreateTile(float x, float y, int gid, Ground g)
        {
            var offsetX = gid > 66 ? gid * cellSize.X - 2010 : gid * cellSize.X;
            var offsetY = gid > 66 ? 30 : 0;
            var destroyedPos = Vector2.Zero;
            var randForDestroyed = r.Next(3) * cellSize.X;

            if (g == Ground.Ground)
            {
                destroyedPos = new Vector2(510 + randForDestroyed, 0);
            }
            else if (g == Ground.Asphalt)
            {
                destroyedPos = new Vector2(1560 + randForDestroyed, 30);
            }
            else if (g == Ground.Sand)
            {
                destroyedPos = new Vector2(630 + randForDestroyed, 30);
            }

            var sprite = new Sprite(textureManager.Get(Images.Instance.TileSet), cellSize, Layers.Ground);
            var entity = CreateEntity();
            var fsm = new EntityStateMachine(entity);
            fsm.CreateState(States.Alive)
                .Add<Display>().WithInstance(new Display(new Clip(sprite, new Vector2(offsetX, offsetY))));
            fsm.CreateState(States.Destroyed)
                .Add<Display>().WithInstance(new Display(new Clip(sprite, destroyedPos)));
            fsm.ChangeState(States.Alive);

            return entity
                .Add(new FSM(fsm))
                .Add(new Position(x, y));
        }

        public Entity CreateTree(float x, float y)
        {
            var entity = CreateEntity(EntityType.Tree);
            var sprite = new Sprite(textureManager.Get(Images.Instance.TileSet), cellSize, Layers.Wall);
            var fsm = new EntityStateMachine(entity);
            var position = new Position(x, y);
            var rand = r.Next(3) * cellSize.X;
            AddCellToMap(x, y, CellType.Tree);

            return entity
                .Add(position)
                .Add(new FSM(fsm))
                .Add(new Damage(1))
                .Add(new Health(1))
                .Add(new Position(x, y))
                .Add(new Collision(cellSize, 15))
                .Add(new ActionAfterDeath(() => gameField.ClearCell(position.position.Divide(cellSize.X))))
                .Add(new Display(new Clip(sprite, new Vector2(0 + rand, 0))));
        }

        public Entity CreateObstacle(float x, float y)
        {
            var sprite = new Sprite(textureManager.Get(Images.Instance.TileSet), cellSize, Layers.Wall);
            var rand = r.Next(3) * cellSize.X;
            AddCellToMap(x, y, CellType.Obstacle);

            return CreateEntity(EntityType.Obstacle)
                .Add(new Position(x, y))
                .Add(new Collision(cellSize, 15))
                .Add(new Display(new Clip(sprite, new Vector2(90 + rand, 0))));
        }

    }
}
