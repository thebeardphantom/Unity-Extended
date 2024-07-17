using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UnityExtended.Editor
{
    [CustomPropertyDrawer(typeof(ReferenceDrawerAttribute))]
    public class ReferenceDrawerAttributePropertyDrawer : PropertyDrawer
    {
        private readonly List<Type> _typeOptions = new();

        private string[] _typeOptionLabels;

        private bool _initialized;

        private static void Delete(object userData)
        {
            var property = (SerializedProperty)userData;
            property.isExpanded = false;
            property.managedReferenceValue = null;
            property.serializedObject.ApplyModifiedProperties();
        }

        private static void CreateNew(object userData)
        {
            var (type, property) = ((Type, SerializedProperty))userData;
            var instance = Activator.CreateInstance(type);

            property.managedReferenceValue = instance;
            property.isExpanded = true;
            property.serializedObject.ApplyModifiedProperties();

            EditorUtility.SetDirty(property.serializedObject.targetObject);
        }

        private static string GetManagedReferenceTypeName(SerializedProperty property)
        {
            var type = property.type;
            type = type.Remove(0, 17);
            type = type.Substring(0, type.Length - 1);
            return type;
        }

        /// <inheritdoc />
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, true);
        }

        /// <inheritdoc />
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!_initialized)
            {
                Initialize();
            }

            var headerRect = new Rect(position)
            {
                height = EditorGUIUtility.singleLineHeight,
            };
            var typeName = GetManagedReferenceTypeName(property);
            var hasValue = !string.IsNullOrWhiteSpace(typeName);

            if (Event.current.type == EventType.ContextClick && headerRect.Contains(Event.current.mousePosition))
            {
                Event.current.Use();
                ShowContextMenu(property, hasValue);
                return;
            }

            if (hasValue)
            {
                label.text += $" ({typeName})";
            }
            else
            {
                label.text += " (null)";
            }

            EditorGUI.LabelField(headerRect, label, hasValue ? EditorStyles.boldLabel : EditorStyles.label);
            EditorGUI.PropertyField(position, property, GUIContent.none, true);
        }

        private void ShowContextMenu(SerializedProperty property, bool hasValue)
        {
            var menu = new GenericMenu();
            var content = new GUIContent("Delete");
            if (hasValue)
            {
                menu.AddItem(content, false, Delete, property);
            }
            else
            {
                menu.AddDisabledItem(content);
            }

            for (var i = 0; i < _typeOptionLabels.Length; i++)
            {
                var typeOptionLabel = _typeOptionLabels[i];
                var type = _typeOptions[i];
                content = new GUIContent($"Create/{typeOptionLabel}");
                if (hasValue)
                {
                    menu.AddDisabledItem(content);
                }
                else
                {
                    menu.AddItem(
                        content,
                        false,
                        CreateNew,
                        (type, property));
                }
            }

            menu.ShowAsContext();
        }

        private void Initialize()
        {
            _initialized = true;

            var fieldType = fieldInfo.FieldType;
            if (fieldType.IsGenericType)
            {
                fieldType = fieldType.GetGenericArguments()[0];
            }
            else if (fieldType.IsArray)
            {
                fieldType = fieldType.GetElementType();
            }

            _typeOptions.Clear();
            if (!fieldType.IsAbstract)
            {
                _typeOptions.Add(fieldType);
            }

            _typeOptions.AddRange(TypeCache.GetTypesDerivedFrom(fieldType));
            _typeOptions.Sort((t1, t2) => string.Compare(t1.Name, t2.Name, StringComparison.OrdinalIgnoreCase));
            _typeOptionLabels = _typeOptions.Select(t => t.Name).ToArray();
        }
    }
}