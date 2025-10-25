using System.Threading;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public class DefaultInstanceFactoryAPI : IInstanceFactoryAPI
    {
        public async Awaitable<GameObject[]> InstantiateAsync(
            GameObject original,
            int count,
            Vector3? position = null,
            Quaternion? rotation = null,
            InstantiateParameters instantiateParameters = default,
            CancellationToken cancellationToken = default)
        {
            if (!position.HasValue || !rotation.HasValue)
            {
                Transform originalTform = original.transform;
                Vector3 defaultPosition;
                Quaternion defaultRotation;
                if (instantiateParameters.worldSpace)
                {
                    originalTform.GetPositionAndRotation(out defaultPosition, out defaultRotation);
                }
                else
                {
                    originalTform.GetLocalPositionAndRotation(out defaultPosition, out defaultRotation);
                }

                position ??= defaultPosition;
                rotation ??= defaultRotation;
            }

            GameObject[] results = await Object.InstantiateAsync(
                original,
                count,
                position.Value,
                rotation.Value,
                instantiateParameters,
                cancellationToken);

            return results;
        }

        public GameObject Instantiate(
            GameObject original,
            Vector3? position = null,
            Quaternion? rotation = null,
            InstantiateParameters instantiateParameters = default)
        {
            if (!position.HasValue || !rotation.HasValue)
            {
                Transform originalTform = original.transform;
                Vector3 defaultPosition;
                Quaternion defaultRotation;
                if (instantiateParameters.worldSpace)
                {
                    originalTform.GetPositionAndRotation(out defaultPosition, out defaultRotation);
                }
                else
                {
                    originalTform.GetLocalPositionAndRotation(out defaultPosition, out defaultRotation);
                }

                position ??= defaultPosition;
                rotation ??= defaultRotation;
            }

            return Object.Instantiate(original, position.Value, rotation.Value, instantiateParameters);
        }

        public T Instantiate<T>(T original) where T : Object
        {
            return Object.Instantiate(original);
        }

        public void Destroy(Object obj)
        {
            Object.Destroy(obj);
        }

        public void DestroyImmediate(Object obj)
        {
            Object.DestroyImmediate(obj);
        }
    }
}