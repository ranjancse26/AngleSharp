﻿using AngleSharp.DOM.Collections;
using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the @font-face rule.
    /// </summary>
    [DOM("CSSFontFaceRule")]
    public sealed class CSSFontFaceRule : CSSRule
    {
        #region Constants

        internal const String RuleName = "font-face";

        #endregion

        #region Members

        CSSStyleDeclaration style;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new @font-face rule.
        /// </summary>
        internal CSSFontFaceRule()
        {
            style = new CSSStyleDeclaration();
            _type = CssRule.FontFace;
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Appends the given rule to the list of rules.
        /// </summary>
        /// <param name="rule">The rule to append.</param>
        /// <returns>The current font-face rule.</returns>
        internal CSSFontFaceRule AppendRule(CSSProperty rule)
        {
            style.List.Add(rule);
            return this;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the declared CSS rules.
        /// </summary>
        [DOM("cssRules")]
        public CSSStyleDeclaration CssRules
        {
            get { return style; }
        }

        /// <summary>
        /// Gets or sets the font-family.
        /// </summary>
        [DOM("family")]
        public String Family
        {
            get { return style.GetPropertyValue("font-family"); }
            set { style.SetProperty("font-family", value); }
        }

        /// <summary>
        /// Gets or sets the source of the font.
        /// </summary>
        [DOM("src")]
        public String Src
        {
            get { return style.GetPropertyValue("src"); }
            set { style.SetProperty("src", value); }
        }

        /// <summary>
        /// Gets or sets the style of the font.
        /// </summary>
        [DOM("style")]
        public String Style
        {
            get { return style.GetPropertyValue("font-style"); }
            set { style.SetProperty("font-style", value); }
        }

        /// <summary>
        /// Gets or sets the weight of the font.
        /// </summary>
        [DOM("weight")]
        public String Weight
        {
            get { return style.GetPropertyValue("font-weight"); }
            set { style.SetProperty("font-weight", value); }
        }

        /// <summary>
        /// Gets or sets the stretch value of the font.
        /// </summary>
        [DOM("stretch")]
        public String Stretch
        {
            get { return style.GetPropertyValue("stretch"); }
            set { style.SetProperty("stretch", value); }
        }

        /// <summary>
        /// Gets or sets the unicode range of the font.
        /// </summary>
        [DOM("unicodeRange")]
        public String UnicodeRange
        {
            get { return style.GetPropertyValue("unicode-range"); }
            set { style.SetProperty("unicode-range", value); }
        }

        /// <summary>
        /// Gets or sets the variant of the font.
        /// </summary>
        [DOM("variant")]
        public String Variant
        {
            get { return style.GetPropertyValue("font-variant"); }
            set { style.SetProperty("font-variant", value); }
        }

        /// <summary>
        /// Gets or sets the feature settings of the font.
        /// </summary>
        [DOM("featureSettings")]
        public String FeatureSettings
        {
            get { return style.GetPropertyValue("font-feature-settings"); }
            set { style.SetProperty("font-feature-settings", value); }
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return "@font-face {" + Environment.NewLine + style.ToCss() + "}";
        }

        #endregion
    }
}
