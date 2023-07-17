using System.Reflection;
using UnityEditor;

namespace BeardPhantom.UnityExtended.Editor
{
    [InitializeOnLoad]
    public static class EditorApplicationUtility
    {
        #region Events

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

        #endregion

        #region Fields

        private static readonly FieldInfo _globalEventHandler;

        #endregion

        #region Constructors

        static EditorApplicationUtility()
        {
            var type = typeof(EditorApplication);
            _globalEventHandler = type.GetField(
                "globalEventHandler",
                BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        }

        #endregion
    }
}