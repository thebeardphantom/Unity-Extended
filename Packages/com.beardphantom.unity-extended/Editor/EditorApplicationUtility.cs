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
                var callback = (EditorApplication.CallbackFunction)_globalEventHandler.GetValue(null);
                callback += value;
                _globalEventHandler.SetValue(null, callback);
            }
            remove
            {
                var callback = (EditorApplication.CallbackFunction)_globalEventHandler.GetValue(null);
                callback -= value;
                _globalEventHandler.SetValue(null, callback);
            }
        }

        private static readonly FieldInfo _globalEventHandler;

        static EditorApplicationUtility()
        {
            var type = typeof(EditorApplication);
            _globalEventHandler = type.GetField(
                "globalEventHandler",
                BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        }
    }
}