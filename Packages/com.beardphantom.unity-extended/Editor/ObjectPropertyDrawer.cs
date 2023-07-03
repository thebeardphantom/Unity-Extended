using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace BeardPhantom.UnityExtended.Editor
{
    [CustomPropertyDrawer(typeof(Object), true)]
    public class ObjectPropertyDrawer : PropertyDrawer
    {
        #region Fields

        private const string STYLESHEET_GUID = "a08593e392de4a94ea9699ea11e91e82";

        private static readonly GUIContent _menuIcon = EditorGUIUtility.TrIconContent("_Menu", "Menu");

        private Button _menuButton;

        #endregion

        #region Properties

        protected SerializedProperty SerializedProperty { get; private set; }

        #endregion

        #region Methods

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            SerializedProperty = property.Copy();

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(AssetDatabase.GUIDToAssetPath(STYLESHEET_GUID));

            // Root
            var root = new VisualElement();
            root.AddToClassList("extended-root");
            root.styleSheets.Add(styleSheet);

            // Property field
            var propertyField = new PropertyField();
            propertyField.BindProperty(SerializedProperty);
            propertyField.AddToClassList("extended-property-field");
            root.Add(propertyField);

            // Menu button
            _menuButton = new Button
            {
                style =
                {
                    backgroundImage = (Texture2D)_menuIcon.image
                }
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

        #endregion
    }
}