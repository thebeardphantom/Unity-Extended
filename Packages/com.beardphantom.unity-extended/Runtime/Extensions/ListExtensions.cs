using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace BeardPhantom.UnityExtended
{
    public static class ListExtensions
    {
        private static readonly UnityRandomAdapter s_unityRandomAdapter = new();

        public static int ResolveIndex(this IList list, Index index)
        {
            return index.GetOffset(list.Count);
        }

        public static void RemoveSwapback<T>(this IList<T> list, T item)
        {
            int index = list.IndexOf(item);
            RemoveAtSwapback(list, index);
        }

        public static T RemoveAtSwapback<T>(this IList<T> list, int index)
        {
            int count = list.Count;
            Assert.AreNotEqual(0, count, "list.Count == 0");
            Assert.IsTrue(index < count, "index < list.Count");
            Assert.IsTrue(index >= 0, "index >= 0");
            T item;
            if (count == 1)
            {
                item = list[0];
                list.Clear();
            }
            else
            {
                item = list[index];
                list[index] = list[count - 1];
                list.RemoveAt(count - 1);
            }

            return item;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            Shuffle(list, s_unityRandomAdapter);
        }

        public static void Shuffle<T>(this IList<T> list, IRandomAdapter randomAdapter)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = randomAdapter.Next(0, n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static T Random<T>(this IList<T> list, IRandomAdapter randomAdapter)
        {
            int index = randomAdapter.Next(0, list.Count - 1);
            return list[index];
        }

        public static T RemoveRandom<T>(this IList<T> list)
        {
            return RemoveRandom(list, s_unityRandomAdapter);
        }

        public static T RemoveRandom<T>(this IList<T> list, IRandomAdapter randomAdapter)
        {
            int index = randomAdapter.Next(0, list.Count - 1);
            T rnd = list[index];
            list.RemoveAt(index);
            return rnd;
        }

        public static T RemoveAtAndGet<T>(this IList<T> list, int index)
        {
            T rnd = list[index];
            list.RemoveAt(index);
            return rnd;
        }
    }
}