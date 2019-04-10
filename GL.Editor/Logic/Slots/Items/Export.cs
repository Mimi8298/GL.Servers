namespace GL.Editor.Logic.Slots.Items
{
    internal class Export
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        internal string Name;

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        internal uint Identifier;

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        internal int SubCount;

        /// <summary>
        /// Gets or sets the frame count.
        /// </summary>
        internal uint FrameCount;

        /// <summary>
        /// Gets or sets the FPS.
        /// </summary>
        internal uint FPS;

        /// <summary>
        /// Gets or sets the shapes.
        /// </summary>
        internal Shapes Shapes;

        /// <summary>
        /// Gets or sets the spirites.
        /// </summary>
        internal Spirites Spirites;

        /// <summary>
        /// Initializes a new instance of the <see cref="Export"/> class.
        /// </summary>
        internal Export(uint Identifier = 0, string Value = "")
        {
            this.Identifier = Identifier;
            this.Name       = Value;

            this.Shapes     = new Shapes();
            this.Spirites   = new Spirites();
        }
    }
}