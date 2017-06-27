using System;
using Ash.Core;
using Ash.FSM;
using Tanks.Managers;
using Tanks.Resources;
using Tanks.Components;
using Microsoft.Xna.Framework;
using Tanks.Enums;
using Tanks.Tools;
using Tanks.Tools.Extensions;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Tanks._Game
{
    partial class EntityFactory
    {
        Random r;
        IGame game;
        ResourceManager<Texture2D> textureManager;
        ResourceManager<SpriteFont> fontManager;
        GameField gameField;
        Variables vars;
        Vector2 cellSize;

        public EntityFactory(IGame game, ResourceManager<Texture2D> textureManager, 
            ResourceManager<SpriteFont> fontManager, Variables vars)
        {
            r = new Random();
            this.vars = vars;
            this.game = game;
            this.textureManager = textureManager;
            this.fontManager = fontManager;
            cellSize = new Vector2(Config.Instance.CellSize);
        }

        public void Initialize(GameField gameField)
        {
            this.gameField = gameField;
        }

        public Entity CreateEntity()
        {
            var entity = new Entity();
            game.AddEntity(entity);
            return entity;
        }

        public Entity CreateEntity(Enum type)
        {
            var entity = new Entity(type);
            game.AddEntity(entity);
            return entity;
        }

        public void DestroyEntity(Entity entity)
        {
            game.RemoveEntity(entity);
        }

        public Entity CreateGame()
        {
            return CreateEntity()
                .Add(new GameState(new [] { vars.GetTestWave(this) }, 5000));
        }
        
        public Entity CreateHud()
        {
            return CreateEntity()
                .Add(new Hud(fontManager.Get(Fonts.Instance.DefaultFont), "This is text"))
                .Add(new Position(5, 5));
        }

        public Entity CreateCamera(Vector2 mapSize)
        {
            return CreateEntity()
                .Add(new CameraFocus(Global.Camera, mapSize))
                .Add(new Position(0, 0))
                .Add(new Motion(Vector2.Zero, 3, 2, 2))
                .Add(new Controls(Keys.Up, Keys.Down, Keys.Left, Keys.Right, new Vector2(3f)));
        }
        
        public Entity CreateMouseHandler()
        {
            return CreateEntity()
                .Add(new MouseHandler());
        }

        public Entity CreateQuake()
        {
            var entity = CreateEntity();
            return entity
                .Add(new Quake(80))
                .Add(new Controls(new Vector2(4)))
                .Add(new DeathOnTime(1000, () => DestroyEntity(entity)));
        }
        
        public void AddCellToMap(float x, float y, int type)
        {
            gameField.SetCell((int)(x / cellSize.X), (int)(y / cellSize.Y), type);
        }
    }
}
