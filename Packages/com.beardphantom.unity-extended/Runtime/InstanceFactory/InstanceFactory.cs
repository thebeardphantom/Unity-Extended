using System.Threading;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class InstanceFactory
    {
        public static IInstanceFactoryAPI API { get; set; } = new DefaultInstanceFactoryAPI();

        public static async Awaitable<GameObject> InstantiateAsync(
            this IInstanceFactoryAPI api,
            GameObject original,
            Vector3? position = null,
            Quaternion? rotation = null,
            InstantiateParameters instantiateParameters = default,
            CancellationToken cancellationToken = default)
        {
            GameObject[] results = await api.InstantiateAsync(
                original,
                1,
                position,
                rotation,
                instantiateParameters,
                cancellationToken);
            return results[0];
        }
    }
}