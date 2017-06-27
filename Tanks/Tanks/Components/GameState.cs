using Tanks._Game.GameHelpers;

namespace Tanks.Components
{
    class GameState
    {
        public int waveIndex;
        public Wave[] waves;
        public int score;
        public int money;
        public bool isWaveStarted;

        public float waveInterval;
        public float interval;

        public GameState(Wave[] waves, int waveInterval)
        {
            this.waves = waves;
            waveIndex = -1;
            score = 0;
            money = 0;
            isWaveStarted = false;
            this.waveInterval = waveInterval;
            interval = waveInterval;
        }
    }
}
