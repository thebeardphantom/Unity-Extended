using System;

namespace BeardPhantom.UnityExtended
{
    [Serializable]
    public class PrefabComponent<T> : Prefab
    {
        public T Instantiate(in InstantiateArgs instantiateArgs = default)
        {
            return base.Instantiate<T>(instantiateArgs);
        }
    }
}