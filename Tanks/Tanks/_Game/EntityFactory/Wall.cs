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
        public Entity CreateWall(float x, float y)
        {
            var entity = CreateEntity(EntityType.Wall);
            var sprite = new Sprite(textureManager.Get(Images.Instance.TileSet), cellSize, Layers.Wall);
            var fsm = new EntityStateMachine(entity);
            var position = new Position(x, y);
            var randForWall = r.Next(100) > 50 ? 180 : 300;
            AddCellToMap(x, y, CellType.Wall);
            
            return entity
                .Add(new Health(4))
                .Add(new Collision(cellSize, 15))
                .Add(new Display(new Clip(sprite, new Vector2(randForWall, 0), 4)))
                .Add(new Wall())
                .Add(new FSM(fsm))
                .Add(new Damage(100))
                .Add(position)
                .Add(new ActionAfterDeath(() =>
                {
                    var cellPos = position.position.Divide(cellSize.X);
                    gameField.ClearCell(cellPos);
                    gameField.GetCell(cellPos).Entity.Get<FSM>().list["Default"].ChangeState(States.Destroyed);
                    CreateEnemyExplosion(position.position);
                    CreateQuake();
                }));
        }

        public Entity CreateSteelWall(float x, float y, int offsetX, int cellType)
        {
            var entity = CreateEntity(EntityType.SteelWall);
            var sprite = new Sprite(textureManager.Get(Images.Instance.TileSet), cellSize, Layers.Wall);
            var fsm = new EntityStateMachine(entity);
            var position = new Position(x, y);
            AddCellToMap(x, y, cellType);
            
            return entity
                .Add(new Wall())
                .Add(new Health(10000))
                .Add(new Display(new Clip(sprite, new Vector2(offsetX, 0))))
                .Add(new Collision(cellSize, 15))
                .Add(new FSM(fsm))
                .Add(new Damage(100))
                .Add(new Position(x, y))
                .Add(new ActionAfterDeath(() =>
                {
                    var cellPos = position.position.Divide(cellSize.X);
                    gameField.ClearCell(position.position.Divide(cellSize.X));
                    gameField.GetCell(cellPos).Entity.Get<FSM>().list["Default"].ChangeState(States.Destroyed);
                    CreateEnemyExplosion(position.position);
                    CreateQuake();
                }));
        }
    }
}
