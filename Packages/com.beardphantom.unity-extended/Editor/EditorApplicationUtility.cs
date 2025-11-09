using System;
using System.Reflection;
using UnityEditor;

namespace BeardPhantom.UnityExtended.Editor
{
    [InitializeOnLoad]
    public static class EditorApplicationUtility
    {
        public static event EditorApplication.CallbackFunction GlobalEventHandler
        {
            add
            {
                var callback = (EditorApplication.CallbackFunction)s_globalEventHandler.GetValue(null);
                callback += value;
                s_globalEventHandler.SetValue(null, callback);
            }
            remove
            {
                var callback = (EditorApplication.CallbackFunction)s_globalEventHandler.GetValue(null);
                callback -= value;
                s_globalEventHandler.SetValue(null, callback);
            }
        }

        private static readonly FieldInfo s_globalEventHandler;

        static EditorApplicationUtility()
        {
            Type type = typeof(EditorApplication);
            s_globalEventHandler = type.GetField(
                "globalEventHandler",
                BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        }
    }
}