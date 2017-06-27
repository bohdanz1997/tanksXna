using Ash.Core;
using Tanks.Systems;
using Tanks.Managers;
using Tanks.Resources;
using Tanks.Systems.StateSystems;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Tanks.Enums;
using Tanks.Tools;
using Tanks.Components;
using ServerLogic;

namespace Tanks._Game
{
    class GameCore
    {
        ResourceManager<Texture2D> textureManager;
        ResourceManager<SpriteFont> fontManager;
        InputManager inputManager;
        SpriteBatch spriteBatch;
        EntityFactory factory;
        GameField gameField;
        Game<Family> game;
        Variables vars;
        int cellSize;

        public GameCore(SpriteBatch spriteBatch)
        {
            textureManager = Global.TextureManager;
            fontManager = Global.FontManager;
            inputManager = Global.InputManager;
            this.spriteBatch = spriteBatch;
            cellSize = Config.Instance.CellSize;
            vars = new Variables();
            game = new Game<Family>();
            factory = new EntityFactory(game, textureManager, fontManager, vars);
        }

        public void Initialize()
        {
            InitializeMap(Maps.Map2);
            InitializeSystems();
        }

        public void InitializeMap(string mapName)
        {
            Vector2 mapSize;
            var tiles = MapManager.ReadMapLayer(mapName, "ground", new Vector2(cellSize), out mapSize);
            var walls = MapManager.ReadMapLayer(mapName, "walls", new Vector2(cellSize), out mapSize);
            var objects = MapManager.ReadMapLayer(mapName, "objects", new Vector2(cellSize), out mapSize);

            gameField = new GameField((int)mapSize.X, (int)mapSize.Y);

            factory.Initialize(gameField);
            factory.CreateCamera(mapSize * cellSize);
            factory.CreateGame();
            factory.CreateHud();

            foreach (var tile in tiles)
            {
                var g = Ground.Ground;
                if (tile.GID >= 108) g = Ground.Asphalt;
                else if (tile.GID >= 84) g = Ground.Sand;

                var tileEntity = factory.CreateTile(tile.X, tile.Y, tile.GID, g);
                gameField.SetCell(tile.Position, g, tileEntity);
            }
            
            foreach (var wall in walls)
            {
                switch (wall.Type)
                {
                    case CellType.Wall:      factory.CreateWall(wall.X, wall.Y); break;
                    case CellType.SteelWall: factory.CreateSteelWall(wall.X, wall.Y, 420, wall.Type); break;
                    case CellType.TitanWall: factory.CreateSteelWall(wall.X, wall.Y, 450, wall.Type); break;
                    case CellType.ArmorWall: factory.CreateSteelWall(wall.X, wall.Y, 480, wall.Type); break;
                    case CellType.Tree:      factory.CreateTree(wall.X, wall.Y); break;
                    case CellType.Obstacle:  factory.CreateObstacle(wall.X, wall.Y); break;
                }
            }

            foreach (var o in objects)
            {
                switch (o.Type)
                {
                    case CellType.GreenTank: factory.CreatePlayer(o.X, o.Y); break;
                    case CellType.RedTank: factory.CreateTank(o.X, o.Y); break;
                    case CellType.GreenSpawner: factory.CreateRedSpawner(o.X, o.Y); break;
                    case CellType.RedSpawner: factory.CreateRedSpawner(o.X, o.Y); break;
                    case CellType.Tower: factory.CreateTower(o.X, o.Y, 4); break;
                }
            }
        }

        public void InitializeSystems()
        {
            //Update
            //game.AddSystem(new CameraControlSystem(inputManager), SystemPriorities.Update);
            game.AddSystem(new PlayerControlSystem(inputManager), SystemPriorities.Update);
            game.AddSystem(new DeathOnTimeSystem(factory), SystemPriorities.Update);
            game.AddSystem(new CameraFocusSystem(), SystemPriorities.Update);
            game.AddSystem(new GameManager(factory), SystemPriorities.Update);
            game.AddSystem(new SpawnSystem(factory), SystemPriorities.Update);
            game.AddSystem(new GunControlSystem(factory, inputManager), SystemPriorities.Update);
            game.AddSystem(new HealthControlSystem(factory), SystemPriorities.Update);
            game.AddSystem(new HealthBarControlSystem(), SystemPriorities.Update);
            game.AddSystem(new FieldSystem(gameField, cellSize), SystemPriorities.Update);

            //Move
            game.AddSystem(new QuakeSystem(), SystemPriorities.Move);
            game.AddSystem(new MovementSystem(cellSize), SystemPriorities.Move);
            game.AddSystem(new TrackSystem(factory, cellSize), SystemPriorities.Move);

            //ResolveCollisions
            game.AddSystem(new FieldSegmentSystem(gameField, cellSize), SystemPriorities.ResolveCollisions);
            game.AddSystem(new TankCollisionSystem(factory, gameField, cellSize), SystemPriorities.ResolveCollisions);
            game.AddSystem(new BulletCollisionSystem(factory, cellSize), SystemPriorities.ResolveCollisions);

            //StateMachines
            game.AddSystem(new StandSystem(), SystemPriorities.StateMachines);
            game.AddSystem(new TurnSystem(), SystemPriorities.StateMachines);
            game.AddSystem(new WalkSystem(cellSize), SystemPriorities.StateMachines);
            game.AddSystem(new IdleSystem(gameField, cellSize), SystemPriorities.StateMachines);
            game.AddSystem(new PursuitSystem(gameField, cellSize), SystemPriorities.StateMachines);
            game.AddSystem(new AttackSystem(factory, cellSize), SystemPriorities.StateMachines);
            
            //Animate
            game.AddSystem(new AnimationSystem(), SystemPriorities.Animate);

            //Render
            game.AddSystem(new RenderSystem(spriteBatch, cellSize), SystemPriorities.Render);
            game.AddSystem(new HudRenderSystem(spriteBatch), SystemPriorities.Render);
        }
        
        public void Update(float time)
        {
            game.Update(time);
        }
        
        public void Reset()
        {
            game.RemoveAllEntities();
            game.RemoveAllSystems();
        }
    }
}
