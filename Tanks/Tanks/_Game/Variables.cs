using Microsoft.Xna.Framework;
using System;
using Tanks._Game.GameHelpers;

namespace Tanks._Game
{
    class Variables
    {
        public float[][] Bullet =
        {
            //Type--Speed--Damage
            new [] { 11, 2.5f, 1 },
            new [] { 12, 3.5f, 1 },
            new [] { 13, 3.0f, 2 },
            new [] { 14, 2.5f, 2 },
            new [] { 15, 3.5f, 2 },
            new [] { 16, 5.0f, 4 }
        };

        public float[][] LightTank =
        {
            //Speed--Ammo--HP--KD--БК%--TurnSpeed 
            new [] { 1.00f, 0, 03, 1250, 5, 64 },
            new [] { 1.25f, 1, 04, 1250, 4, 48 },
            new [] { 1.25f, 3, 06, 1000, 4, 48 },
            new [] { 1.50f, 3, 08, 1000, 3, 32 },
            new [] { 1.50f, 4, 10, 0750, 2, 16 }
        };

        public float[][] HeavyTank =
        {
            //Speed--Ammo--HP--KD--БК% --TurnSpeed 
            new [] { 0.75f, 3, 06, 1250, 3, 96 },
            new [] { 1.00f, 3, 08, 1250, 2, 80 },
            new [] { 1.00f, 4, 10, 1000, 2,  64 },
            new [] { 1.25f, 4, 10, 1000, 1,  64 },
            new [] { 1.25f, 5, 12, 0750, 1,  48 }
        };

        public float[][] Tower =
        {
            //Speed--Ammo--HP--KD--БК% --TurnSpeed 
            new [] { 0.00f, 0, 06, 1000, 5, 64 },
            new [] { 0.00f, 1, 08, 0750, 4, 64 },
            new [] { 0.00f, 3, 10, 0750, 4, 48 },
            new [] { 0.00f, 3, 11, 0500, 3, 38 },
            new [] { 0.00f, 4, 12, 0500, 2, 32 }
        };

        public Wave GetTestWave(EntityFactory factory)
        {
            var rand = new Random();
            var interval = 5000;
            var interval2 = 1000;
            var range = (int)Mathf.FromPercent(0.25f, interval);
            var range2 = (int)Mathf.FromPercent(0.25f, interval2);

            Wave testWave = new Wave(4, 20, new SpawnerData[]
            {
                new SpawnerData(5, interval + rand.Next(-range, range), 
                    interval2 + rand.Next(-range2, range2), 
                    new [] { 0, 1, 2, 2 }, 
                    new [] { 1, 2, 3, 4 }, 
                    new Action<Vector2, int>[] 
                        { (p, l) => factory.CreateTank(p.X, p.Y, l) }),

                new SpawnerData(7, interval + rand.Next(-range, range), 
                    interval2 + rand.Next(-range2, range2), 
                    new [] { 2, 3, 0, 4 },
                    new [] { 1, 2, 3, 4 },
                    new Action<Vector2, int>[]
                        { (p, l) => factory.CreateTank(p.X, p.Y, l) }),

                new SpawnerData(6, interval + rand.Next(-range, range), 
                    interval2 + rand.Next(-range2, range2), 
                    new [] { 1, 0, 2, 3 },
                    new [] { 1, 2, 3, 4 },
                    new Action<Vector2, int>[]
                        { (p, l) => factory.CreateTank(p.X, p.Y, l) }),

                new SpawnerData(2, interval + rand.Next(-range, range),
                    interval2 + rand.Next(-range2, range2),
                    new [] { 1, 0, 1 },
                    new [] { 2, 3, 4 },
                    new Action<Vector2, int>[]
                        { (p, l) => factory.CreateTank(p.X, p.Y, l) })
            });
            return testWave;
        }
    }
}
