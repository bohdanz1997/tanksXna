namespace Tanks._Game.GameHelpers
{
    public struct Wave
    {
        public int CountSpawners;
        public int CountUnits;
        public SpawnerData[] SpawnerData;

        public Wave(int countSpawners, int countUnits, SpawnerData[] spawnerData)
        {
            CountSpawners = countSpawners;
            CountUnits = countUnits;
            SpawnerData = spawnerData;
        }
    }
}
