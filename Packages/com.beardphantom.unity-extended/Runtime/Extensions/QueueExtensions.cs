using System.Collections.Generic;

namespace BeardPhantom.UnityExtended
{
    public static class QueueExtensions
    {
        public static void EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> range)
        {
            foreach (T v in range)
            {
                queue.Enqueue(v);
            }
        }
    }
}