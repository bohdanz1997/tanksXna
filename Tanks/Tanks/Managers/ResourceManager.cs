using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;

namespace Tanks.Managers
{
    public class ResourceManager<T>
    {
        Dictionary<string, T> resources;
        ContentManager content;

        public ResourceManager(ContentManager content)
        {
            this.content = content;
            resources = new Dictionary<string, T>();
        }

        public void Load(string filename)
        {
            var t = content.Load<T>(filename);
            resources.Add(filename, t);
        }

        public T Get(string id)
        {
            var found = resources.FirstOrDefault(f => f.Key == id);
            if (found.Value != null)
                return found.Value;
            Load(id);
            return resources[id];
        }
    }
}
