using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace BeardPhantom.UnityExtended
{
    /// <summary>
    /// Provides a collection of extension methods for working with Unity's UI Toolkit elements.
    /// </summary>
    public static partial class UIToolkitExtensions
    {
        /// <summary>
        /// Adds the current VisualElement instance to the specified parent VisualElement.
        /// </summary>
        /// <param name="t">The VisualElement instance to be added.</param>
        /// <param name="parent">The parent VisualElement to which the current instance will be added.</param>
        /// <return>Returns the current VisualElement instance after being added to the parent.</return>
        public static T AddTo<T>(this T t, VisualElement parent) where T : VisualElement
        {
            parent.Add(t);
            return t;
        }

        /// <summary>
        /// Sets the provided reference to the current VisualElement instance and returns the instance.
        /// </summary>
        /// <typeparam name="T">The type of the VisualElement.</typeparam>
        /// <param name="t">The current instance of the VisualElement.</param>
        /// <param name="reference">A reference to be set to the current instance.</param>
        /// <returns>The current VisualElement instance with the reference set.</returns>
        public static T SetReference<T>(this T t, ref T reference) where T : VisualElement
        {
            reference = t;
            return t;
        }

        /// <summary>
        /// Adds a child VisualElement to the current VisualElement and returns the current element.
        /// </summary>
        /// <param name="t">The VisualElement to which the child is added.</param>
        /// <param name="child">The VisualElement to add as a child to the current element.</param>
        /// <returns>The current VisualElement, with the child added.</returns>
        public static T WithChild<T>(this T t, VisualElement child) where T : VisualElement
        {
            t.Add(child);
            return t;
        }

        /// <summary>
        /// Adds multiple child elements to the specified parent element.
        /// </summary>
        /// <typeparam name="T">The type of the VisualElement, which must derive from VisualElement.</typeparam>
        /// <param name="t">The parent VisualElement to which the child elements will be added.</param>
        /// <param name="children">An enumerable collection of VisualElement instances to be added to the parent.</param>
        /// <return>Returns the parent VisualElement with the added child elements.</return>
        public static T WithChildren<T>(this T t, IEnumerable<VisualElement> children) where T : VisualElement
        {
            foreach (VisualElement child in children)
            {
                t.Add(child);
            }

            return t;
        }

        /// <summary>
        /// Sets the display style of the specified <see cref="VisualElement" /> to either visible or hidden based on a boolean
        /// value.
        /// </summary>
        /// <typeparam name="T">The type of the VisualElement.</typeparam>
        /// <param name="t">The target VisualElement to modify.</param>
        /// <param name="isDisplaying">
        /// A boolean value indicating whether to display the element (true for visible, false for
        /// hidden).
        /// </param>
        /// <returns>The modified VisualElement with the updated display style.</returns>
        public static T WithDisplay<T>(this T t, bool isDisplaying) where T : VisualElement
        {
            return t.WithDisplay(isDisplaying ? DisplayStyle.Flex : DisplayStyle.None);
        }

        /// <summary>
        /// Adjusts the display style of the specified VisualElement.
        /// </summary>
        /// <typeparam name="T">The type of the VisualElement.</typeparam>
        /// <param name="t">The VisualElement whose display style will be set.</param>
        /// <param name="displayStyle">The desired display style to apply to the VisualElement.</param>
        /// <returns>The VisualElement with the updated display style applied.</returns>
        public static T WithDisplay<T>(this T t, DisplayStyle displayStyle) where T : VisualElement
        {
            t.style.display = displayStyle;
            return t;
        }

        /// <summary>
        /// Configures the visibility state of the VisualElement.
        /// </summary>
        /// <typeparam name="T">The type of the VisualElement.</typeparam>
        /// <param name="t">The VisualElement to configure.</param>
        /// <param name="isVisible">A boolean indicating whether the element should be visible.</param>
        /// <returns>The modified VisualElement instance.</returns>
        public static T WithVisibility<T>(this T t, bool isVisible) where T : VisualElement
        {
            t.visible = isVisible;
            return t;
        }

        /// <summary>
        /// Sets the visibility of the specified VisualElement.
        /// </summary>
        /// <param name="t">The VisualElement on which to set the visibility.</param>
        /// <param name="visibility">The desired visibility state, specified as a Visibility enum.</param>
        /// <return>The modified VisualElement with the updated visibility.</return>
        public static T WithVisibility<T>(this T t, Visibility visibility) where T : VisualElement
        {
            t.style.visibility = visibility;
            return t;
        }

        /// <summary>
        /// Sets whether the specified VisualElement is enabled or disabled.
        /// </summary>
        /// <typeparam name="T">The type of the VisualElement.</typeparam>
        /// <param name="t">The VisualElement instance to modify.</param>
        /// <param name="enabled">A boolean indicating whether the element should be enabled (true) or disabled (false).</param>
        /// <return>Returns the modified VisualElement instance.</return>
        public static T WithEnabled<T>(this T t, bool enabled) where T : VisualElement
        {
            t.SetEnabled(enabled);
            return t;
        }

        /// <summary>
        /// Sets the name of the specified VisualElement and returns the element.
        /// </summary>
        /// <param name="t">The VisualElement to set the name for.</param>
        /// <param name="name">The name to assign to the VisualElement.</param>
        /// <return>The VisualElement with the updated name.</return>
        public static T WithName<T>(this T t, string name) where T : VisualElement
        {
            t.name = name;
            return t;
        }

        /// <summary>
        /// Adds the specified class name to the class list of the visual element.
        /// </summary>
        /// <typeparam name="T">The type of the visual element.</typeparam>
        /// <param name="t">The visual element to add the class to.</param>
        /// <param name="clss">The class name to add.</param>
        /// <returns>The same visual element with the class name added.</returns>
        public static T WithClass<T>(this T t, string clss) where T : VisualElement
        {
            t.AddToClassList(clss);
            return t;
        }

        /// <summary>
        /// Adds a class and a name to the specified VisualElement.
        /// The same string value is used for both the class and name of the element.
        /// </summary>
        /// <param name="t">The target VisualElement to which the class and name will be applied.</param>
        /// <param name="classAndName">The string to be used as both the class name and the element's name.</param>
        /// <returns>The modified VisualElement with the specified class and name applied.</returns>
        public static T WithClassAndName<T>(this T t, string classAndName) where T : VisualElement
        {
            t.AddToClassList(classAndName);
            t.name = classAndName;
            return t;
        }

        /// <summary>
        /// Adds a callback to the button that is invoked when it is clicked.
        /// </summary>
        /// <param name="t">
        /// The button to which the click callback will be added.
        /// </param>
        /// <param name="callback">
        /// The action to be executed when the button is clicked.
        /// </param>
        /// <return>
        /// Returns the button instance with the specified callback added.
        /// </return>
        public static T WithClickedCallback<T>(this T t, Action callback) where T : Button
        {
            t.clicked += callback;
            return t;
        }

        /// <summary>
        /// Sets the text of a Foldout element.
        /// </summary>
        /// <param name="t">The Foldout element to modify.</param>
        /// <param name="text">The text to assign to the Foldout element.</param>
        /// <returns>The modified Foldout element.</returns>
        public static T WithFoldoutText<T>(this T t, string text) where T : Foldout
        {
            t.text = text;
            return t;
        }

        /// <summary>
        /// Sets the value of the foldout element and returns the foldout instance.
        /// </summary>
        /// <typeparam name="T">The type of the foldout element, which must inherit from Foldout.</typeparam>
        /// <param name="t">The foldout element to set the value for.</param>
        /// <param name="value">The boolean value to set for the foldout.</param>
        /// <returns>The foldout element with the specified value applied.</returns>
        public static T WithFoldoutValue<T>(this T t, bool value) where T : Foldout
        {
            t.value = value;
            return t;
        }

        /// <summary>
        /// Sets the text content of the specified TextElement.
        /// </summary>
        /// <param name="t">The TextElement to modify.</param>
        /// <param name="text">The text string to assign to the element.</param>
        /// <return>The modified TextElement instance with the updated text.</return>
        public static T WithText<T>(this T t, string text) where T : TextElement
        {
            t.text = text;
            return t;
        }

        /// <summary>
        /// Sets the value of the BaseField<TValue> without notifying any registered callbacks.
        /// </summary>
        /// <param name="t">The target BaseField on which the value will be set.</param>
        /// <param name="value">The value to set on the BaseField.</param>
        /// <typeparam name="T">The type of the VisualElement, which must be a BaseField of type TValue.</typeparam>
        /// <typeparam name="TValue">The type of the value to set on the BaseField.</typeparam>
        /// <returns>The modified BaseField instance.</returns>
        public static T WithValueWithoutNotify<T, TValue>(this T t, TValue value) where T : BaseField<TValue>
        {
            t.SetValueWithoutNotify(value);
            return t;
        }

        /// <summary>
        /// Sets the label of the specified <see cref="TextField" /> instance.
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="TextField" />.</typeparam>
        /// <param name="t">The <see cref="TextField" /> instance to modify.</param>
        /// <param name="label">The label text to set for the <see cref="TextField" />.</param>
        /// <returns>The modified <see cref="TextField" /> instance.</returns>
        public static T WithLabel<T>(this T t, string label) where T : TextField
        {
            t.label = label;
            return t;
        }

        /// <summary>
        /// Sets the label of a BaseField element and returns the updated element.
        /// This method is useful for configuring a BaseField's label in a fluent API style.
        /// </summary>
        /// <typeparam name="T">The type of the BaseField being configured.</typeparam>
        /// <typeparam name="TValue">The type of the value in the BaseField.</typeparam>
        /// <param name="t">The BaseField instance being configured.</param>
        /// <param name="label">The text to set as the label of the BaseField.</param>
        /// <returns>The updated BaseField instance with the configured label.
        public static T WithBaseFieldLabel<T, TValue>(this T t, string label) where T : BaseField<TValue>
        {
            t.label = label;
            return t;
        }
    }
}