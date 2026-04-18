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
        /// Queries the specified VisualElement for a child element that matches the provided name and class name.
        /// Throws an exception if the child element is not found.
        /// </summary>
        /// <param name="e">The parent VisualElement to query.</param>
        /// <param name="name">The optional name of the child element to query for.</param>
        /// <param name="className">The optional class name of the child element to query for.</param>
        /// <return>Returns the modified VisualElement.</return>
        /// <exception cref="Exception">Thrown if no element matching the query is found.</exception>
        public static VisualElement QRequired(this VisualElement e, string name = null, string className = null)
        {
            VisualElement result = e.Q(name, className);
            if (result == null)
            {
                throw new Exception($"Query on {e} returned no element: name = '{name}' className = '{className}'");
            }

            return result;
        }

        /// <summary>
        /// Queries a child VisualElement of a specified type from the current VisualElement, using a name, class name, or both,
        /// and throws an exception if no matching element is found.
        /// </summary>
        /// <typeparam name="T">The type of the VisualElement to query.</typeparam>
        /// <param name="e">The parent VisualElement to query from.</param>
        /// <param name="name">An optional name to filter the query results.</param>
        /// <param name="className">An optional class name to filter the query results.</param>
        /// <return>Returns the modified VisualElement.</return>
        public static T QRequired<T>(this VisualElement e, string name = null, string className = null) where T : VisualElement
        {
            var result = e.Q<T>(name, className);
            if (result == null)
            {
                throw new Exception($"Query on {e} returned no element: name = '{name}' className = '{className}'");
            }

            return result;
        }

        /// <summary>
        /// Adds the current VisualElement instance to the specified parent VisualElement.
        /// </summary>
        /// <param name="element">The VisualElement instance to be added.</param>
        /// <param name="parent">The parent VisualElement to which the current instance will be added.</param>
        /// <return>Returns the modified VisualElement.</return>
        public static T AddTo<T>(this T element, VisualElement parent) where T : VisualElement
        {
            parent.Add(element);
            return element;
        }

        /// <summary>
        /// Sets the provided reference to the current VisualElement instance and returns the instance.
        /// </summary>
        /// <typeparam name="T">The type of the VisualElement.</typeparam>
        /// <param name="element">The current instance of the VisualElement.</param>
        /// <param name="reference">A reference to be set to the current instance.</param>
        /// <return>Returns the modified VisualElement.</return>
        public static T SetReference<T>(this T element, ref T reference) where T : VisualElement
        {
            reference = element;
            return element;
        }

        /// <summary>
        /// Adds a child VisualElement to the current VisualElement and returns the current element.
        /// </summary>
        /// <param name="element">The VisualElement to which the child is added.</param>
        /// <param name="child">The VisualElement to add as a child to the current element.</param>
        /// <return>Returns the modified VisualElement.</return>
        public static T WithChild<T>(this T element, VisualElement child) where T : VisualElement
        {
            element.Add(child);
            return element;
        }

        /// <summary>
        /// Adds multiple child elements to the specified parent element.
        /// </summary>
        /// <typeparam name="T">The type of the VisualElement, which must derive from VisualElement.</typeparam>
        /// <param name="element">The parent VisualElement to which the child elements will be added.</param>
        /// <param name="children">An enumerable collection of VisualElement instances to be added to the parent.</param>
        /// <return>Returns the modified VisualElement.</return>
        public static T WithChildren<T>(this T element, IEnumerable<VisualElement> children) where T : VisualElement
        {
            foreach (VisualElement child in children)
            {
                element.Add(child);
            }

            return element;
        }

        /// <summary>
        /// Sets the display style of the specified <see cref="VisualElement" /> to either visible or hidden based on a boolean
        /// value.
        /// </summary>
        /// <typeparam name="T">The type of the VisualElement.</typeparam>
        /// <param name="element">The target VisualElement to modify.</param>
        /// <param name="isDisplaying">
        /// A boolean value indicating whether to display the element (true for visible, false for
        /// hidden).
        /// </param>
        /// <return>Returns the modified VisualElement.</return>
        public static T WithDisplay<T>(this T element, bool isDisplaying) where T : VisualElement
        {
            return element.WithDisplay(isDisplaying ? DisplayStyle.Flex : DisplayStyle.None);
        }

        /// <summary>
        /// Adjusts the display style of the specified VisualElement.
        /// </summary>
        /// <typeparam name="T">The type of the VisualElement.</typeparam>
        /// <param name="element">The VisualElement whose display style will be set.</param>
        /// <param name="displayStyle">The desired display style to apply to the VisualElement.</param>
        /// <return>Returns the modified VisualElement.</return>
        public static T WithDisplay<T>(this T element, DisplayStyle displayStyle) where T : VisualElement
        {
            element.style.display = displayStyle;
            return element;
        }

        /// <summary>
        /// Configures the visibility state of the VisualElement.
        /// </summary>
        /// <typeparam name="T">The type of the VisualElement.</typeparam>
        /// <param name="element">The VisualElement to configure.</param>
        /// <param name="isVisible">A boolean indicating whether the element should be visible.</param>
        /// <return>Returns the modified VisualElement.</return>
        public static T WithVisibility<T>(this T element, bool isVisible) where T : VisualElement
        {
            element.visible = isVisible;
            return element;
        }

        /// <summary>
        /// Sets the visibility of the specified VisualElement.
        /// </summary>
        /// <param name="element">The VisualElement on which to set the visibility.</param>
        /// <param name="visibility">The desired visibility state, specified as a Visibility enum.</param>
        /// <return>Returns the modified VisualElement.</return>
        public static T WithVisibility<T>(this T element, Visibility visibility) where T : VisualElement
        {
            element.style.visibility = visibility;
            return element;
        }

        /// <summary>
        /// Sets whether the specified VisualElement is enabled or disabled.
        /// </summary>
        /// <typeparam name="T">The type of the VisualElement.</typeparam>
        /// <param name="element">The VisualElement instance to modify.</param>
        /// <param name="enabled">A boolean indicating whether the element should be enabled (true) or disabled (false).</param>
        /// <return>Returns the modified VisualElement.</return>
        public static T WithEnabled<T>(this T element, bool enabled) where T : VisualElement
        {
            element.SetEnabled(enabled);
            return element;
        }

        /// <summary>
        /// Sets the name of the specified VisualElement and returns the element.
        /// </summary>
        /// <param name="element">The VisualElement to set the name for.</param>
        /// <param name="name">The name to assign to the VisualElement.</param>
        /// <return>Returns the modified VisualElement.</return>
        public static T WithName<T>(this T element, string name) where T : VisualElement
        {
            element.name = name;
            return element;
        }

        /// <summary>
        /// Adds the specified class name to the class list of the visual element.
        /// </summary>
        /// <typeparam name="T">The type of the visual element.</typeparam>
        /// <param name="element">The visual element to add the class to.</param>
        /// <param name="clss">The class name to add.</param>
        /// <return>Returns the modified VisualElement.</return>
        public static T WithClass<T>(this T element, string clss) where T : VisualElement
        {
            element.AddToClassList(clss);
            return element;
        }

        /// <summary>
        /// Adds a class and a name to the specified VisualElement.
        /// The same string value is used for both the class and name of the element.
        /// </summary>
        /// <param name="element">The target VisualElement to which the class and name will be applied.</param>
        /// <param name="classAndName">The string to be used as both the class name and the element's name.</param>
        /// <return>Returns the modified VisualElement.</return>
        public static T WithClassAndName<T>(this T element, string classAndName) where T : VisualElement
        {
            element.AddToClassList(classAndName);
            element.name = classAndName;
            return element;
        }

        /// <summary>
        /// Adds a callback to the button that is invoked when it is clicked.
        /// </summary>
        /// <param name="element">
        /// The button to which the click callback will be added.
        /// </param>
        /// <param name="callback">
        /// The action to be executed when the button is clicked.
        /// </param>
        /// <return>Returns the modified VisualElement.</return>
        /// Returns the button instance with the specified callback added.
        /// </return>
        public static T WithClickedCallback<T>(this T element, Action callback) where T : Button
        {
            element.clicked += callback;
            return element;
        }

        /// <summary>
        /// Sets the text of a Foldout element.
        /// </summary>
        /// <param name="element">The Foldout element to modify.</param>
        /// <param name="text">The text to assign to the Foldout element.</param>
        /// <return>Returns the modified VisualElement.</return>
        public static T WithFoldoutText<T>(this T element, string text) where T : Foldout
        {
            element.text = text;
            return element;
        }

        /// <summary>
        /// Sets the value of the foldout element and returns the foldout instance.
        /// </summary>
        /// <typeparam name="T">The type of the foldout element, which must inherit from Foldout.</typeparam>
        /// <param name="element">The foldout element to set the value for.</param>
        /// <param name="value">The boolean value to set for the foldout.</param>
        /// <return>Returns the modified VisualElement.</return>
        public static T WithFoldoutValue<T>(this T element, bool value) where T : Foldout
        {
            element.value = value;
            return element;
        }

        /// <summary>
        /// Sets the text content of the specified TextElement.
        /// </summary>
        /// <param name="element">The TextElement to modify.</param>
        /// <param name="text">The text string to assign to the element.</param>
        /// <return>Returns the modified VisualElement.</return>
        public static T WithText<T>(this T element, string text) where T : TextElement
        {
            element.text = text;
            return element;
        }

        /// <summary>
        /// Sets the value of the BaseField<TValue> without notifying any registered callbacks.
        /// </summary>
        /// <param name="element">The target BaseField on which the value will be set.</param>
        /// <param name="value">The value to set on the BaseField.</param>
        /// <typeparam name="T">The type of the VisualElement, which must be a BaseField of type TValue.</typeparam>
        /// <typeparam name="TValue">The type of the value to set on the BaseField.</typeparam>
        /// <return>Returns the modified VisualElement.</return>
        public static T WithValueWithoutNotify<T, TValue>(this T element, TValue value) where T : BaseField<TValue>
        {
            element.SetValueWithoutNotify(value);
            return element;
        }

        /// <summary>
        /// Sets the label of the specified <see cref="TextField" /> instance.
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="TextField" />.</typeparam>
        /// <param name="element">The <see cref="TextField" /> instance to modify.</param>
        /// <param name="label">The label text to set for the <see cref="TextField" />.</param>
        /// <return>Returns the modified VisualElement.</return>
        public static T WithLabel<T>(this T element, string label) where T : TextField
        {
            element.label = label;
            return element;
        }

        /// <summary>
        /// Sets the label of a BaseField element and returns the updated element.
        /// This method is useful for configuring a BaseField's label in a fluent API style.
        /// </summary>
        /// <typeparam name="T">The type of the BaseField being configured.</typeparam>
        /// <typeparam name="TValue">The type of the value in the BaseField.</typeparam>
        /// <param name="element">The BaseField instance being configured.</param>
        /// <param name="label">The text to set as the label of the BaseField.</param>
        /// <return>Returns the modified VisualElement.</return>
        public static T WithBaseFieldLabel<T, TValue>(this T element, string label) where T : BaseField<TValue>
        {
            element.label = label;
            return element;
        }

        /// <summary>
        /// Sets the picking mode for the specified VisualElement and returns the element for method chaining.
        /// </summary>
        /// <param name="element">The VisualElement to modify.</param>
        /// <param name="pickingMode">The PickingMode value to assign to the VisualElement.</param>
        /// <return>Returns the modified VisualElement.</return>
        public static T WithPickingMode<T>(this T element, PickingMode pickingMode) where T : VisualElement
        {
            element.pickingMode = pickingMode;
            return element;
        }
    }
}