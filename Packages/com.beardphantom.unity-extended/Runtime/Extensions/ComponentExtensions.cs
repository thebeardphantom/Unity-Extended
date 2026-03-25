using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Provides extension methods for working with Unity components and GameObjects.
    /// These methods offer utility functions to simplify common tasks such as requiring components,
    /// adding or retrieving components, and generating cancellation tokens tied to the destruction of objects.
    /// </summary>
    public static class ComponentExtensions
    {
        /// <summary>
        /// Retrieves a <see cref="CancellationToken" /> that is canceled when the specified
        /// <see cref="GameObject" /> is destroyed.
        /// </summary>
        /// <param name="gameObject">The <see cref="GameObject" /> to monitor for destruction.</param>
        /// <returns>
        /// A <see cref="CancellationToken" /> that is tied to the lifecycle of the
        /// specified <see cref="GameObject" />. If the <see cref="GameObject" /> contains a
        /// <see cref="MonoBehaviour" />, its destruction token is returned. Otherwise, a dummy
        /// <see cref="MonoBehaviour" /> is added to the <see cref="GameObject" /> to manage the token.
        /// </returns>
        public static CancellationToken GetDestroyCancellationToken(this GameObject gameObject)
        {
            return gameObject.TryGetComponent(out MonoBehaviour monoBehaviour)
                ? monoBehaviour.destroyCancellationToken
                : gameObject.AddOrGetComponent<DummyMonoBehaviour>().destroyCancellationToken;
        }

        /// <summary>
        /// Retrieves the required component of type <typeparamref name="T" /> from the GameObject associated with the specified
        /// Unity Component.
        /// If the specified component or the required component is missing, an exception is thrown.
        /// </summary>
        /// <typeparam name="T">The type of the required component.</typeparam>
        /// <param name="component">The Unity Component from which to retrieve the required component.</param>
        /// <returns>The component of type <typeparamref name="T" /> on the GameObject.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the supplied <paramref name="component" /> is null.</exception>
        /// <exception cref="Exception">Thrown if a component of type <typeparamref name="T" /> does not exist on the GameObject.</exception>
        public static T GetRequiredComponent<T>(this Component component)
        {
            if (component.IsNull())
            {
                throw new ArgumentNullException(nameof(component));
            }

            return component.gameObject.GetRequiredComponent<T>();
        }

        /// <summary>
        /// Retrieves a required component of type <typeparamref name="T" /> from the specified <see cref="GameObject" />.
        /// </summary>
        /// <typeparam name="T">The type of component to retrieve.</typeparam>
        /// <param name="gameObject">The <see cref="GameObject" /> from which to retrieve the component.</param>
        /// <returns>
        /// An instance of the component of type <typeparamref name="T" /> on the specified <see cref="GameObject" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="gameObject" /> is null.</exception>
        /// <exception cref="Exception">
        /// Thrown if the component of type <typeparamref name="T" /> is not found on the
        /// <paramref name="gameObject" />.
        /// </exception>
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

        /// <summary>
        /// Adds a component of type <typeparamref name="T" /> to the current component's GameObject, or retrieves the existing one
        /// if it already exists.
        /// </summary>
        /// <typeparam name="T">The type of the component to add or retrieve. Must be a subclass of UnityEngine.Component.</typeparam>
        /// <param name="component">The component whose GameObject will be used to add or retrieve the target component.</param>
        /// <returns>An instance of the component of type <typeparamref name="T" />.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the provided <paramref name="component" /> is null.</exception>
        public static T AddOrGetComponent<T>(this Component component) where T : Component
        {
            if (component.IsNull())
            {
                throw new ArgumentNullException(nameof(component));
            }

            return component.gameObject.AddOrGetComponent<T>();
        }

        /// <summary>
        /// Adds a component of type T to the GameObject if it does not already exist,
        /// or retrieves the existing component of type T if it does.
        /// </summary>
        /// <typeparam name="T">The type of the component to add or retrieve. Must derive from UnityEngine.Component.</typeparam>
        /// <param name="gameObject">The GameObject to which the component should be added or from which it should be retrieved.</param>
        /// <returns>The existing or newly added component of type T.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the GameObject is null.</exception>
        /// <exception cref="ArgumentException">
        /// Thrown if T is an abstract class or an interface, as these types cannot be added as a component.
        /// </exception>
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

        /// <summary>
        /// Creates a clone of the specified GameObject, retaining only rendering-related components.
        /// All other components are removed from the cloned object.
        /// </summary>
        /// <param name="gameObject">The GameObject to clone.</param>
        /// <returns>A new GameObject containing only rendering-related components.</returns>
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