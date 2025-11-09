using System;
using System.Linq;
using System.Reflection;
using System.Security;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BeardPhantom.UnityExtended.Editor
{
    public static class CopyUtilities
    {
        private const string AssetsCopyPrefix = "Assets/Copy.../";

        private const int Priority = 17;

        private static MethodInfo s_makeAssetUriMethod;

        private static void FindUriMethod()
        {
            const string TypeName = "UnityEditor.UIElements.StyleSheets.URIHelpers";
            Assembly assembly = typeof(UIElementsEntryPoint).Assembly;
            Type type = assembly.GetType(TypeName);
            s_makeAssetUriMethod = type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .FirstOrDefault(m => m.Name == "MakeAssetUri" && m.GetParameters().Length == 2);
        }

        [MenuItem(AssetsCopyPrefix + "GUID", true, priority = Priority)]
        [MenuItem(AssetsCopyPrefix + "Path", true, priority = Priority)]
        [MenuItem(AssetsCopyPrefix + "Local File ID", true, priority = Priority)]
        [MenuItem(AssetsCopyPrefix + "UI Toolkit URI", true, priority = Priority)]
        private static bool CopyValidate()
        {
            return Selection.activeObject.IsNotNull();
        }

        [MenuItem(AssetsCopyPrefix + "Path", priority = Priority)]
        private static void CopyPath()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            GUIUtility.systemCopyBuffer = path;
        }

        [MenuItem(AssetsCopyPrefix + "GUID", priority = Priority)]
        private static void CopyGuid()
        {
            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(Selection.activeObject, out string guid, out long _);
            GUIUtility.systemCopyBuffer = guid;
        }

        [MenuItem(AssetsCopyPrefix + "Local File ID", priority = Priority)]
        private static void CopyLocalFileId()
        {
            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(Selection.activeObject, out _, out long localId);
            GUIUtility.systemCopyBuffer = localId.ToString();
        }

        [MenuItem(AssetsCopyPrefix + "UI Toolkit URI", priority = Priority)]
        private static void CopyUIToolkitUri()
        {
            Object assetObject = Selection.activeObject;
            if (s_makeAssetUriMethod.IsNull())
            {
                FindUriMethod();
            }

            var uri = (string)s_makeAssetUriMethod.Invoke(
                null,
                new object[]
                {
                    assetObject,
                    false,
                });
            uri = SecurityElement.Escape(uri);
            GUIUtility.systemCopyBuffer = uri;
        }
    }
}