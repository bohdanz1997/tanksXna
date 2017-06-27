using System;

namespace Tanks.Components
{
    class ActionAfterDeath
    {
        public Action callback;

        public ActionAfterDeath(Action callback = null)
        {
            this.callback = callback;
        }
    }
}
