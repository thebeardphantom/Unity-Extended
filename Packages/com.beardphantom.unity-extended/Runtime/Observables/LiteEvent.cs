using UnityEngine.Pool;

namespace BeardPhantom.UnityExtended
{
    public sealed partial class LiteEvent : LiteEventBase<LiteEvent.OnEventInvoked>
    {
        #region Types

        public delegate void OnEventInvoked();

        #endregion

        #region Events

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

        #endregion

        #region Methods

        public void Invoke()
        {
            if (!Enabled)
            {
                return;
            }

            using (ListPool<OnEventInvoked>.Get(out var listenersCopy))
            {
                listenersCopy.AddRange(Listeners);
                foreach (var listener in listenersCopy)
                {
                    listener();
                }
            }
        }

        #endregion
    }

    public sealed partial class LiteEvent<TArgs> : LiteEventBase<LiteEvent<TArgs>.OnEventInvoked> where TArgs : struct
    {
        #region Types

        public delegate void OnEventInvoked(in TArgs args);

        #endregion

        #region Events

        public event OnEventInvoked Event
        {
            add => Add(value);
            remove => Remove(value);
        }

        #endregion

        #region Methods

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

            using (ListPool<OnEventInvoked>.Get(out var listenersCopy))
            {
                listenersCopy.AddRange(Listeners);
                foreach (var listener in listenersCopy)
                {
                    listener.Invoke(args);
                }
            }
        }

        #endregion
    }
}