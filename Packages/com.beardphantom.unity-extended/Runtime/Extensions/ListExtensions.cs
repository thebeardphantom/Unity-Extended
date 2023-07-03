using System.Collections.Generic;

namespace BeardPhantom.UnityExtended
{
    public static class ListExtensions
    {
        #region Fields

        private static readonly UnityRandomAdapter _unityRandomAdapter = new();

        #endregion

        #region Methods

        public static void RemoveSwapback<T>(this IList<T> list, T item)
        {
            var index = list.IndexOf(item);
            RemoveAtSwapback(list, index);
        }

        public static T RemoveAtSwapback<T>(this IList<T> list, int index)
        {
            var endIndex = list.Count - 1;
            var toRemove = list[index];
            if (endIndex > 0)
            {
                list[index] = list[endIndex];
            }

            list.RemoveAt(endIndex);
            return toRemove;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            Shuffle(list, _unityRandomAdapter);
        }

        public static void Shuffle<T>(this IList<T> list, IRandomAdapter randomAdapter)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = randomAdapter.Next(0, n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static T Random<T>(this IList<T> list, IRandomAdapter randomAdapter)
        {
            var index = randomAdapter.Next(0, list.Count - 1);
            return list[index];
        }

        public static T RemoveRandom<T>(this IList<T> list)
        {
            return RemoveRandom(list, _unityRandomAdapter);
        }

        public static T RemoveRandom<T>(this IList<T> list, IRandomAdapter randomAdapter)
        {
            var index = randomAdapter.Next(0, list.Count - 1);
            var rnd = list[index];
            list.RemoveAt(index);
            return rnd;
        }

        public static T RemoveAtAndGet<T>(this IList<T> list, int index)
        {
            var rnd = list[index];
            list.RemoveAt(index);
            return rnd;
        }

        #endregion
    }
}