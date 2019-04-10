namespace GL.Servers.Core.Consoles.Colorful
{
    using System.Drawing;

    /// <summary>
    /// Exposes methods and properties used for alternating over a set of colors.
    /// </summary>
    public abstract class ColorAlternator : IPrototypable<ColorAlternator>
    {
        /// <summary>
        /// The set of colors over which to alternate.
        /// </summary>
        public Color[] Colors { get; set; }

        protected int nextColorIndex = 0;

        /// <summary>
        /// Exposes methods and properties used for alternating over a set of colors.
        /// </summary>
        public ColorAlternator()
        {
            this.Colors = new Color[]{};
        }

        /// <summary>
        /// Exposes methods and properties used for alternating over a set of colors.
        /// </summary>
        public ColorAlternator(params Color[] colors)
        {
            this.Colors = colors;
        }

        public ColorAlternator Prototype()
        {
            return this.PrototypeCore();
        }

        protected abstract ColorAlternator PrototypeCore();

        /// <summary>
        /// Alternates colors based on the state of the ColorAlternator instance.
        /// </summary>
        /// <param name="input">The string to be styled.</param>
        /// <returns>The current color of the ColorAlternator.</returns>
        public abstract Color GetNextColor(string input);

        protected abstract void TryIncrementColorIndex();
    }
}
