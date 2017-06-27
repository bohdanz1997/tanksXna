namespace Tanks.Resources
{
    public class Images
    {
        public static Images Instance;

        public static void Instantiate()
        {
            Instance = new Images();
            Instance.GameTextures = Instance.path + "GameTextures";
            Instance.TileSet = Instance.path + "TileSet";
            Instance.BulletExplosion = Instance.path + "BulletExplosion";
            Instance.EnemyExplosion = Instance.path + "EnemyExplosion";
            Instance.Overlay = Instance.path + "Overlay";
            Instance.Heaven = Instance.path + "Heaven";
            Instance.HealthBack = Instance.path + "HealthBack";
            Instance.HealthFront = Instance.path + "HealthFront";
        }

        string path = "Images/";
        
        public string GameTextures;
        public string TileSet;
        public string BulletExplosion;
        public string EnemyExplosion;
        public string Overlay;
        public string Heaven;
        public string HealthBack;
        public string HealthFront;
    }
}
