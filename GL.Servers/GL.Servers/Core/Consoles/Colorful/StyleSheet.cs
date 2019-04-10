namespace GL.Servers.Core.Consoles.Colorful
{
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// Exposes a collection of style classifications which can be used to style text.
    /// </summary>
    public sealed class StyleSheet
    {
        /// <summary>
        /// The StyleSheet's collection of style classifications.
        /// </summary>
        public List<StyleClass<TextPattern>> Styles { get; private set; }
        /// <summary>
        /// The color to be associated with unstyled text.
        /// </summary>
        public Color UnstyledColor;

        /// <summary>
        /// Exposes a collection of style classifications which can be used to style text.
        /// </summary>
        /// <param name="defaultColor">The color to be associated with unstyled text.</param>
        public StyleSheet(Color defaultColor)
        {
            this.Styles = new List<StyleClass<TextPattern>>();
            this.UnstyledColor = defaultColor;
        }

        /// <summary>
        /// Adds a style classification to the StyleSheet.
        /// </summary>
        /// <param name="target">The string to be styled.</param>
        /// <param name="color">The color to be applied to the target.</param>
        /// <param name="matchHandler">A delegate instance which describes a transformation that
        /// can be applied to the target.</param>
        public void AddStyle(string target, Color color, Styler.MatchFound matchHandler)
        {
            Styler styler = new Styler(target, color, matchHandler);

            this.Styles.Add(styler);
        }

        /// <summary>
        /// Adds a style classification to the StyleSheet.
        /// </summary>
        /// <param name="target">The string to be styled.</param>
        /// <param name="color">The color to be applied to the target.</param>
        /// <param name="matchHandler">A delegate instance which describes a simpler transformation that
        /// can be applied to the target.</param>
        public void AddStyle(string target, Color color, Styler.MatchFoundLite matchHandler)
        {
            Styler.MatchFound wrapper = (s, l, m) => matchHandler.Invoke(m);
            Styler styler = new Styler(target, color, wrapper);

            this.Styles.Add(styler);
        }

        /// <summary>
        /// Adds a style classification to the StyleSheet.
        /// </summary>
        /// <param name="target">The string to be styled.</param>
        /// <param name="color">The color to be applied to the target.</param>
        public void AddStyle(string target, Color color)
        {
            Styler.MatchFound handler = (s, l, m) => m;
            Styler styler = new Styler(target, color, handler);

            this.Styles.Add(styler);
        }
    }
}
