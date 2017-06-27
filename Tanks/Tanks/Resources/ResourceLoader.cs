using Tanks.Managers;
using System.Linq;
using System.Reflection;

namespace Tanks.Resources
{
    public static class ResourceLoader
    {
        /// <summary>
        /// Загружает все ресурсы основываясь на полях класа R и типе ресурса T
        /// </summary>
        /// <typeparam name="T">Тип ресурса</typeparam>
        /// <typeparam name="R">Тип класа со строковыми константами</typeparam>
        /// <param name="resourceManager"></param>
        public static void LoadResources<T, R>(ResourceManager<T> resourceManager)
        {
            var type = typeof(R);
            var fields = type.GetFields(BindingFlags.Instance);
            fields
                .Select(s => s.GetValue(s))
                .ToList()
                .ForEach(s => resourceManager.Load(s.ToString()));
        }
    }
}
