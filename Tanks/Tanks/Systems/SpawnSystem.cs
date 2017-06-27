using Tanks.Nodes;
using Tanks._Game;
using Ash.Tools;

namespace Tanks.Systems
{
    class SpawnSystem : ListIteratingSystem<SpawnerNode>
    {
        EntityFactory factory;

        public SpawnSystem(EntityFactory factory)
        {
            this.factory = factory;
            NodeUpdate += Update;
        }

        private void Update(SpawnerNode node, float time)
        {
            var spawner = node.spawner;

            if (spawner.isActive)
            {
                spawner.interval -= time;
                if (spawner.interval < 0)
                {
                    //если интервал спавна между групами истек
                    spawner.insideInterval -= time;
                    if (spawner.insideInterval < 0)
                    {
                        spawner.insideInterval = spawner.spawnInsideInterval;

                        //если количество заспавненых юнитов текущей групы 
                        //превысило максимально возможное
                        if (spawner.unitsSpawned >= spawner.spawnGroups[spawner.groupIndex])
                        {
                            spawner.unitsSpawned = 0;
                            spawner.interval = spawner.spawnInterval;
                            
                            //идем к следующей групе
                            spawner.groupIndex++;

                            //если групы закончились
                            //выключаем спавнер и сбрасываем счетчики
                            if (spawner.groupIndex > spawner.spawnGroups.Length - 1)
                            {
                                spawner.groupIndex = 0;
                                spawner.isActive = false;
                            }
                        }
                        else
                        {
                            //спавним юнита
                            spawner.groupCalls[spawner.groupIndex]?.Invoke(
                                node.position.position, 
                                spawner.groupsLevels[spawner.groupIndex]
                            );
                            spawner.unitsSpawned++;
                        }
                    }
                }
            }
        }
    }
}
