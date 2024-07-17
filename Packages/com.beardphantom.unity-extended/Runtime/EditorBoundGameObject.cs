#if UNITY_EDITOR
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    public class EditorBoundGameObject : MonoBehaviour
    {
        public void DestroyIfNecessary()
        {
            if (!Application.IsPlaying(this) || !Application.isEditor)
            {
                return;
            }

            transform.DetachChildren();
            DestroyImmediate(gameObject);
        }

        private void Awake()
        {
            DestroyIfNecessary();
        }

        private void OnValidate()
        {
            hideFlags |= HideFlags.DontSaveInBuild;
        }
    }
}
#endif