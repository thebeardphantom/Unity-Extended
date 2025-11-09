using System;
using System.Reflection;
using UnityEditor;
using UnityEngine.Assertions;

namespace BeardPhantom.UnityExtended.Editor
{
    public static class SerializedPropertyUtility
    {
        private static readonly MethodInfo s_getFieldInfoFromProperty;

        static SerializedPropertyUtility()
        {
            Type scriptAttributeUtility = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.ScriptAttributeUtility");
            Assert.IsNotNull(scriptAttributeUtility, "ScriptAttributeUtility != null");

            s_getFieldInfoFromProperty = scriptAttributeUtility.GetMethod(
                nameof(GetFieldInfoFromProperty),
                BindingFlags.NonPublic | BindingFlags.Static);
            Assert.IsNotNull(s_getFieldInfoFromProperty, "getFieldInfoFromProperty != null");
        }

        public static FieldInfo GetFieldInfoFromProperty(this SerializedProperty property, out Type type)
        {
            type = null;
            var args = new object[]
            {
                property,
                type,
            };
            var fieldInfo = (FieldInfo)s_getFieldInfoFromProperty.Invoke(
                null,
                args);
            type = (Type)args[1];
            return fieldInfo;
        }
    }
}