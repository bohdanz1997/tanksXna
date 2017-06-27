using System;
using Ash.Core;
using Ash.FSM;
using Tanks.Resources;
using Tanks.Components;
using Microsoft.Xna.Framework;
using Tanks.Enums;
using System.Collections.Generic;
using Tanks.Components.StateComponents;
using Tanks._Game.EntityViews;

namespace Tanks._Game
{
    partial class EntityFactory
    {
        public Entity CreatePlayer(float x, float y)
        {
            var entity = CreateEntity(EntityType.GreenTank);
            var position = new Position(x, y, Direction.Left);
            var view = new TankView(32, 36, position.direction);

            var listFsm = new Dictionary<string, EntityStateMachine>();
            var moveFSM = new EntityStateMachine(entity);
            var motionFSM = new EntityStateMachine(entity);

            moveFSM.CreateState(States.Stand).Add<Stand>().WithInstance(new Stand());
            moveFSM.CreateState(States.Turn).Add<Turn>().WithInstance(new Turn());
            moveFSM.CreateState(States.Walk).Add<Walk>().WithInstance(new Walk());
            moveFSM.ChangeState(States.Stand);

            var speed = 1.5f;
            var addSpeed = Mathf.FromPercent(0.5f, speed);

            motionFSM.CreateState(States.Fast).Add<Controls>()
                .WithInstance(new Controls(
                Config.Instance.MoveUp,
                Config.Instance.MoveDown,
                Config.Instance.MoveLeft,
                Config.Instance.MoveRight,
                new Vector2(speed + addSpeed)));
            
            motionFSM.CreateState(States.Slow).Add<Controls>()
                .WithInstance(new Controls(
                Config.Instance.MoveUp,
                Config.Instance.MoveDown,
                Config.Instance.MoveLeft,
                Config.Instance.MoveRight,
                new Vector2(speed - addSpeed)));
            
            motionFSM.CreateState(States.Normal).Add<Controls>()
                .WithInstance(new Controls(
                Config.Instance.MoveUp,
                Config.Instance.MoveDown,
                Config.Instance.MoveLeft,
                Config.Instance.MoveRight,
                new Vector2(speed)));
            motionFSM.ChangeState(States.Normal);

            listFsm.Add("MoveFSM", moveFSM);
            listFsm.Add("MotionFSM", motionFSM);

            entity
                .Add(position)
                .Add(view.GetView())
                .Add(new Health(100, 0))
                .Add(new Damage(100))
                .Add(new Player())
                .Add(new FSM(listFsm))
                .Add(new Gun(5, 500))
                .Add(new GunControls(Config.Instance.GunTrigger))
                .Add(new Motion(Vector2.Zero, r.Next(CellType.Segment, 10000)))
                .Add(new Collision(cellSize, 15))
                .Add(new ActionAfterDeath(() =>
                {
                    gameField.ClearCell(position.prevCell);
                    CreateEnemyExplosion(position.position);
                    CreateQuake();
                }));

            CreateGreenOverlay(entity);
            CreateGreenHealthBar(entity);

            return entity;
        }
    }
}
