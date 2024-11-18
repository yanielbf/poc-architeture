using System.Collections.Concurrent;

namespace WROBoxLabelGeneration.SharedKernel.Extensions
{
    public static class GenericTypeExtensions
    {
        private static readonly ConcurrentDictionary<Type, string> genericTypeNamesCache = new();
        
        /// <summary>
        /// Gets a display friendly version of generic types.
        /// </summary>
        /// <param name="obj">The object for which a type name will be returned.</param>
        /// <returns>The name of the given type, supports more friendly rendering of generic types.</returns>
        public static string GetGenericTypeName(this object obj)
            => genericTypeNamesCache.GetOrAdd(obj.GetType(), GenericTypeNameFactory);
        
        private static string GenericTypeNameFactory(Type type)
        {
            if (type.IsGenericType)
            {
                var genericTypes = string.Join(",", type.GetGenericArguments().Select(t => t.Name).ToArray());
                return $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
            }
            return type.Name;
        }
    }
}
