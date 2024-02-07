using System;
using System.Collections.Generic;

namespace BeardPhantom.UnityExtended
{
    public class EventBus
    {
        #region Types

        public delegate void OnEventInvoked<T>(in T eventArgs);

        private class EventInvoker<T> where T : struct
        {
            #region Fields

            private readonly HashSet<OnEventInvoked<T>> _callbacks = new();

            #endregion

            #region Methods

            public bool Register(OnEventInvoked<T> callback)
            {
                return _callbacks.Add(callback);
            }

            public bool Unregister(OnEventInvoked<T> callback)
            {
                return _callbacks.Remove(callback);
            }

            public void Invoke(T args)
            {
                foreach (var callback in _callbacks)
                {
                    callback.Invoke(args);
                }
            }

            #endregion
        }

        #endregion

        #region Fields

        private readonly Dictionary<Type, object> _typeToInvoker = new();

        #endregion

        #region Methods

        public void Invoke<T>(in T evtArgs) where T : struct
        {
            var typedInvoker = AddOrGetTypedInvoker<T>();
            typedInvoker.Invoke(evtArgs);
        }

        public void Register<T>(OnEventInvoked<T> callback) where T : struct
        {
            var typedInvoker = AddOrGetTypedInvoker<T>();
            typedInvoker.Register(callback);
        }

        private EventInvoker<T> AddOrGetTypedInvoker<T>() where T : struct
        {
            EventInvoker<T> typedInvoker;
            if (_typeToInvoker.TryGetValue(typeof(T), out var rawInvoker))
            {
                typedInvoker = (EventInvoker<T>)rawInvoker;
            }
            else
            {
                typedInvoker = new EventInvoker<T>();
                _typeToInvoker[typeof(T)] = typedInvoker;
            }

            return typedInvoker;
        }

        #endregion
    }
}