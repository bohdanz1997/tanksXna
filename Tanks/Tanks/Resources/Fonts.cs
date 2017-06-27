namespace Tanks.Resources
{
    public class Fonts
    {
        public static Fonts Instance;

        public static void Instantiate()
        {
            Instance = new Fonts();
            Instance.DefaultFont = Instance.path + "DefaultFont";
        }

        string path = "Fonts/";

        public string DefaultFont;
    }
}
