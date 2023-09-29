using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class GameObjectExtensions
    {
        #region Methods

        public static bool TryFindChildWithTag(this GameObject root, string tag, out GameObject result)
        {
            if (root.CompareTag(tag))
            {
                result = root;
                return true;
            }

            var childCount = root.transform.childCount;
            for (var i = 0; i < childCount; i++)
            {
                var child = root.transform.GetChild(i);
                if (TryFindChildWithTag(child.gameObject, tag, out result))
                {
                    return true;
                }
            }

            result = default;
            return false;
        }

        #endregion
    }
}