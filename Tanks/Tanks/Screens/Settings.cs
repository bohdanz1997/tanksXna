//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using SFML.System;
//using TGUI;
//using DoodleJump.Resources;
//using SFML.Graphics;

//namespace DoodleJump.Screens
//{
//    class Settings : ScreenBase
//    {
//        public Settings(RenderWindow window) : base(window)
//        {
//            var back_btn = gui.Add(new Button(Configs.Doodle));
//            back_btn.Position = new Vector2f(340, 50);
//            back_btn.Size = new Vector2f(120, 40);
//            back_btn.Text = "Назад";
//            back_btn.LeftMouseClickedCallback += (o, e) =>
//            {
//                Manager.Pop();
//                GameSettings.WriteSettings();
//            };

//            var sounds_cbx = gui.Add(new Checkbox(Configs.BabyBlue));
//            sounds_cbx.Position = new Vector2f(100, 150);
//            sounds_cbx.Text = "Звуки";
//            sounds_cbx.CheckedCallback += (o, e) => GameSettings.Sounds = true;
//            sounds_cbx.UncheckedCallback += (o, e) => GameSettings.Sounds = false;

//            var keysControl_cbx = gui.Add(new Checkbox(Configs.BabyBlue));
//            keysControl_cbx.Position = new Vector2f(100, 200);
//            keysControl_cbx.Text = "Управление мышью";
//            keysControl_cbx.CheckedCallback += (o, e) => GameSettings.MouseSteering = true;
//            keysControl_cbx.UncheckedCallback += (o, e) => GameSettings.MouseSteering = false;

//            if (GameSettings.Sounds) sounds_cbx.Check();
//            if (GameSettings.MouseSteering) keysControl_cbx.Check();
//        }
//    }
//}
