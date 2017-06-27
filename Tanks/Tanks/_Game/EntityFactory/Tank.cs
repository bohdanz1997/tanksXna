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
using Tanks._Game.EntityViews;

namespace Tanks._Game
{
    partial class EntityFactory
    {
        public Entity CreateTank(float x, float y, int level = 0)
        {
            var entity = CreateEntity(EntityType.RedTank);
            var position = new Position(x, y, AntEnum.Parse<Direction>(r.Next(1, 5).ToString()));
            var myvars = vars.LightTank[level];
            var view = new TankView(myvars[5], 0, position.direction);

            var listFsm = new Dictionary<string, EntityStateMachine>();
            var moveFSM = new EntityStateMachine(entity);
            var aiFSM = new EntityStateMachine(entity);
            var motionFSM = new EntityStateMachine(entity);

            moveFSM.CreateState(States.Stand).Add<Stand>().WithInstance(new Stand());
            moveFSM.CreateState(States.Turn).Add<Turn>().WithInstance(new Turn());
            moveFSM.CreateState(States.Walk).Add<Walk>().WithInstance(new Walk());
            moveFSM.ChangeState(States.Stand);

            aiFSM.CreateState(States.Idle).Add<Idle>().WithInstance(new Idle(7));
            aiFSM.CreateState(States.Pursuit).Add<Pursuit>().WithInstance(new Pursuit());
            aiFSM.CreateState(States.Attack).Add<Attack>().WithInstance(new Attack());
            aiFSM.ChangeState(States.Idle);
            
            var addSpeed = Mathf.FromPercent(0.5f, myvars[0]);

            motionFSM.CreateState(States.Fast).Add<Controls>()
                .WithInstance(new Controls(new Vector2(myvars[0] + addSpeed)));
            motionFSM.CreateState(States.Slow).Add<Controls>()
                .WithInstance(new Controls(new Vector2(myvars[0] - addSpeed)));
            motionFSM.CreateState(States.Normal).Add<Controls>()
                .WithInstance(new Controls(new Vector2(myvars[0])));
            motionFSM.ChangeState(States.Normal);

            listFsm.Add("MoveFSM", moveFSM);
            listFsm.Add("AiFSM", aiFSM);
            listFsm.Add("MotionFSM", motionFSM);

            entity
                .Add(position)
                .Add(view.GetView())
                .Add(new Health(myvars[2], myvars[4]))
                .Add(new Damage(100))
                .Add(new Tank())
                .Add(new FSM(listFsm))
                .Add(new AI(360, 210, new[] { EntityType.GreenTank }))
                .Add(new Gun(myvars[1], myvars[3]))
                .Add(new Collision(cellSize, 15))
                .Add(new Motion(Vector2.Zero, r.Next(CellType.Segment, 10000)))
                .Add(new ActionAfterDeath(() =>
                {
                    gameField.ClearCell(position.prevCell);
                    CreateEnemyExplosion(position.position);
                    CreateQuake();
                }));

            CreateRedOverlay(entity);
            CreateGreenHealthBar(entity);

            return entity;
        }
    }
}
