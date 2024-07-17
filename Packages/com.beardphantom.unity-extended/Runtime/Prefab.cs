using System;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    [Serializable]
    public class Prefab
    {
        [field: SerializeField]
        public GameObject Asset { get; private set; }

        public GameObject InstantiatePrefab(in InstantiateArgs instantiateArgs = default)
        {
            var instance = instantiateArgs.Instantiate(Asset);
            return instance;
        }

        public T Instantiate<T>(in InstantiateArgs instantiateArgs = default)
        {
            var instance = InstantiatePrefab(instantiateArgs);
            var component = instance.GetComponent<T>();
            return component;
        }
    }
}