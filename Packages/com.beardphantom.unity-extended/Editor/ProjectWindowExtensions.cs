using System.IO;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UnityExtended.Editor
{
    public static class ProjectWindowExtensions
    {
        #region Fields

        private const string DELETE_SUBASSET = "Assets/Delete Subasset";

        private const string DELETE_SUBASSET_CONTEXT = "CONTEXT/ScriptableObject/Delete Subasset";

        #endregion

        #region Methods

        private static bool IsValidSubasset(Object obj)
        {
            return obj.IsNotNull() && AssetDatabase.IsSubAsset(obj);
        }

        private static void DeleteSubasset(Object obj)
        {
            if (Selection.activeObject == obj)
            {
                var path = AssetDatabase.GetAssetPath(obj);
                Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(path);
            }

            AssetDatabase.RemoveObjectFromAsset(obj);
            AssetDatabase.SaveAssets();
        }

        [MenuItem(DELETE_SUBASSET, true)]
        private static bool DeleteSubassetValidate()
        {
            return IsValidSubasset(Selection.activeObject);
        }

        [MenuItem(DELETE_SUBASSET, priority = 16)]
        private static void DeleteSubasset()
        {
            DeleteSubasset(Selection.activeObject);
        }

        [MenuItem(DELETE_SUBASSET_CONTEXT, true)]
        private static bool DeleteSubassetContextValidate(MenuCommand cmd)
        {
            return IsValidSubasset(cmd.context);
        }

        [MenuItem(DELETE_SUBASSET_CONTEXT)]
        private static void DeleteSubassetContext(MenuCommand cmd)
        {
            DeleteSubasset(cmd.context);
        }

        [MenuItem("Assets/Replace Source")]
        private static void ReplaceSourceAsset()
        {
            var guids = Selection.assetGUIDs;
            foreach (var guid in guids)
            {
                var originalPath = AssetDatabase.GUIDToAssetPath(guid);
                var ext = Path.GetExtension(originalPath).Substring(1);
                var type = AssetDatabase.GetMainAssetTypeAtPath(originalPath);
                var originalName = Path.GetFileNameWithoutExtension(originalPath);
                var replacementPath = EditorUtility.OpenFilePanelWithFilters(
                    $"Replace Source Asset '{originalPath}'",
                    "",
                    new[]
                    {
                        type.Name,
                        ext
                    });
                if (string.IsNullOrWhiteSpace(replacementPath))
                {
                    break;
                }

                File.Copy(replacementPath, originalPath, true);
                AssetDatabase.ImportAsset(originalPath);
                AssetDatabase.LoadAssetAtPath<Object>(originalPath).name = originalName;
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        #endregion
    }
}