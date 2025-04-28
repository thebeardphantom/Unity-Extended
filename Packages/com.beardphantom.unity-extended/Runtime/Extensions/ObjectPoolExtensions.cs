using System.Collections.Generic;
using UnityEngine.Pool;

namespace BeardPhantom.UnityExtended
{
    public static class ObjectPoolExtensions
    {
        /// <summary>
        /// Primes an IObjectPool by creating temporary items and then returning them to the pool.
        /// </summary>
        /// <param name="pool">The pool to prime.</param>
        /// <param name="count">The number of elements to create.</param>
        /// <typeparam name="T">The type of element.</typeparam>
        public static void Prime<T>(this IObjectPool<T> pool, int count) where T : class
        {
            using (ListPool<T>.Get(out List<T> initPool))
            {
                for (var i = 0; i < count; i++)
                {
                    T item = pool.Get();
                    initPool.Add(item);
                }

                for (int i = count - 1; i >= 0; i--)
                {
                    T element = initPool[i];
                    pool.Release(element);
                }
            }
        }
    }
}