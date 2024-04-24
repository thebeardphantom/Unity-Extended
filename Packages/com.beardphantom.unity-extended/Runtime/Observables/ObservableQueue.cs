using System;
using System.Collections;
using System.Collections.Generic;

namespace BeardPhantom.UnityExtended
{
    public interface IObservableQueue : ICollection
    {
        #region Methods

        void CopyFrom(Array array);

        #endregion
    }

    public class ObservableQueue<T> : Queue<T>, IObservableQueue
    {
        #region Types

        public readonly struct ItemEnqueuedArgs
        {
            #region Fields

            public readonly T Item;

            #endregion

            #region Constructors

            public ItemEnqueuedArgs(T item)
            {
                Item = item;
            }

            #endregion
        }

        public readonly struct ItemDequeuedArgs
        {
            #region Fields

            public readonly T Item;

            #endregion

            #region Constructors

            public ItemDequeuedArgs(T item)
            {
                Item = item;
            }

            #endregion
        }

        #endregion

        #region Events

        public event LiteEvent<ItemEnqueuedArgs>.OnEventInvoked ItemEnqueued
        {
            add => _itemEnqueued.Add(value);
            remove => _itemEnqueued.Remove(value);
        }

        public event LiteEvent<ItemDequeuedArgs>.OnEventInvoked ItemDequeued
        {
            add => _itemDequeued.Add(value);
            remove => _itemDequeued.Remove(value);
        }

        #endregion

        #region Fields

        private readonly LiteEvent<ItemEnqueuedArgs> _itemEnqueued = new();

        private readonly LiteEvent<ItemDequeuedArgs> _itemDequeued = new();

        #endregion

        #region Methods

        public new void Enqueue(T item)
        {
            base.Enqueue(item);
            _itemEnqueued.Invoke(new ItemEnqueuedArgs(item));
        }

        public new T Dequeue()
        {
            var item = base.Dequeue();
            _itemDequeued.Invoke(new ItemDequeuedArgs(item));
            return item;
        }

        /// <inheritdoc />
        public void CopyFrom(Array array)
        {
            foreach (var obj in array)
            {
                base.Enqueue((T)obj);
            }
        }

        #endregion
    }
}