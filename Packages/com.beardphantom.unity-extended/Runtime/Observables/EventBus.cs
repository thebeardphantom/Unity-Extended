using System;
using System.Collections.Generic;

namespace BeardPhantom.UnityExtended
{
    public class EventBus
    {
        public delegate void OnEventInvoked<T>(in T eventArgs);

        private readonly Dictionary<Type, object> _typeToInvoker = new();

        public void Invoke<T>(in T evtArgs) where T : struct
        {
            EventInvoker<T> typedInvoker = AddOrGetTypedInvoker<T>();
            typedInvoker.Invoke(evtArgs);
        }

        public void Register<T>(OnEventInvoked<T> callback) where T : struct
        {
            EventInvoker<T> typedInvoker = AddOrGetTypedInvoker<T>();
            typedInvoker.Register(callback);
        }

        private EventInvoker<T> AddOrGetTypedInvoker<T>() where T : struct
        {
            EventInvoker<T> typedInvoker;
            if (_typeToInvoker.TryGetValue(typeof(T), out object rawInvoker))
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

        private class EventInvoker<T> where T : struct
        {
            private readonly HashSet<OnEventInvoked<T>> _callbacks = new();

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
                foreach (OnEventInvoked<T> callback in _callbacks)
                {
                    callback.Invoke(args);
                }
            }
        }
    }
}