using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace BeardPhantom.UnityExtended.Editor
{
    public static class CopyUtilities
    {
        #region Fields

        private const string ASSETS_COPY_PREFIX = "Assets/Copy.../";

        private const int PRIORITY = 17;

        private static MethodInfo _makeAssetUriMethod;

        #endregion

        #region Methods

        private static void FindUriMethod()
        {
            const string TYPE_NAME = "UnityEditor.UIElements.StyleSheets.URIHelpers";
            var assembly = typeof(UIElementsEntryPoint).Assembly;
            var type = assembly.GetType(TYPE_NAME);
            _makeAssetUriMethod = type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .FirstOrDefault(m => m.Name == "MakeAssetUri" && m.GetParameters().Length == 2);
        }

        [MenuItem(ASSETS_COPY_PREFIX + "GUID", true, priority = PRIORITY)]
        [MenuItem(ASSETS_COPY_PREFIX + "Path", true, priority = PRIORITY)]
        [MenuItem(ASSETS_COPY_PREFIX + "Local File ID", true, priority = PRIORITY)]
        [MenuItem(ASSETS_COPY_PREFIX + "UI Toolkit URI", true, priority = PRIORITY)]
        private static bool CopyValidate()
        {
            return Selection.activeObject.IsNotNull();
        }

        [MenuItem(ASSETS_COPY_PREFIX + "Path", priority = PRIORITY)]
        private static void CopyPath()
        {
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            GUIUtility.systemCopyBuffer = path;
        }

        [MenuItem(ASSETS_COPY_PREFIX + "GUID", priority = PRIORITY)]
        private static void CopyGuid()
        {
            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(Selection.activeObject, out var guid, out long _);
            GUIUtility.systemCopyBuffer = guid;
        }

        [MenuItem(ASSETS_COPY_PREFIX + "Local File ID", priority = PRIORITY)]
        private static void CopyLocalFileId()
        {
            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(Selection.activeObject, out _, out long localId);
            GUIUtility.systemCopyBuffer = localId.ToString();
        }

        [MenuItem(ASSETS_COPY_PREFIX + "UI Toolkit URI", priority = PRIORITY)]
        private static void CopyUIToolkitUri()
        {
            var assetObject = Selection.activeObject;
            if (_makeAssetUriMethod.IsNull())
            {
                FindUriMethod();
            }

            var uri = (string)_makeAssetUriMethod.Invoke(
                null,
                new object[]
                {
                    assetObject,
                    false
                });
            GUIUtility.systemCopyBuffer = uri;
        }

        #endregion
    }
}