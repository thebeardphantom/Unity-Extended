using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace BeardPhantom.UnityExtended.Editor
{
    // [CustomPropertyDrawer(typeof(Object), true)]
    public class ObjectPropertyDrawer : PropertyDrawer
    {
        private static readonly GUIContent s_menuIcon = EditorGUIUtility.TrIconContent("_Menu", "Menu");

        private static readonly GUID s_stylesheetGuid = new("a08593e392de4a94ea9699ea11e91e82");

        private Button _menuButton;

        private Type _type;

        private VisualElement _root;

        private Foldout _foldout;

        private PropertyField _propertyField;

        private InspectorElement _inspectorElement;

        private SerializedProperty _property;

        private Toggle _foldoutToggle;

        private Type _propertyType;

        private ObjectField _propertyFieldObjectField;

        protected SerializedProperty SerializedProperty { get; private set; }

        private static bool IsEmbeddedObject(Object obj)
        {
            return obj.IsNotNull() && (!EditorUtility.IsPersistent(obj) || AssetDatabase.IsSubAsset(obj));
        }

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
            _propertyType = GetUnpackedFieldInfoType();
            if (_propertyType != typeof(Material) && !typeof(ScriptableObject).IsAssignableFrom(_propertyType))
            {
                var propertyField = new PropertyField(property);
                propertyField.Bind(property.serializedObject);
                return propertyField;
            }

            // if (IsGhostObject())
            // {
            //     SerializedProperty.objectReferenceValue = null;
            //     SerializedProperty.serializedObject.ApplyModifiedProperties();
            // }

            SerializedProperty = property.Copy();
            var styleSheet = AssetDatabase.LoadAssetByGUID<StyleSheet>(s_stylesheetGuid);
            _root = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                },
            };
            _root.styleSheets.Add(styleSheet);
            _foldout = new Foldout
            {
                text = SerializedProperty.displayName,
                value = SerializedProperty.isExpanded,
                style = { flexGrow = 1f, },
            };
            var foldoutLabel = _foldout.Q<Label>();
            foldoutLabel.style.flexGrow = 0f;
            foldoutLabel.AddToClassList(PropertyField.labelUssClassName);
            _foldout.RegisterValueChangedCallback(evt =>
            {
                SerializedProperty.isExpanded = evt.newValue;
            });
            _root.Add(_foldout);
            _propertyField = new PropertyField(SerializedProperty, "")
            {
                style =
                {
                    flexGrow = 1f,
                    width = 0f,
                    minWidth = 0f,
                },
            };
            _propertyField.RegisterCallbackOnce<GeometryChangedEvent>(_ =>
            {
                _propertyFieldObjectField = _propertyField.Q<ObjectField>();
                _propertyFieldObjectField.RegisterValueChangedCallback(OnValueChanged);
            });
            var newFoldoutHeader = new VisualElement
            {
                style = { flexDirection = FlexDirection.Row, },
            };
            _foldoutToggle = _foldout.Q<Toggle>();
            _foldout.hierarchy.Insert(0, newFoldoutHeader);
            newFoldoutHeader.Add(_foldoutToggle);
            newFoldoutHeader.Add(_propertyField);

            // Menu button
            _menuButton = new Button
            {
                style =
                {
                    backgroundImage = (Texture2D)s_menuIcon.image,
                },
            };
            _menuButton.AddToClassList("extended-button");
            _menuButton.clickable.clickedWithEventInfo += OnMenuButtonClicked;

            newFoldoutHeader.Add(_menuButton);

            UpdateInspectorElement(SerializedProperty.objectReferenceValue);
            return _root;
        }

        private void PopulateGenericMenu(GenericDropdownMenu menu)
        {
            const string EditLabel = "Edit";
            if (SerializedProperty.objectReferenceValue.IsNotNull())
            {
                menu.AddItem(EditLabel, false, EditValue);
            }
            else
            {
                menu.AddDisabledItem(EditLabel, false);
            }

            bool hasEmbeddedValue = IsEmbeddedObject(SerializedProperty.objectReferenceValue);
            const string AddLabel = "Add";
            if (hasEmbeddedValue)
            {
                menu.AddDisabledItem(AddLabel, false);
            }
            else
            {
                menu.AddItem(AddLabel, false, CreateNewEmbedded);
            }
        }

        private void UpdateInspectorElement(Object inspectedObj)
        {
            _inspectorElement?.RemoveFromHierarchy();
            if (inspectedObj == null)
            {
                _foldout.SetValueWithoutNotify(false);
                _foldoutToggle.Q(className: Toggle.checkmarkUssClassName).WithVisibility(false);
                _inspectorElement = null;
            }
            else
            {
                _foldout.SetValueWithoutNotify(SerializedProperty.isExpanded);
                _foldoutToggle.Q(className: Toggle.checkmarkUssClassName).WithVisibility(true);
                _inspectorElement?.RemoveFromHierarchy();
                _inspectorElement = new InspectorElement(inspectedObj)
                {
                    style =
                    {
                        paddingLeft = 0f,
                        paddingBottom = 0f,
                        paddingRight = 0f,
                        paddingTop = 0f,
                    },
                };
                _foldout.Add(_inspectorElement);
            }
        }

        private Type GetUnpackedFieldInfoType()
        {
            if (fieldInfo.FieldType.IsArray)
            {
                return fieldInfo.FieldType.GetElementType();
            }

            if (fieldInfo.FieldType.IsGenericType && typeof(List<>).IsAssignableFrom(fieldInfo.FieldType.GetGenericTypeDefinition()))
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
            Object obj = SerializedProperty.objectReferenceValue;
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
            if (!evt.previousValue.IsNotNull() || !evt.newValue.IsNull() || !AssetDatabase.IsSubAsset(evt.previousValue))
            {
                UpdateInspectorElement(evt.newValue);
                return;
            }

            if (EditorUtility.DisplayDialog(
                    "Confirm Delete",
                    "This action will delete a subasset, are you sure?",
                    "Yes",
                    "No"))
            {
                AssetDatabase.RemoveObjectFromAsset(evt.previousValue);
                AssetDatabase.SaveAssets();
                UpdateInspectorElement(null);
            }
            else
            {
                _propertyFieldObjectField.SetValueWithoutNotify(evt.previousValue);
                UpdateInspectorElement(evt.previousValue);
            }
        }

        private void EditValue()
        {
            EditorUtility.OpenPropertyEditor(SerializedProperty.objectReferenceValue);
        }

        private void OnMenuButtonClicked(EventBase eventBase)
        {
            var btn = (VisualElement)eventBase.target;
            Rect rect = btn.worldBound;
            var menu = new GenericDropdownMenu();
            PopulateGenericMenu(menu);
            menu.DropDown(rect, btn, false);
        }

        private void CreateNewEmbedded()
        {
            Debug.Assert(!IsEmbeddedObject(SerializedProperty.objectReferenceValue), "!GetHasEmbeddedValue()");

            var instance = ScriptableObject.CreateInstance(_propertyType.FullName);
            instance.name = $"(Embedded {Guid.NewGuid():N})";

            Object host = SerializedProperty.serializedObject.targetObject;
            if (EditorUtility.IsPersistent(host))
            {
                string propertyPath = SerializedProperty.propertyPath;
                AssetDatabase.AddObjectToAsset(instance, host);
                AssetDatabase.SaveAssets();

                // AssetDatabase operations dispose the SerializedObject, need to recreate
                SerializedProperty = new SerializedObject(host).FindProperty(propertyPath);
            }
            else
            {
                GameObject gObj = host switch
                {
                    GameObject gameObject => gameObject,
                    Component cmp => cmp.gameObject,
                    _ => null,
                };

                if (gObj.IsNotNull())
                {
                    PrefabStage prefabStage = PrefabStageUtility.GetPrefabStage(gObj);
                    if (prefabStage != null)
                    {
                        string propertyPath = SerializedProperty.propertyPath;
                        AssetDatabase.AddObjectToAsset(instance, prefabStage.assetPath);
                        AssetDatabase.SaveAssets();
                        SerializedProperty = new SerializedObject(host).FindProperty(propertyPath);
                    }
                }
            }

            SerializedProperty.objectReferenceValue = instance;
            SerializedProperty.serializedObject.ApplyModifiedProperties();
        }
    }
}