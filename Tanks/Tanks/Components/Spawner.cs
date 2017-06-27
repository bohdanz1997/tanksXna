using Microsoft.Xna.Framework;
using System;
using Tanks._Game.GameHelpers;

namespace Tanks.Components
{
    class Spawner
    {
        public Action<Vector2, int>[] groupCalls;

        public int countUnits;

        public float spawnInterval;
        public float interval;

        public float spawnInsideInterval;
        public float insideInterval;

        public int[] spawnGroups;
        public int[] groupsLevels;
        public int groupIndex;

        public bool isActive;
        public int unitsSpawned;

        public Spawner()
        {
            isActive = false;
        }

        public void Initialize(SpawnerData spawnerData)
        {
            spawnGroups = spawnerData.SpawnGroups;
            groupsLevels = spawnerData.GroupsLevels;
            groupCalls = spawnerData.GroupCalls;
            countUnits = spawnerData.CountUnits;
            spawnInterval = spawnerData.Interval;
            spawnInsideInterval = spawnerData.InsideInterval;
            interval = 0;
            insideInterval = 0;
            groupIndex = 0;
            unitsSpawned = 0;
        }
    }
}
