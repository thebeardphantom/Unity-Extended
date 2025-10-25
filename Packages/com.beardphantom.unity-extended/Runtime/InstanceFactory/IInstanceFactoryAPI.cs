using System.Threading;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BeardPhantom.UnityExtended
{
    public interface IInstanceFactoryAPI
    {
        Awaitable<GameObject[]> InstantiateAsync(
            GameObject original,
            int count,
            Vector3? position = null,
            Quaternion? rotation = null,
            InstantiateParameters instantiateParameters = default,
            CancellationToken cancellationToken = default);
        
        GameObject Instantiate(
            GameObject original,
            Vector3? position = null,
            Quaternion? rotation = null,
            InstantiateParameters instantiateParameters = default);

        T Instantiate<T>(T original) where T : Object;

        void Destroy(Object obj);

        void DestroyImmediate(Object obj);
    }
}