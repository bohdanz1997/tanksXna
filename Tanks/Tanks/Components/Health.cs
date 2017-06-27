namespace Tanks.Components
{
    class Health
    {
        public float chanceOfFire;
        public float current;
        public float max;

        public Health(float health, float chanceOfFire = 0)
        {
            this.chanceOfFire = chanceOfFire;
            current = health;
            max = health;
        }
    }
}
