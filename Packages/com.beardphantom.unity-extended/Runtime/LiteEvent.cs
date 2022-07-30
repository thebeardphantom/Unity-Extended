using System.Collections.Generic;
using UnityEngine.Pool;

namespace BeardPhantom.UnityExtended
{
    public sealed class LiteEvent : ILiteEvent<LiteEvent.OnEventInvoked>
    {
        #region Types

        public delegate void OnEventInvoked();

        #endregion

        #region Events

        public event OnEventInvoked Event
        {
            add => Register(value);
            remove => Unregister(value);
        }

        #endregion

        #region Fields

        private readonly HashSet<OnEventInvoked> _listeners = new();

        #endregion

        #region Properties

        public int ListenerCount => _listeners.Count;

        #endregion

        #region Methods

        public void Invoke()
        {
            using (ListPool<OnEventInvoked>.Get(out var listenersCopy))
            {
                listenersCopy.AddRange(_listeners);
                foreach (var listener in listenersCopy)
                {
                    listener();
                }
            }
        }

        public IEnumerable<OnEventInvoked> GetListeners()
        {
            using (ListPool<OnEventInvoked>.Get(out var listeners))
            {
                foreach (var listener in listeners)
                {
                    yield return listener;
                }
            }
        }

        public void Clear()
        {
            _listeners.Clear();
        }

        public bool Register(OnEventInvoked listener)
        {
            return _listeners.Add(listener);
        }

        public bool Unregister(OnEventInvoked listener)
        {
            return _listeners.Remove(listener);
        }

        #endregion
    }

    public sealed class LiteEvent<TArgs> : ILiteEvent<LiteEvent<TArgs>.OnEventInvoked>
    {
        #region Types

        public delegate void OnEventInvoked(TArgs args);

        #endregion

        #region Events

        public event OnEventInvoked Event
        {
            add => Register(value);
            remove => Unregister(value);
        }

        #endregion

        #region Fields

        private readonly HashSet<OnEventInvoked> _listeners = new();

        #endregion

        #region Properties

        public int ListenerCount => _listeners.Count;

        #endregion

        #region Methods

        public IEnumerable<OnEventInvoked> GetListeners()
        {
            using (ListPool<OnEventInvoked>.Get(out var listeners))
            {
                foreach (var listener in listeners)
                {
                    yield return listener;
                }
            }
        }

        public void Clear()
        {
            _listeners.Clear();
        }

        public bool Register(OnEventInvoked listener)
        {
            return _listeners.Add(listener);
        }

        public bool Unregister(OnEventInvoked listener)
        {
            return _listeners.Remove(listener);
        }

        public void Invoke(TArgs args)
        {
            using (ListPool<OnEventInvoked>.Get(out var listenersCopy))
            {
                listenersCopy.AddRange(_listeners);
                foreach (var listener in listenersCopy)
                {
                    listener.Invoke(args);
                }
            }
        }

        #endregion
    }
}