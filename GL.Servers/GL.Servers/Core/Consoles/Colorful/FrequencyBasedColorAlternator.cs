namespace GL.Servers.Core.Consoles.Colorful
{
    using System;
    using System.Drawing;
    using System.Linq;

    /// <summary>
    /// Exposes methods and properties used for alternating over a set of colors according to
    /// frequency of use.
    /// </summary>
    public sealed class FrequencyBasedColorAlternator : ColorAlternator, IPrototypable<FrequencyBasedColorAlternator>
    {
        private int alternationFrequency;
        private int writeCount = 0;

        /// <summary>
        /// Exposes methods and properties used for alternating over a set of colors according to
        /// frequency of use.
        /// </summary>
        /// <param name="alternationFrequency">The number of times GetNextColor must be called in order for
        /// the color to alternate.</param>
        /// <param name="colors">The set of colors over which to alternate.</param>
        public FrequencyBasedColorAlternator(int alternationFrequency, params Color[] colors)
            : base(colors)
        {
            this.alternationFrequency = alternationFrequency;
        }

        public new FrequencyBasedColorAlternator Prototype()
        {
            return new FrequencyBasedColorAlternator(this.alternationFrequency, this.Colors.DeepCopy().ToArray());
        }

        protected override ColorAlternator PrototypeCore()
        {
            return this.Prototype();
        }

        /// <summary>
        /// Alternates colors based on the number of times GetNextColor has been called.
        /// </summary>
        /// <param name="input">The string to be styled.</param>
        /// <returns>The current color of the ColorAlternator.</returns>
        public override Color GetNextColor(string input)
        {
            if (this.Colors.Length == 0)
            {
                throw new InvalidOperationException("No colors have been supplied over which to alternate!");
            }

            Color nextColor = this.Colors[this.nextColorIndex];
            this.TryIncrementColorIndex();

            return nextColor;
        }

        protected override void TryIncrementColorIndex()
        {
            if (this.writeCount >= (this.Colors.Length * this.alternationFrequency) - 1)
            {
                this.nextColorIndex = 0;
                this.writeCount = 0;
            }
            else
            {
                this.writeCount++;
                this.nextColorIndex = (int)Math.Floor(this.writeCount / (double)this.alternationFrequency);
            }
        }
    }
}
