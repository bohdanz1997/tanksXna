using Microsoft.Xna.Framework.Input;

namespace Tanks._Game
{
    class Config
    {
        public static Config Instance { get; set; }

        public static void Instantiate()
        {
            Instance = new Config();
        }

        public int ScreenWidth;
        public int ScreenHeight;
        public int CellSize;

        public Keys MoveUp;
        public Keys MoveDown;
        public Keys MoveLeft;
        public Keys MoveRight;
        public Keys GunTrigger;
    }
}
