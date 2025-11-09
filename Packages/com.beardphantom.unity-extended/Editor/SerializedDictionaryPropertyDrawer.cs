using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace BeardPhantom.UnityExtended.Editor
{
    [CustomPropertyDrawer(typeof(SerializedDictionary<,>))]
    public class SerializedDictionaryPropertyDrawer : PropertyDrawer
    {
        private static readonly string s_propertyPath =
            $"<{SerializedDictionary<object, object>.SerializedKeyValuePairsPropertyName}>k__BackingField";

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            SerializedProperty serializedValuesProperty = property.FindPropertyRelative(s_propertyPath);
            return new PropertyField(serializedValuesProperty, property.displayName);
        }
    }
}