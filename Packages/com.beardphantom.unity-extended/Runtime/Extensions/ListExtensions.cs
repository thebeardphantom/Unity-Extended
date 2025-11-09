using System;
using System.Collections;
using System.Collections.Generic;

namespace BeardPhantom.UnityExtended
{
    public static class ListExtensions
    {
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
            if (count == 0)
            {
                throw new Exception("Cannot remove from an empty list.");
            }

            if (index >= count)
            {
                throw new IndexOutOfRangeException($"Index {index} must be less than or equal to count {count}.");
            }

            if (index < 0)
            {
                throw new IndexOutOfRangeException($"Index {index} must be greater than or equal to zero.");
            }

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
            Shuffle(list, UnityRandomNumberGenerator.Instance);
        }

        public static void Shuffle<T>(this IList<T> list, IRandomNumberGenerator rng)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(0, n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static T Random<T>(this IReadOnlyList<T> list, IRandomNumberGenerator rng)
        {
            int index = rng.Next(0, list.Count);
            return list[index];
        }

        public static T RemoveRandom<T>(this IList<T> list)
        {
            return RemoveRandom(list, UnityRandomNumberGenerator.Instance);
        }

        public static T RemoveRandom<T>(this IList<T> list, IRandomNumberGenerator rng)
        {
            int index = rng.Next(0, list.Count);
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

        public static int ResolveIndex<T>(this ReadOnlySpan<T> span, Index index)
        {
            return index.GetOffset(span.Length);
        }

        public static int ResolveIndex<T>(this Span<T> span, Index index)
        {
            return index.GetOffset(span.Length);
        }

        public static void Shuffle<T>(this Span<T> span)
        {
            Shuffle(span, UnityRandomNumberGenerator.Instance);
        }

        public static void Shuffle<T>(this Span<T> span, IRandomNumberGenerator rng)
        {
            int n = span.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(0, n + 1);
                (span[k], span[n]) = (span[n], span[k]);
            }
        }

        public static T Random<T>(this ReadOnlySpan<T> span, IRandomNumberGenerator rng)
        {
            if (span.Length == 0)
            {
                throw new Exception("Cannot select from an empty span.");
            }

            return span[rng.Next(0, span.Length)];
        }

        public static T Random<T>(this Span<T> span, IRandomNumberGenerator rng)
        {
            if (span.Length == 0)
            {
                throw new Exception("Cannot select from an empty span.");
            }

            return span[rng.Next(0, span.Length)];
        }
    }
}