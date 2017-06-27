using Microsoft.Xna.Framework.Graphics;
using Tanks.Components;
using Tanks.Managers;
using Tanks.Tools;

namespace Tanks._Game.EntityViews
{
    abstract class EntityView
    {
        protected ResourceManager<Texture2D> textureManager;

        public EntityView()
        {
            textureManager = Global.TextureManager;
        }

        public abstract Display GetView();
    }
}
