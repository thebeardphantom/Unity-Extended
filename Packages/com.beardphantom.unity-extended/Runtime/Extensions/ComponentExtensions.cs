using System;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace BeardPhantom.UnityExtended
{
    public static class ComponentExtensions
    {
        #region Methods

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

            if (gameObject.TryGetComponent<T>(out var component))
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

            if (gameObject.TryGetComponent<T>(out var component))
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
            var clone = Object.Instantiate(gameObject);
            using (ListPool<Component>.Get(out var components))
            {
                clone.GetComponentsInChildren(true, components);
                foreach (var component in components)
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

        #endregion
    }
}