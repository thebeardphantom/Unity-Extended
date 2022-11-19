using System.IO;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UnityExtended.Editor
{
    public static class ProjectWindowExtensions
    {
        #region Methods

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