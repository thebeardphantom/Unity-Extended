using System.Collections.Generic;

namespace BeardPhantom.UnityExtended
{
    public static class QueueExtensions
    {
        #region Methods

        public static void EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> range)
        {
            foreach (var v in range)
            {
                queue.Enqueue(v);
            }
        }

        #endregion
    }
}