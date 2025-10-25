#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UnityExtended
{
    [ExecuteInEditMode]
    public class InspectorSection : MonoBehaviour
    {
        public IReadOnlyList<Component> ComponentsReadOnly => Components;

        [field: SerializeField]
        private string Name { get; set; }

        [field: HideInInspector]
        [field: SerializeField]
        private List<Component> Components { get; set; } = new();

        public bool IsValidComponent(Component component)
        {
            return component != null && component.gameObject == gameObject;
        }

        public bool TryRemove(Component component)
        {
            bool didRemove = Components.Remove(component);
            if (didRemove)
            {
                component.hideFlags &= ~HideFlags.HideInInspector;
            }

            return didRemove;
        }

        public bool TryAdd(Component component)
        {
            if (!IsValidComponent(component))
            {
                return false;
            }

            if (Components.Contains(component))
            {
                return false;
            }

            component.hideFlags |= HideFlags.HideInInspector;
            Components.Add(component);
            return true;
        }

        private void Awake()
        {
            if (Application.isPlaying)
            {
                Destroy(this);
            }
        }

        private void Update()
        {
            CleanList();
            foreach (Component component in Components)
            {
                component.hideFlags |= HideFlags.HideInInspector;
            }
        }

        private void OnDestroy()
        {
            GameObject gObj = gameObject;
            foreach (Component component in Components)
            {
                if (component != null && component.gameObject == gObj)
                {
                    component.hideFlags &= ~HideFlags.HideInInspector;
                }
            }

            Components.Clear();
        }

        private void OnValidate()
        {
            CleanList();
        }

        private void CleanList()
        {
            GameObject gObj = gameObject;
            Components.RemoveAll(component => component == null || component.gameObject != gObj);
        }
    }
}
#endif