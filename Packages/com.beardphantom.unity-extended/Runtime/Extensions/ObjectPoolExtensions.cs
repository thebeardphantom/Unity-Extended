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
            using (ListPool<T>.Get(out var initPool))
            {
                for (var i = 0; i < count; i++)
                {
                    var item = pool.Get();
                    initPool.Add(item);
                }

                for (var i = count - 1; i >= 0; i--)
                {
                    var element = initPool[i];
                    pool.Release(element);
                }
            }
        }
    }
}