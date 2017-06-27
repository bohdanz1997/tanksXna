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
        public Entity CreateTower(float x, float y, int level = 0)
        {
            var entity = CreateEntity(EntityType.RedTank);
            var listFsm = new Dictionary<string, EntityStateMachine>();
            var moveFSM = new EntityStateMachine(entity);
            var aiFSM = new EntityStateMachine(entity);
            var position = new Position(x, y, AntEnum.Parse<Direction>(r.Next(1, 5).ToString()));
            var myvars = vars.Tower[level];
            var view = new TowerView(myvars[5], position.direction);

            moveFSM.CreateState(States.Stand).Add<Stand>().WithInstance(new Stand());
            moveFSM.CreateState(States.Turn).Add<Turn>().WithInstance(new Turn());
            moveFSM.ChangeState(States.Stand);

            aiFSM.CreateState(States.Idle).Add<Idle>().WithInstance(new Idle(10));
            aiFSM.CreateState(States.Attack).Add<Attack>().WithInstance(new Attack());
            aiFSM.ChangeState(States.Idle);

            listFsm.Add("MoveFSM", moveFSM);
            listFsm.Add("AiFSM", aiFSM);

            entity
                .Add(position)
                .Add(view.GetView())
                .Add(new Health(myvars[2], myvars[4]))
                .Add(new Damage(100))
                .Add(new Tank())
                .Add(new FSM(listFsm))
                .Add(new AI(210, 210, new[] { EntityType.GreenTank }))
                .Add(new Gun(myvars[1], myvars[3]))
                .Add(new Collision(cellSize, 15))
                .Add(new Motion(Vector2.Zero, r.Next(CellType.Segment, 10000)))
                .Add(new ActionAfterDeath(() =>
                {
                    gameField.ClearCell(position.position.Divide(cellSize.X));
                    CreateEnemyExplosion(position.position);
                    CreateQuake();
                }));

            CreateRedOverlay(entity);
            CreateGreenHealthBar(entity);
            AddCellToMap(x, y, CellType.Tower);

            return entity;
        }
    }
}
