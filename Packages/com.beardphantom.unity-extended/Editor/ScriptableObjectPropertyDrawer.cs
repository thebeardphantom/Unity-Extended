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

        private static bool IsEmbeddedObject(Object obj)
        {
            return obj.IsNotNull() && !EditorUtility.IsPersistent(obj);
        }

        /// <inheritdoc />
        protected override void PopulateGenericMenu(GenericDropdownMenu menu)
        {
            base.PopulateGenericMenu(menu);
            var hasEmbeddedValue = IsEmbeddedObject(SerializedProperty.objectReferenceValue);
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
            Debug.Assert(!IsEmbeddedObject(SerializedProperty.objectReferenceValue), "!GetHasEmbeddedValue()");

            var typeName = Regex.Match(SerializedProperty.type, @"PPtr<\$(.+?)>").Groups[1].Value.Trim();
            var instance = ScriptableObject.CreateInstance(typeName);
            instance.name = "(Embedded)";

            var host = SerializedProperty.serializedObject.targetObject;
            if (EditorUtility.IsPersistent(host))
            {
                AssetDatabase.AddObjectToAsset(instance, host);
                AssetDatabase.SaveAssets();
            }

            SerializedProperty.objectReferenceValue = instance;
            SerializedProperty.serializedObject.ApplyModifiedProperties();
            EditorUtility.OpenPropertyEditor(instance);
        }

        #endregion
    }
}