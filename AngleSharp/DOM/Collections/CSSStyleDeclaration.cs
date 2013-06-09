﻿using AngleSharp.Css;
using AngleSharp.DOM.Css;
using System;
using System.Collections.Generic;
using System.Collections;

namespace AngleSharp.DOM.Collections
{
    /// <summary>
    /// Represents a single CSS declaration block.
    /// </summary>
    public sealed class CSSStyleDeclaration : DOMCollection, IEnumerable<CSSProperty>
    {
        #region Members

        List<CSSProperty> _rules;
        CSSRule _parent;

        string oldCssText;
        Func<string> getter;
        Action<string> setter;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS style declaration.
        /// </summary>
        public CSSStyleDeclaration()
        {
            _rules = new List<CSSProperty>();
        }

        /// <summary>
        /// Creates a new CSS style declaration using the given parser list.
        /// </summary>
        /// <param name="decl">The initial declaration.</param>
        internal CSSStyleDeclaration(List<CssNamedRule> decl)
        {
            // TODO
        }

        /// <summary>
        /// Creates a bound CSSStyleDeclaration from the given properties.
        /// </summary>
        /// <param name="getter">The access to the getter property part.</param>
        /// <param name="setter">The access to the setter property part.</param>
        /// <returns>The CSSStyleDeclaration.</returns>
        internal static CSSStyleDeclaration From(Func<string> getter, Action<string> setter)
        {
            var styles = new CSSStyleDeclaration();
            styles.getter = getter;
            styles.setter = setter;
            return styles;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the textual representation of the declaration block.
        /// </summary>
        public string CssText
        {
            get { GetValue(); return oldCssText; }
            set { Reset(value); }
        }

        /// <summary>
        /// Gets the number of properties in the declaration.
        /// </summary>
        public int Length
        {
            get { return _rules.Count; }
        }

        /// <summary>
        /// Gets the containing CSSRule.
        /// </summary>
        public CSSRule ParentRule
        {
            get { return _parent; }
            internal set { _parent = value; }
        }

        /// <summary>
        /// Returns a property name.
        /// </summary>
        /// <param name="index">The index of the property to retrieve.</param>
        /// <returns>The name of the property at the given index.</returns>
        public string this[int index]
        {
            get
            {
                GetValue();
                return index >= 0 && index < Length ? _rules[index].Name : null;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the value deleted.
        /// </summary>
        /// <param name="propertyName">The name of the property to be removed.</param>
        /// <returns>The value of the deleted property.</returns>
        public string RemoveProperty(string propertyName)
        {
            GetValue();

            for (int i = 0; i < _rules.Count; i++)
            {
                if (_rules[i].Name.Equals(propertyName))
                {
                    var value = _rules[i].Value;
                    _rules.RemoveAt(i);
                    return value.CssText;
                }
            }

            SetValue();
            return null;
        }

        /// <summary>
        /// Returns the optional priority, "important".
        /// </summary>
        /// <param name="propertyName">The name of the property to get the priority of.</param>
        /// <returns>A priority or null.</returns>
        public string GetPropertyPriority(string propertyName)
        {
            GetValue();

            for (int i = 0; i < _rules.Count; i++)
            {
                if (_rules[i].Name.Equals(propertyName))
                    return _rules[i].Important ? "important" : null;
            }

            return null;
        }

        /// <summary>
        /// Returns the value of a property.
        /// </summary>
        /// <param name="propertyName">The name of the property to get the priority of.</param>
        /// <returns>A value or null if nothing has been set.</returns>
        public string GetPropertyValue(string propertyName)
        {
            GetValue();

            for (int i = 0; i < _rules.Count; i++)
            {
                if (_rules[i].Name.Equals(propertyName))
                    return _rules[i].Value.CssText;
            }

            return null;
        }

        /// <summary>
        /// Sets a property with the given name and value.
        /// </summary>
        /// <param name="propertyName">The property's name.</param>
        /// <param name="propertyValue">The value of the property.</param>
        /// <returns>The current style declaration.</returns>
        public CSSStyleDeclaration SetProperty(string propertyName, string propertyValue)
        {
            GetValue();
            //TODO
            SetValue();
            return this;
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Appends a property to the list of rules.
        /// </summary>
        /// <param name="rule">The rule to append.</param>
        /// <returns>The current declaration list.</returns>
        internal CSSStyleDeclaration AppendProperty(CSSProperty rule)
        {
            _rules.Add(rule);
            return this;
        }

        #endregion

        #region Helpers

        void Reset(string value)
        {
            _rules.Clear();
            _rules.AddRange(CssParser.ParseDeclarations(value)._rules);
            SetValue();
        }

        void GetValue()
        {
            if (getter != null)
            {
                var newCssText = getter();

                if (newCssText != oldCssText)
                    Reset(newCssText);
            }
        }

        void SetValue()
        {
            oldCssText = ToCss();

            if (setter != null)
                setter(oldCssText);
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns the CSS representation of the list of rules.
        /// </summary>
        /// <returns></returns>
        public string ToCss()
        {
            //TODO
            return oldCssText;
        }

        /// <summary>
        /// Returns an HTML-code representation of the style declaration (which is just the CSS snippet).
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public override string ToHtml()
        {
            return ToCss();
        }

        #endregion

        #region Interface implementation

        /// <summary>
        /// Returns an ienumerator that enumerates over all entries.
        /// </summary>
        /// <returns>The iterator.</returns>
        public IEnumerator<CSSProperty> GetEnumerator()
        {
            return _rules.GetEnumerator();
        }

        /// <summary>
        /// Returns a common ienumerator to enumerate all entries.
        /// </summary>
        /// <returns>The iterator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_rules).GetEnumerator();
        }

        #endregion
    }
}