using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

namespace BeardPhantom.UnityExtended.Editor
{
    [CustomPropertyDrawer(typeof(SerializedHashSet<>))]
    public class SerializedHashSetPropertyDrawer : PropertyDrawer
    {
        private static readonly string _propertyPath =
            $"<{SerializedHashSet<object>.SerializedValuesPropertyName}>k__BackingField";

        private static void CheckForDuplicates(VisualElement propertyField)
        {
            var listView = propertyField.Q<ListView>();
            var listProperty = (SerializedProperty)propertyField.userData;
            using (ListPool<VisualElement>.Get(out List<VisualElement> visualElements))
            {
                listView.Query(className: "unity-list-view__item").ToList(visualElements);
                for (var i = 0; i < visualElements.Count; i++)
                {
                    if (i >= listProperty.arraySize)
                    {
                        break;
                    }

                    SerializedProperty arrayElementA = listProperty.GetArrayElementAtIndex(i);
                    VisualElement visualElement = visualElements[i];
                    if (i == 0)
                    {
                        visualElement.style.backgroundColor = new StyleColor(StyleKeyword.Null);
                        continue;
                    }

                    for (var j = 0; j < i; j++)
                    {
                        SerializedProperty arrayElementB = listProperty.GetArrayElementAtIndex(j);
                        visualElement.style.backgroundColor = SerializedProperty.DataEquals(arrayElementA, arrayElementB)
                            ? new Color(1f, 0f, 0f, 0.5f)
                            : new StyleColor(StyleKeyword.Null);
                    }
                }
            }
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            SerializedProperty serializedValuesProperty = property.FindPropertyRelative(_propertyPath).Copy();
            var propertyField = new PropertyField(serializedValuesProperty, property.displayName)
            {
                userData = serializedValuesProperty,
            };
            propertyField.schedule.Execute(() => CheckForDuplicates(propertyField)).Every(100);
            return propertyField;
        }
    }
}