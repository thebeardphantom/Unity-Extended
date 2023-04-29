using UnityEngine;
using UnityEngine.Pool;

namespace BeardPhantom.UnityExtended
{
    public static class ComponentExtensions
    {
        #region Methods

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