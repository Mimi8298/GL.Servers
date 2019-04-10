namespace GL.Servers.Core.Consoles.Colorful
{
    using System;
    using System.Drawing;
    using System.Linq;

    /// <summary>
    /// Exposes methods and properties used for alternating over a set of colors according to
    /// the occurrences of patterns.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class PatternBasedColorAlternator<T> : ColorAlternator, IPrototypable<PatternBasedColorAlternator<T>>
    {
        private PatternCollection<T> patternMatcher;
        private bool isFirstRun = true;

        /// <summary>
        /// Exposes methods and properties used for alternating over a set of colors according to
        /// the occurrences of patterns.
        /// </summary>
        /// <param name="patternMatcher">The PatternMatcher instance which will dictate what will
        /// need to happen in order for the color to alternate.</param>
        /// <param name="colors">The set of colors over which to alternate.</param>
        public PatternBasedColorAlternator(PatternCollection<T> patternMatcher, params Color[] colors)
            : base(colors)
        {
            this.patternMatcher = patternMatcher;
        }

        public new PatternBasedColorAlternator<T> Prototype()
        {
            return new PatternBasedColorAlternator<T>(this.patternMatcher.Prototype(), this.Colors.DeepCopy().ToArray());
        }

        protected override ColorAlternator PrototypeCore()
        {
            return this.Prototype();
        }

        /// <summary>
        /// Alternates colors based on patterns matched in the input string.
        /// </summary>
        /// <param name="input">The string to be styled.</param>
        /// <returns>The current color of the ColorAlternator.</returns>
        public override Color GetNextColor(string input)
        {
            if (this.Colors.Length == 0)
            {
                throw new InvalidOperationException("No colors have been supplied over which to alternate!");
            }

            if (this.isFirstRun)
            {
                this.isFirstRun = false;
                return this.Colors[this.nextColorIndex];
            }

            if (this.patternMatcher.MatchFound(input))
            {
                this.TryIncrementColorIndex();
            }

            Color nextColor = this.Colors[this.nextColorIndex];

            return nextColor;
        }

        protected override void TryIncrementColorIndex()
        {
            if (this.nextColorIndex >= this.Colors.Length - 1)
            {
                this.nextColorIndex = 0;
            }
            else
            {
                this.nextColorIndex++;
            }
        }
    }
}
