using System;
using System.Collections;
using System.Collections.Generic;

namespace BeardPhantom.UnityExtended
{
    public abstract class LiteEventBase<TDelegate> : IEnumerable<TDelegate>, ILiteEvent<TDelegate> where TDelegate : Delegate
    {
        #region Fields

        protected readonly HashSet<TDelegate> Listeners = new();

        #endregion

        #region Properties

        /// <inheritdoc />
        public int ListenerCount => Listeners.Count;

        #endregion

        #region Methods

        /// <inheritdoc />
        public void Clear()
        {
            Listeners.Clear();
        }

        /// <inheritdoc />
        public bool Register(TDelegate listener)
        {
            return Listeners.Add(listener);
        }

        /// <inheritdoc />
        public bool Unregister(TDelegate listener)
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

        #endregion
    }
}