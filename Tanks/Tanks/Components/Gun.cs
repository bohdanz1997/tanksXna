namespace Tanks.Components
{
    class Gun
    {
        public float timeSinceLastShot;
        public float minimumShotInterval;
        public float bulletLevel;

        public Gun(float bulletLevel, float minimumShotInterval)
        {
            this.minimumShotInterval = minimumShotInterval;
            this.bulletLevel = bulletLevel;
        }
    }
}
