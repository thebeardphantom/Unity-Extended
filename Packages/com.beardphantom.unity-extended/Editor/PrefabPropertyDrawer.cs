using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace BeardPhantom.UnityExtended.Editor
{
    [CustomPropertyDrawer(typeof(Prefab), true)]
    public class PrefabPropertyDrawer : PropertyDrawer
    {
        private static void OnAttachToPanel(AttachToPanelEvent evt)
        {
            var propertyField = (PropertyField)evt.target;
            string labelText = propertyField.label;
            var label = propertyField.Q<Label>(className: PropertyField.labelUssClassName);
            if (label != null)
            {
                label.text = labelText;
            }
        }

        /// <inheritdoc />
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            SerializedProperty assetProperty = property.FindPropertyRelative("<Asset>k__BackingField");
            var assetPropertyField = new PropertyField(assetProperty, property.displayName);
            assetPropertyField.RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            return assetPropertyField;
        }
    }
}