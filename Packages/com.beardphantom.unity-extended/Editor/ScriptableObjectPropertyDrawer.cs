using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace BeardPhantom.UnityExtended.Editor
{
    [CustomPropertyDrawer(typeof(ScriptableObject), true)]
    public class ScriptableObjectPropertyDrawer : ObjectPropertyDrawer
    {
        #region Methods

        /// <inheritdoc />
        protected override void PopulateGenericMenu(GenericDropdownMenu menu)
        {
            base.PopulateGenericMenu(menu);
            var hasEmbeddedValue = GetHasEmbeddedValue();
            const string ADD_LABEL = "Add";
            if (hasEmbeddedValue)
            {
                menu.AddDisabledItem(ADD_LABEL, false);
            }
            else
            {
                menu.AddItem(ADD_LABEL, false, CreateNewEmbedded);
            }
        }

        private void CreateNewEmbedded()
        {
            Debug.Assert(!GetHasEmbeddedValue(), "!GetHasEmbeddedValue()");

            var typeName = Regex.Match(SerializedProperty.type, @"PPtr<\$(.+?)>").Groups[1].Value.Trim();
            var instance = ScriptableObject.CreateInstance(typeName);
            instance.name = "(Embedded)";
            SerializedProperty.objectReferenceValue = instance;
            SerializedProperty.serializedObject.ApplyModifiedProperties();
            EditorUtility.OpenPropertyEditor(instance);
        }

        private bool GetHasEmbeddedValue()
        {
            var currentValue = SerializedProperty.objectReferenceValue;
            return currentValue.IsNotNull() && !EditorUtility.IsPersistent(currentValue);
        }

        #endregion
    }
}