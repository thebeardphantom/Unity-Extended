using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace BeardPhantom.UnityExtended.Editor
{
    [CustomPropertyDrawer(typeof(Object), true)]
    public class ObjectPropertyDrawer : PropertyDrawer
    {
        private const string STYLESHEET_GUID = "a08593e392de4a94ea9699ea11e91e82";

        private static readonly GUIContent _menuIcon = EditorGUIUtility.TrIconContent("_Menu", "Menu");

        private Button _menuButton;

        private Type _type;
        private ObjectField _objField;

        protected SerializedProperty SerializedProperty { get; private set; }

        /// <inheritdoc />
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label);
        }

        /// <inheritdoc />
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            SerializedProperty = property.Copy();
            if (IsGhostObject())
            {
                SerializedProperty.objectReferenceValue = null;
                SerializedProperty.serializedObject.ApplyModifiedProperties();
            }

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(AssetDatabase.GUIDToAssetPath(STYLESHEET_GUID));

            // Root
            var root = new VisualElement();
            root.AddToClassList("extended-root");
            root.styleSheets.Add(styleSheet);

            // Property field
            _objField = new ObjectField(SerializedProperty.displayName)
            {
                objectType = GetUnpackedFieldInfoType(),
                value = SerializedProperty.objectReferenceValue,
            };
            _objField.Q<Label>(className: "unity-object-field__label").AddToClassList("unity-property-field__label");
            _objField.RegisterValueChangedCallback(OnValueChanged);
            _objField.AddToClassList("unity-base-field__aligned");
            _objField.AddToClassList("extended-property-field");
            root.Add(_objField);

            // Menu button
            _menuButton = new Button
            {
                style =
                {
                    backgroundImage = (Texture2D)_menuIcon.image,
                },
            };
            _menuButton.AddToClassList("extended-button");
            _menuButton.clickable.clickedWithEventInfo += OnMenuButtonClicked;

            root.Add(_menuButton);

            return root;
        }

        protected virtual void PopulateGenericMenu(GenericDropdownMenu menu)
        {
            const string EDIT_LABEL = "Edit";
            if (SerializedProperty.objectReferenceValue.IsNotNull())
            {
                menu.AddItem(EDIT_LABEL, false, EditValue);
            }
            else
            {
                menu.AddDisabledItem(EDIT_LABEL, false);
            }
        }

        protected void UpdateObjectField()
        {
            _objField.SetValueWithoutNotify(SerializedProperty.objectReferenceValue);
        }

        private Type GetUnpackedFieldInfoType()
        {
            if (fieldInfo.FieldType.IsArray)
            {
                return fieldInfo.FieldType.GetElementType();
            }

            if (fieldInfo.FieldType.IsGenericType
                && typeof(List<>).IsAssignableFrom(fieldInfo.FieldType.GetGenericTypeDefinition()))
            {
                return fieldInfo.FieldType.GetGenericArguments()[0];
            }

            return fieldInfo.FieldType;
        }

        /// <summary>
        /// Attempts to check if the assigned value is a deleted subasset stranded in memory.
        /// </summary>
        private bool IsGhostObject()
        {
            var obj = SerializedProperty.objectReferenceValue;
            if (!obj.IsNotNull())
            {
                return false;
            }

            if (!EditorUtility.IsPersistent(SerializedProperty.serializedObject.targetObject))
            {
                // Check if owning object exists in a scene
                // Ghost objects only exist as subassets.
                return false;
            }

            if (AssetDatabase.IsSubAsset(obj))
            {
                // Valid subasset, not a ghost
                return false;
            }

            if (EditorUtility.IsPersistent(obj))
            {
                // Persistent in some way, not a ghost 
                return false;
            }

            // AssetDatabase doesn't contain this at all, ghost
            return !AssetDatabase.Contains(obj);
        }

        private void OnValueChanged(ChangeEvent<Object> evt)
        {
            if (evt.previousValue.IsNotNull() && evt.newValue.IsNull() && AssetDatabase.IsSubAsset(evt.previousValue))
            {
                if (EditorUtility.DisplayDialog(
                        "Confirm Delete",
                        "This action will delete a subasset, are you sure?",
                        "Yes",
                        "No"))
                {
                    AssetDatabase.RemoveObjectFromAsset(evt.previousValue);
                    AssetDatabase.SaveAssets();
                }
                else
                {
                    _objField.SetValueWithoutNotify(evt.previousValue);
                    return;
                }
            }

            SerializedProperty.objectReferenceValue = evt.newValue;
            SerializedProperty.serializedObject.ApplyModifiedProperties();
        }

        private void EditValue()
        {
            EditorUtility.OpenPropertyEditor(SerializedProperty.objectReferenceValue);
        }

        private void OnMenuButtonClicked(EventBase eventBase)
        {
            var btn = (VisualElement)eventBase.target;
            var rect = btn.worldBound;
            var menu = new GenericDropdownMenu();
            PopulateGenericMenu(menu);
            menu.DropDown(rect, btn);
        }
    }
}