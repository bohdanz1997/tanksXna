using Microsoft.Xna.Framework;
using System;

namespace Tanks._Game.GameHelpers
{
    public struct SpawnerData
    {
        public int CountUnits;
        public int InsideInterval;
        public int Interval;
        public int[] SpawnGroups;
        public int[] GroupsLevels;
        public Action<Vector2, int>[] GroupCalls;

        public SpawnerData(int countUnits, int interval, int insideInterval, int[] spawnGroups,
            int[] groupsLevels, Action<Vector2, int>[] groupCalls)
        {
            CountUnits = countUnits;
            Interval = interval;
            InsideInterval = insideInterval;
            SpawnGroups = spawnGroups;
            GroupsLevels = groupsLevels;

            if (groupCalls.Length == 1)
            {
                GroupCalls = new Action<Vector2, int>[spawnGroups.Length];
                for (int i = 0; i < spawnGroups.Length; i++)
                {
                    GroupCalls[i] = groupCalls[0];
                }
            }
            else
            {
                GroupCalls = groupCalls;
            }
        }
    }
}
