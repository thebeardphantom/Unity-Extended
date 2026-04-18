using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Provides extension methods for the GameObject class.
    /// </summary>
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Attempts to find a child GameObject with the specified tag within the hierarchy of the given root GameObject.
        /// </summary>
        /// <param name="root">
        /// The root GameObject to search within.
        /// </param>
        /// <param name="tag">
        /// The tag to search for.
        /// </param>
        /// <param name="result">
        /// The first GameObject with the specified tag if found; otherwise, null.
        /// </param>
        /// <returns>
        /// True if a GameObject with the specified tag is found; otherwise, false.
        /// </returns>
        public static bool TryFindChildWithTag(this GameObject root, string tag, out GameObject result)
        {
            if (root.CompareTag(tag))
            {
                result = root;
                return true;
            }

            int childCount = root.transform.childCount;
            for (var i = 0; i < childCount; i++)
            {
                Transform child = root.transform.GetChild(i);
                if (child.gameObject.TryFindChildWithTag(tag, out result))
                {
                    return true;
                }
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Sets the layer of the specified GameObject and all its child GameObjects recursively.
        /// </summary>
        /// <param name="gameObject">
        /// The GameObject whose layer and the layer of its children will be changed.
        /// </param>
        /// <param name="layer">
        /// The layer to apply to the GameObject and its children.
        /// </param>
        public static void SetLayerRecursively(this GameObject gameObject, int layer)
        {
            gameObject.layer = layer;
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.SetLayerRecursively(layer);
            }
        }
    }
}