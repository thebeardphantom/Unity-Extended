using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;
using ObjectField = UnityEditor.Search.ObjectField;

namespace BeardPhantom.UnityExtended.Editor
{
    [CustomPropertyDrawer(typeof(InterfaceReference<>), true)]
    public class InterfaceReferenceDrawer : PropertyDrawer
    {
        private readonly List<string> _choices = new();

        private readonly List<Component> _components = new();

        private Type _interfaceType;

        private GenericMenu _menu;

        private ObjectField _gameObjectField;

        private DropdownField _componentDropdown;

        private SerializedProperty _componentProp;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            _componentProp = property.FindPropertyRelative("<Component>k__BackingField");
            _interfaceType = GetInterfaceType();

            var root = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                },
            };

            _gameObjectField = new ObjectField(property.displayName)
            {
                objectType = typeof(GameObject),
                searchContext = SearchService.CreateContext("scene", $"t:{_interfaceType.FullName}"),
                style =
                {
                    flexGrow = 1f,
                },
            };
            _gameObjectField.RegisterValueChangedCallback(OnGameObjectFieldValueChanged);
            root.Add(_gameObjectField);

            _componentDropdown = new DropdownField
            {
                choices = _choices,
                style =
                {
                    flexGrow = 0.5f,
                },
            };
            _componentDropdown.RegisterValueChangedCallback(OnComponentDropdownValueChanged);
            root.Add(_componentDropdown);

            root.RegisterCallbackOnce<AttachToPanelEvent>(_ =>
            {
                SetFromSerializedProperty();
            });
            return root;
        }

        private void OnComponentDropdownValueChanged(ChangeEvent<string> evt)
        {
            _componentProp.objectReferenceValue = _componentDropdown.index >= 0 ? _components[_componentDropdown.index] : null;
            _componentProp.serializedObject.ApplyModifiedProperties();
        }

        private void OnGameObjectFieldValueChanged(ChangeEvent<Object> evt)
        {
            RefreshComponentDropdownChoices();
        }

        private void RefreshComponentDropdownChoices()
        {
            _choices.Clear();
            _components.Clear();
            if (_gameObjectField.value is GameObject gameObject)
            {
                gameObject.GetComponents(_interfaceType, _components);
                _choices.AddRange(_components.Select((component, index) => $"{index + 1}: {component.GetType().FullName}"));
            }

            _componentDropdown.choices = _choices;
        }

        private void SetGameObject(GameObject gameObject)
        {
            _gameObjectField.SetValueWithoutNotify(gameObject);
            RefreshComponentDropdownChoices();
        }

        private void SetFromSerializedProperty()
        {
            var cmp = _componentProp.objectReferenceValue as Component;
            if (cmp == null)
            {
                SetGameObject(null);
                _componentDropdown.SetValueWithoutNotify(null);
            }
            else if (!_interfaceType.IsInstanceOfType(cmp))
            {
                SetGameObject(cmp.gameObject);
                _componentDropdown.SetValueWithoutNotify(null);
            }
            else
            {
                SetGameObject(cmp.gameObject);
                _componentDropdown.index = _components.IndexOf(cmp);
            }
        }

        private Type GetInterfaceType()
        {
            // Extract the generic type parameter from the serialized type string
            Type type = fieldInfo.FieldType;
            return type.IsGenericType ? type.GetGenericArguments()[0] : typeof(object);
        }
    }
}