using Tanks.Screens;
using System.Collections.Generic;

namespace Tanks.Managers
{
    public class ScreenManager
    {
        Dictionary<ScreenType, ScreenBase> screens;
        Stack<ScreenType> history;
        ScreenBase currentScreen;
        ScreenType prevScreen;

        public ScreenType CurrentScreen => history.Peek();
        
        public ScreenManager()
        {
            screens = new Dictionary<ScreenType, ScreenBase>();
            history = new Stack<ScreenType>();
        }
        public ScreenManager AddScreen(ScreenType type, ScreenBase value)
        {
            value.Manager = this;
            value.Deactivate();
            screens.Add(type, value);
            return this;
        }
        public void DeactivateScreen(ScreenType screen)
        {
            prevScreen = screen;
            screens[screen].Deactivate();
        }
        private void SetScreen(ScreenType screen)
        {
            currentScreen = screens[screen];
            currentScreen.Activate(prevScreen);
        }
        public ScreenManager Push(ScreenType screen)
        {
            if (history.Count > 0)
                DeactivateScreen(history.Peek());
            history.Push(screen);
            SetScreen(screen);
            return this;
        }
        public void Pop()
        {
            var key = history.Pop();
            DeactivateScreen(key);
            if (history.Count > 0)
                SetScreen(history.Peek());
        }
        public void Update(float elapsed)
        {
            currentScreen.Update(elapsed);
        }
        public void Draw()
        {
            currentScreen.Draw();
        }
    }
}
