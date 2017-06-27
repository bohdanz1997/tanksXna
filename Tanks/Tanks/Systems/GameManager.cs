using Ash.Core;
using Tanks.Nodes;
using Tanks._Game;
using Tanks.Components;

namespace Tanks.Systems
{
    class GameManager : SystemBase
    {
        EntityFactory factory;

        NodeList gameNodeList;
        NodeList enemyNodeList;
        NodeList spawnerNodeList;

        public GameManager(EntityFactory factory)
        {
            this.factory = factory;
        }

        public override void AddToGame(IGame game)
        {
            gameNodeList = game.GetNodeList<GameNode>();
            enemyNodeList = game.GetNodeList<EnemyNode>();
            spawnerNodeList = game.GetNodeList<SpawnerNode>();
        }

        public override void RemoveFromGame(IGame game)
        {
            gameNodeList = null;
            enemyNodeList = null;
            spawnerNodeList = null;
        }

        public override void Update(float time)
        {
            var gameNode = (GameNode)gameNodeList.Head;

            var gameState = gameNode.state;

            //если волна еще не начата и таймер работает 
            if (!gameState.isWaveStarted && gameState.interval > 0)
            {
                gameState.interval -= time;

                //если таймер закончился запускаем новую волну
                if (gameState.interval < 0)
                {
                    gameState.interval = gameState.waveInterval;
                    gameState.isWaveStarted = true;
                    gameState.waveIndex++;
                    StartWave(gameState);
                }
            }

            //если начата волна и вся вражина уничтожена
            else if (gameState.isWaveStarted && enemyNodeList.Empty)
            {
                //останавливаем волну и включаем таймер на следующую волну
                gameState.isWaveStarted = false;
            }
        }

        private void StartWave(GameState gameState)
        {
            int i = 0;

            //если все волны были выпущены начинаем сначала))
            //но по хорошему нужно заканчивать игру
            if (gameState.waveIndex >= gameState.waves.Length)
            {
                gameState.waveIndex = 0;
            }

            //перебираем все спавнеры
            spawnerNodeList.ForEach<SpawnerNode>(spawnerNode =>
            {
                var spawner = spawnerNode.spawner;
                var wave = gameState.waves[gameState.waveIndex];

                //запускаем спавнер
                spawner.isActive = true;
                //инициализируем значениями текужей волны
                spawner.Initialize(wave.SpawnerData[i]);
                i++;
            });
        }
    }
}
