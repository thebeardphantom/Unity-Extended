using System.Collections.Generic;
using UnityEngine.Pool;

namespace BeardPhantom.UnityExtended
{
    public sealed class LiteEvent : LiteEventBase<LiteEvent.OnEventInvoked>
    {
        public delegate void OnEventInvoked();

        public event OnEventInvoked Event
        {
            add => Add(value);
            remove => Remove(value);
        }

        public event OnEventInvoked EventWithImmediateInvoke
        {
            add
            {
                Add(value);
                value();
            }
            remove => Remove(value);
        }

        public void Invoke()
        {
            if (!Enabled)
            {
                return;
            }

            using (ListPool<OnEventInvoked>.Get(out List<OnEventInvoked> listenersCopy))
            {
                listenersCopy.AddRange(Listeners);
                foreach (OnEventInvoked listener in listenersCopy)
                {
                    listener();
                }
            }
        }
    }

    public sealed class LiteEvent<TArgs> : LiteEventBase<LiteEvent<TArgs>.OnEventInvoked> where TArgs : struct
    {
        public delegate void OnEventInvoked(in TArgs args);

        public event OnEventInvoked Event
        {
            add => Add(value);
            remove => Remove(value);
        }

        public void AddWithImmediateInvoke(OnEventInvoked callback, in TArgs initArgs)
        {
            Event += callback;
            callback(in initArgs);
        }

        public void Invoke(in TArgs args)
        {
            if (!Enabled)
            {
                return;
            }

            using (ListPool<OnEventInvoked>.Get(out List<OnEventInvoked> listenersCopy))
            {
                listenersCopy.AddRange(Listeners);
                foreach (OnEventInvoked listener in listenersCopy)
                {
                    listener.Invoke(args);
                }
            }
        }
    }
}