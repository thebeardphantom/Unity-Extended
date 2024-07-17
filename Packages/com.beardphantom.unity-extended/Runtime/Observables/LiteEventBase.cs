using System;
using System.Collections;
using System.Collections.Generic;

namespace BeardPhantom.UnityExtended
{
    public abstract class LiteEventBase<TDelegate> : IEnumerable<TDelegate>, ILiteEvent<TDelegate> where TDelegate : Delegate
    {
        protected readonly HashSet<TDelegate> Listeners = new();

        /// <inheritdoc />
        public int ListenerCount => Listeners.Count;

        /// <inheritdoc />
        public bool Enabled { get; set; } = true;

        /// <inheritdoc />
        public void Clear()
        {
            Listeners.Clear();
        }

        /// <inheritdoc />
        public bool Add(TDelegate listener)
        {
            return Listeners.Add(listener);
        }

        /// <inheritdoc />
        public bool Remove(TDelegate listener)
        {
            return Listeners.Remove(listener);
        }

        /// <inheritdoc />
        public IEnumerator<TDelegate> GetEnumerator()
        {
            return Listeners.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Listeners).GetEnumerator();
        }
    }
}