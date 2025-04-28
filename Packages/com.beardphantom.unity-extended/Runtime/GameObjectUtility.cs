using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public static class GameObjectUtility
    {
        public static TComponent FindWithTag<TComponent>(string tag) where TComponent : Component
        {
            GameObject gameObject = GameObject.FindWithTag(tag);
            if (gameObject.IsNull())
            {
                return default;
            }

            return typeof(TComponent) == typeof(Transform)
                ? gameObject.transform as TComponent
                : gameObject.GetComponent<TComponent>();
        }
    }
}