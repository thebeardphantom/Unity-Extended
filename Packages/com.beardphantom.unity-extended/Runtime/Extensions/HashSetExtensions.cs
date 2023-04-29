using System.Collections.Generic;

namespace BeardPhantom.UnityExtended
{
    public static class HashSetExtensions
    {
        #region Methods

        public static void AddRange<T>(this HashSet<T> set, IEnumerable<T> range)
        {
            foreach (var v in range)
            {
                set.Add(v);
            }
        }

        #endregion
    }
}