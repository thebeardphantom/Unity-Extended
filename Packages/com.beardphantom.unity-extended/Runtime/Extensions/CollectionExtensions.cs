using System.Collections.Generic;

namespace BeardPhantom.UnityExtended
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> range)
        {
            foreach (T v in range)
            {
                collection.Add(v);
            }
        }
    }
}