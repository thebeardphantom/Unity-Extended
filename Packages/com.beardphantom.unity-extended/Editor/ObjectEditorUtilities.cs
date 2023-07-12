using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UnityExtended.Editor
{
    [InitializeOnLoad]
    public static class ObjectEditorUtilities
    {
        #region Constructors

        static ObjectEditorUtilities()
        {
            UnityEditor.Editor.finishedDefaultHeaderGUI += OnPostHeader;
        }

        #endregion

        #region Methods

        private static void OnPostHeader(UnityEditor.Editor editor)
        {
            if (editor.target is AssetImporter
                || PrefabUtility.IsPartOfPrefabAsset(editor.target)
                || !EditorUtility.IsPersistent(editor.target)
                || (editor.target.hideFlags & HideFlags.NotEditable) != 0)
            {
                return;
            }

            editor.serializedObject.Update();
            var name = editor.serializedObject.FindProperty("m_Name");
            if (name == null)
            {
                return;
            }

            using (var changeCheck = new EditorGUI.ChangeCheckScope())
            {
                name.stringValue = EditorGUILayout.DelayedTextField("Asset Name", name.stringValue);
                if (changeCheck.changed)
                {
                    editor.serializedObject.ApplyModifiedProperties();
                    AssetDatabase.SaveAssets();
                }
            }
        }

        #endregion
    }
}