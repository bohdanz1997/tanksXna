using System;
using System.Xml;
using System.IO;

namespace Tanks._Game
{
    public static class GameSettings
    {
        public static void ReadSettings()
        {
            var reader = new XmlTextReader("settings");
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name == "setting")
                        {
                            while (reader.MoveToNextAttribute())
                            {
                                if (reader.Name == "sound")
                                    Sounds = Convert.ToBoolean(reader.Value);
                                else if (reader.Name == "mouseSteering")
                                    MouseSteering = Convert.ToBoolean(reader.Value);
                            }
                        }
                        break;
                }
            }
            reader.Close();
        }
        public static void WriteSettings()
        {
            string xml =
            "<settings>\n" +
                "<setting sound=\"" + Sounds + "\"></setting>\n" +
                "<setting mouseSteering=\"" + MouseSteering + "\"></setting>\n" +
             "</settings>";
            using (var writer = new StreamWriter("settings"))
                writer.WriteLine(xml);
        }

        public static bool Sounds { get; set; }
        public static bool MouseSteering { get; set; }
    }
}
