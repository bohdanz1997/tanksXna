using Microsoft.Xna.Framework.Input;

namespace Tanks.Components
{
    class GunControls
    {
        public Keys trigger;
        public bool triggerReleased;

        public GunControls(Keys trigger)
        {
            this.trigger = trigger;
        }
    }
}
