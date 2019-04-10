namespace GL.Editor.Logic.Slots
{
    internal class Temporary
    {
        /// <summary>
        /// Gets or sets the shapes.
        /// </summary>
        internal Shapes Shapes;

        /// <summary>
        /// Gets or sets the chunks.
        /// </summary>
        internal Chunks Chunks;

        /// <summary>
        /// Gets or sets the spirites.
        /// </summary>
        internal Spirites Spirites;

        /// <summary>
        /// Gets or sets the fonts.
        /// </summary>
        internal Fonts Fonts;

        /// <summary>
        /// Initializes a new instance of the <see cref="Temporary"/> class.
        /// </summary>
        internal Temporary()
        {
            this.Shapes     = new Shapes();
            this.Chunks     = new Chunks();
            this.Spirites   = new Spirites();
            this.Fonts      = new Fonts();
        }
    }
}