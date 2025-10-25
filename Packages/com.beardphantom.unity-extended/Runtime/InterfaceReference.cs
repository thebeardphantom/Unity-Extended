using System;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    [Serializable]
    public class InterfaceReference<T> where T : class
    {
        public T Value => (T)(object)Component;

        public bool IsValid => Component is T;

        [field: SerializeField]
        private MonoBehaviour Component { get; set; }

        public bool TryGetValue(out T value)
        {
            if (Component is T interfaceValue)
            {
                value = interfaceValue;
                return true;
            }

            value = null;
            return false;
        }
    }
}