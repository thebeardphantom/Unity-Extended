using System;

namespace BeardPhantom.UnityExtended
{
    [Serializable]
    public class PrefabComponent<T> : Prefab
    {
        #region Methods

        public T Instantiate(in InstantiateArgs instantiateArgs = default)
        {
            return base.Instantiate<T>(instantiateArgs);
        }

        #endregion
    }
}