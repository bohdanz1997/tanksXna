using System;

namespace Tanks.Components
{
    class DeathOnTime
    {
        public float timeRemaining;
        public float maxTime;
        public Action callback;

        public DeathOnTime(float timeRemaining, Action callback = null)
        {
            this.callback = callback;
            this.timeRemaining = timeRemaining;
            maxTime = timeRemaining;
        }
    }
}
