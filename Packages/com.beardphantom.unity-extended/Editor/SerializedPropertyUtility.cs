using System;
using System.Reflection;
using UnityEditor;
using UnityEngine.Assertions;

namespace BeardPhantom.UnityExtended.Editor
{
    public static class SerializedPropertyUtility
    {
        private static readonly MethodInfo _getFieldInfoFromProperty;

        static SerializedPropertyUtility()
        {
            Type scriptAttributeUtility = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.ScriptAttributeUtility");
            Assert.IsNotNull(scriptAttributeUtility, "ScriptAttributeUtility != null");

            _getFieldInfoFromProperty = scriptAttributeUtility.GetMethod(
                nameof(GetFieldInfoFromProperty),
                BindingFlags.NonPublic | BindingFlags.Static);
            Assert.IsNotNull(_getFieldInfoFromProperty, "getFieldInfoFromProperty != null");
        }

        public static FieldInfo GetFieldInfoFromProperty(this SerializedProperty property, out Type type)
        {
            type = null;
            var fieldInfo = (FieldInfo)_getFieldInfoFromProperty.Invoke(
                null,
                new object[]
                {
                    property,
                    type,
                });
            return fieldInfo;
        }
    }
}