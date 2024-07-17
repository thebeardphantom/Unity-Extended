using System;
using System.Collections;
using System.Collections.Generic;

namespace BeardPhantom.UnityExtended
{
    public interface IObservableQueue : ICollection
    {
        void CopyFrom(Array array);
    }

    public class ObservableQueue<T> : Queue<T>, IObservableQueue
    {
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

        private readonly LiteEvent<ItemEnqueuedArgs> _itemEnqueued = new();

        private readonly LiteEvent<ItemDequeuedArgs> _itemDequeued = new();

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

        public readonly struct ItemEnqueuedArgs
        {
            public readonly T Item;

            public ItemEnqueuedArgs(T item)
            {
                Item = item;
            }
        }

        public readonly struct ItemDequeuedArgs
        {
            public readonly T Item;

            public ItemDequeuedArgs(T item)
            {
                Item = item;
            }
        }
    }
}