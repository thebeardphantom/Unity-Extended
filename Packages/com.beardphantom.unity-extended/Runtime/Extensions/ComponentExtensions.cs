using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace BeardPhantom.UnityExtended
{
    public static class ComponentExtensions
    {
        public static CancellationToken GetDestroyCancellationToken(this GameObject gameObject)
        {
            return gameObject.TryGetComponent(out MonoBehaviour monoBehaviour)
                ? monoBehaviour.destroyCancellationToken
                : gameObject.AddOrGetComponent<DummyMonoBehaviour>().destroyCancellationToken;
        }

        public static T GetRequiredComponent<T>(this Component component)
        {
            if (component.IsNull())
            {
                throw new ArgumentNullException(nameof(component));
            }

            return GetRequiredComponent<T>(component.gameObject);
        }

        public static T GetRequiredComponent<T>(this GameObject gameObject)
        {
            if (gameObject.IsNull())
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            if (gameObject.TryGetComponent(out T component))
            {
                return component;
            }

            throw new Exception($"No component of type {typeof(T)} on GameObject {gameObject.name}.");
        }

        public static T AddOrGetComponent<T>(this Component component) where T : Component
        {
            if (component.IsNull())
            {
                throw new ArgumentNullException(nameof(component));
            }

            return AddOrGetComponent<T>(component.gameObject);
        }

        public static T AddOrGetComponent<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject.IsNull())
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            if (gameObject.TryGetComponent(out T component))
            {
                return component;
            }

            if (typeof(T).IsAbstract)
            {
                throw new ArgumentException($"Cannot add abstract component of type {typeof(T)}.", nameof(T));
            }

            if (typeof(T).IsInterface)
            {
                throw new ArgumentException($"Cannot add interface of type {typeof(T)}.", nameof(T));
            }

            return gameObject.AddComponent<T>();
        }

        public static GameObject GetRenderClone(GameObject gameObject)
        {
            GameObject clone = Object.Instantiate(gameObject);
            using (ListPool<Component>.Get(out List<Component> components))
            {
                clone.GetComponentsInChildren(true, components);
                foreach (Component component in components)
                {
                    if (!IsRenderingComponent(component) && component is not Transform)
                    {
                        Object.Destroy(component);
                    }
                }
            }

            return clone;
        }

        private static bool IsRenderingComponent(Component component)
        {
            return component is Renderer or MeshFilter or Animator;
        }
    }
}