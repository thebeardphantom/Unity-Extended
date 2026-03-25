using System;
using System.Collections;
using System.Collections.Generic;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Provides a set of extension methods for working with various collection types.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Determines whether the specified element exists in the array.
        /// </summary>
        /// <typeparam name="T">The type of elements in the array.</typeparam>
        /// <param name="array">The array to search within.</param>
        /// <param name="item">The element to locate in the array.</param>
        /// <returns>True if the element is found in the array; otherwise, false.</returns>
        public static bool Contains<T>(this T[] array, T item)
        {
            return Array.IndexOf(array, item) >= 0;
        }

        /// <summary>
        /// Determines whether an array contains a specified item using the provided equality comparer.
        /// </summary>
        /// <typeparam name="T">The type of elements in the array.</typeparam>
        /// <param name="array">The array to be checked for the presence of the item.</param>
        /// <param name="item">The item to locate in the array.</param>
        /// <param name="comparer">The equality comparer to use for comparing elements.</param>
        /// <returns>
        /// True if the item is found in the array using the specified comparer; otherwise, false.
        /// </returns>
        public static bool Contains<T>(this T[] array, T item, IEqualityComparer<T> comparer)
        {
            foreach (T arrayItem in array)
            {
                if (comparer.Equals(item, arrayItem))
                {
                    return true;
                }
            }

            return false;
        }

        /// Adds a range of items to the specified collection.
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="collection">The collection to which the items will be added.</param>
        /// <param name="range">The enumerable containing the items to add to the collection.</param>
        /// <exception cref="ArgumentNullException">Thrown if the collection or range is null.</exception>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> range)
        {
            foreach (T v in range)
            {
                collection.Add(v);
            }
        }

        /// <summary>
        /// Resolves the zero-based index value based on the given <see cref="Index" />.
        /// </summary>
        /// <param name="list">The list from which the index will be resolved.</param>
        /// <param name="index">The <see cref="Index" /> specifying the position to resolve.</param>
        /// <returns>The zero-based index as an integer.</returns>
        public static int ResolveIndex(this IList list, Index index)
        {
            return index.GetOffset(list.Count);
        }

        /// Removes the specified item from the list by swapping it with the last item and then removing the last item.
        /// This operation avoids shifting elements and ensures better performance for unordered lists.
        /// If the item does not exist in the list, no action is taken.
        /// <param name="list">The list from which to remove the item. Must not be null.</param>
        /// <param name="item">The item to remove from the list.</param>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <exception cref="ArgumentNullException">Thrown if the list is null.</exception>
        /// <exception cref="Exception">Thrown if attempting to remove an item from an empty list.</exception>
        public static void RemoveSwapback<T>(this IList<T> list, T item)
        {
            int index = list.IndexOf(item);
            list.RemoveAtSwapback(index);
        }

        /// <summary>
        /// Removes the element at the specified index from the list by swapping it with the last element
        /// and then removing the last element, thereby minimizing the number of operations.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list from which the element will be removed.</param>
        /// <param name="index">The index of the element to remove.</param>
        /// <returns>The element that was removed from the list.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the index is less than 0 or greater than or equal to the
        /// list's count.
        /// </exception>
        /// <exception cref="Exception">Thrown when attempting to remove an element from an empty list.</exception>
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

        /// <summary>
        /// Shuffles the elements of the specified <see cref="IList{T}" /> in random order.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list to be shuffled.</param>
        /// <remarks>
        /// This will use <see cref="UnityRandomNumberGenerator" /> as the <see cref="IRandomNumberGenerator" />.
        /// </remarks>
        public static void Shuffle<T>(this IList<T> list)
        {
            list.Shuffle(UnityRandomNumberGenerator.Instance);
        }

        /// <summary>
        /// Randomly shuffles the elements in the specified list using Unity's random number generator.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list to shuffle.</param>
        /// <param name="rng">The random number generator to use for shuffling the list.</param>
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

        /// <summary>
        /// Retrieves a randomly selected element from the given read-only list using the specified random number generator.
        /// </summary>
        /// <typeparam name="T">The type of elements within the list.</typeparam>
        /// <param name="list">The read-only list to select a random element from.</param>
        /// <param name="rng">The random number generator to use for selecting the random element.</param>
        /// <returns>A randomly selected element from the list.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the list is empty.</exception>
        public static T Random<T>(this IReadOnlyList<T> list, IRandomNumberGenerator rng)
        {
            int index = rng.Next(0, list.Count);
            return list[index];
        }

        /// <summary>
        /// Removes and returns a random item from the list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list from which the random item is removed.</param>
        /// <returns>The item that was removed from the list.</returns>
        /// <remarks>
        /// This will use <see cref="UnityRandomNumberGenerator" /> as the <see cref="IRandomNumberGenerator" />.
        /// </remarks>
        public static T RemoveRandom<T>(this IList<T> list)
        {
            return list.RemoveRandom(UnityRandomNumberGenerator.Instance);
        }

        /// <summary>
        /// Removes a random element from the specified list using the default random number generator and returns it.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list from which a random element is removed.</param>
        /// <param name="rng">The random number generator to use for selecting the item to remove.</param>
        /// <returns>The element that was removed.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the list is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the list is empty.</exception>
        public static T RemoveRandom<T>(this IList<T> list, IRandomNumberGenerator rng)
        {
            int index = rng.Next(0, list.Count);
            T rnd = list[index];
            list.RemoveAt(index);
            return rnd;
        }

        /// <summary>
        /// Removes the element at the specified index in the list and returns it.
        /// </summary>
        /// <param name="list">The list from which the element will be removed.</param>
        /// <param name="index">The zero-based index of the element to remove.</param>
        /// <returns>The element that was removed from the list.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the index is not a valid position in the list.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the list is null.</exception>
        public static T RemoveAtAndGet<T>(this IList<T> list, int index)
        {
            T rnd = list[index];
            list.RemoveAt(index);
            return rnd;
        }

        /// <summary>
        /// Resolves the zero-based index from a given span based on the specified Index value.
        /// </summary>
        /// <typeparam name="T">The type of elements in the span.</typeparam>
        /// <param name="span">The read-only span to compute the index against.</param>
        /// <param name="index">The Index value representing the position to resolve.</param>
        /// <returns>The resolved absolute index within the span.</returns>
        public static int ResolveIndex<T>(this ReadOnlySpan<T> span, Index index)
        {
            return index.GetOffset(span.Length);
        }

        /// <summary>
        /// Resolves the zero-based index from a given <see cref="Index" /> within the specified <see cref="Span{T}" />.
        /// </summary>
        /// <typeparam name="T">The type of elements in the span.</typeparam>
        /// <param name="span">The span in which the index is being resolved.</param>
        /// <param name="index">The index structure to resolve.</param>
        /// <returns>The zero-based index corresponding to the provided <see cref="Index" /> within the span.</returns>
        public static int ResolveIndex<T>(this Span<T> span, Index index)
        {
            return index.GetOffset(span.Length);
        }

        /// <summary>
        /// Shuffles the order of the elements in the specified span.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="span">The span whose elements will be shuffled.</param>
        /// <remarks>
        /// This will use <see cref="UnityRandomNumberGenerator" /> as the <see cref="IRandomNumberGenerator" />.
        /// </remarks>
        public static void Shuffle<T>(this Span<T> span)
        {
            span.Shuffle(UnityRandomNumberGenerator.Instance);
        }

        /// <summary>
        /// Shuffles the elements in the specified span.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="span">The span whose elements will be shuffled.</param>
        /// <param name="rng">The random number generator used to shuffle the span.</param>
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

        /// <summary>
        /// Selects a random element from the specified <see cref="ReadOnlySpan{T}" /> using the provided random number generator.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the span.</typeparam>
        /// <param name="span">The span from which to select a random element.</param>
        /// <param name="rng">The random number generator used to select the index.</param>
        /// <returns>A randomly selected element from the span.</returns>
        /// <exception cref="Exception">Thrown when the span is empty.</exception>
        public static T Random<T>(this ReadOnlySpan<T> span, IRandomNumberGenerator rng)
        {
            if (span.Length == 0)
            {
                throw new Exception("Cannot select from an empty span.");
            }

            return span[rng.Next(0, span.Length)];
        }

        /// <summary>
        /// Returns a random item from the given <see cref="Span{T}" /> based on the provided random number generator.
        /// </summary>
        /// <typeparam name="T">The type of elements in the span.</typeparam>
        /// <param name="span">The span to select the random item from.</param>
        /// <param name="rng">The random number generator to use for selecting the item.</param>
        /// <returns>A random item from the span.</returns>
        /// <exception cref="Exception">Thrown if the span is empty.</exception>
        public static T Random<T>(this Span<T> span, IRandomNumberGenerator rng)
        {
            if (span.Length == 0)
            {
                throw new Exception("Cannot select from an empty span.");
            }

            return span[rng.Next(0, span.Length)];
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the queue.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the queue and collection.</typeparam>
        /// <param name="queue">The queue to which the elements will be added.</param>
        /// <param name="range">The collection of elements to be added to the queue.</param>
        public static void EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> range)
        {
            foreach (T v in range)
            {
                queue.Enqueue(v);
            }
        }
    }
}