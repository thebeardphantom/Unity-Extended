using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace BeardPhantom.UnityExtended
{
    public static partial class UIToolkitExtensions
    {
        public static T AddTo<T>(this T t, VisualElement parent) where T : VisualElement
        {
            parent.Add(t);
            return t;
        }

        public static T SetReference<T>(this T t, ref T reference) where T : VisualElement
        {
            reference = t;
            return t;
        }

        public static T WithChild<T>(this T t, VisualElement child) where T : VisualElement
        {
            t.Add(child);
            return t;
        }

        public static T WithChildren<T>(this T t, IEnumerable<VisualElement> children) where T : VisualElement
        {
            foreach (VisualElement child in children)
            {
                t.Add(child);
            }

            return t;
        }

        public static T WithDisplay<T>(this T t, bool isDisplaying) where T : VisualElement
        {
            return t.WithDisplay(isDisplaying ? DisplayStyle.Flex : DisplayStyle.None);
        }

        public static T WithDisplay<T>(this T t, DisplayStyle displayStyle) where T : VisualElement
        {
            t.style.display = displayStyle;
            return t;
        }

        public static T WithVisibility<T>(this T t, bool isVisible) where T : VisualElement
        {
            t.visible = isVisible;
            return t;
        }

        public static T WithVisibility<T>(this T t, Visibility visibility) where T : VisualElement
        {
            t.style.visibility = visibility;
            return t;
        }

        public static T WithEnabled<T>(this T t, bool enabled) where T : VisualElement
        {
            t.SetEnabled(enabled);
            return t;
        }

        public static T WithName<T>(this T t, string name) where T : VisualElement
        {
            t.name = name;
            return t;
        }

        public static T WithClass<T>(this T t, string clss) where T : VisualElement
        {
            t.AddToClassList(clss);
            return t;
        }

        public static T WithClassAndName<T>(this T t, string classAndName) where T : VisualElement
        {
            t.AddToClassList(classAndName);
            t.name = classAndName;
            return t;
        }

        public static T WithClickedCallback<T>(this T t, Action callback) where T : Button
        {
            t.clicked += callback;
            return t;
        }

        public static T WithFoldoutText<T>(this T t, string text) where T : Foldout
        {
            t.text = text;
            return t;
        }

        public static T WithFoldoutValue<T>(this T t, bool value) where T : Foldout
        {
            t.value = value;
            return t;
        }

        public static T WithText<T>(this T t, string text) where T : TextElement
        {
            t.text = text;
            return t;
        }

        public static T WithValueWithoutNotify<T, TValue>(this T t, TValue value) where T : BaseField<TValue>
        {
            t.SetValueWithoutNotify(value);
            return t;
        }

        public static T WithLabel<T>(this T t, string label) where T : TextField
        {
            t.label = label;
            return t;
        }

        public static T WithBaseFieldLabel<T, TValue>(this T t, string label) where T : BaseField<TValue>
        {
            t.label = label;
            return t;
        }
    }
}