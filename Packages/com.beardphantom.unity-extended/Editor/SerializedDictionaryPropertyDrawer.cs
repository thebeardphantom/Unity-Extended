using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace BeardPhantom.UnityExtended.Editor
{
    [CustomPropertyDrawer(typeof(SerializedDictionary<,>))]
    public class SerializedDictionaryPropertyDrawer : PropertyDrawer
    {
        #region Fields

        private static readonly string _propertyPath =
            $"<{SerializedDictionary<object, object>.SERIALIZED_KEY_VALUE_PAIRS_PROPERTY_NAME}>k__BackingField";

        #endregion

        #region Methods

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var serializedValuesProperty = property.FindPropertyRelative(_propertyPath);
            return new PropertyField(serializedValuesProperty, property.displayName);
        }

        #endregion
    }
}