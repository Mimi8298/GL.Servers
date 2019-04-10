namespace GL.Servers.Core.Consoles.Colorful
{
    using System.Drawing;

    /// <summary>
    /// Exposes properties representing an object and its color.  This is a convenience wrapper around
    /// the StyleClass type, so you don't have to provide the type argument each time.
    /// </summary>
    public sealed class Formatter
    {
        /// <summary>
        /// The object to be styled.
        /// </summary>
        public object Target
        {
            get
            {
                return this.backingClass.Target;
            }
        }
        /// <summary>
        /// The color to be applied to the target.
        /// </summary>
        public Color Color
        {
            get
            {
                return this.backingClass.Color;
            }
        }

        private StyleClass<object> backingClass;

        /// <summary>
        /// Exposes properties representing an object and its color.  This is a convenience wrapper around
        /// the StyleClass type, so you don't have to provide the type argument each time.
        /// </summary>
        /// <param name="target">The object to be styled.</param>
        /// <param name="color">The color to be applied to the target.</param>
        public Formatter(object target, Color color)
        {
            this.backingClass = new StyleClass<object>(target, color);
        }
    }
}
