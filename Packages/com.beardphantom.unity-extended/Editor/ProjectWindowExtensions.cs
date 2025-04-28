using System;
using System.IO;
using UnityEditor;
using Object = UnityEngine.Object;

namespace BeardPhantom.UnityExtended.Editor
{
    public static class ProjectWindowExtensions
    {
        private const string DELETE_SUBASSET = "Assets/Delete Subasset";

        private const string DELETE_SUBASSET_CONTEXT = "CONTEXT/ScriptableObject/Delete Subasset";

        private static bool IsValidSubasset(Object obj)
        {
            return obj.IsNotNull() && AssetDatabase.IsSubAsset(obj);
        }

        private static void DeleteSubasset(Object obj)
        {
            if (Selection.activeObject == obj)
            {
                string path = AssetDatabase.GetAssetPath(obj);
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
            string[] guids = Selection.assetGUIDs;
            foreach (string guid in guids)
            {
                string originalPath = AssetDatabase.GUIDToAssetPath(guid);
                string ext = Path.GetExtension(originalPath).Substring(1);
                Type type = AssetDatabase.GetMainAssetTypeAtPath(originalPath);
                string originalName = Path.GetFileNameWithoutExtension(originalPath);
                string replacementPath = EditorUtility.OpenFilePanelWithFilters(
                    $"Replace Source Asset '{originalPath}'",
                    "",
                    new[]
                    {
                        type.Name,
                        ext,
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
    }
}