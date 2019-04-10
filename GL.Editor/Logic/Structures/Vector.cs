namespace GL.Editor.Logic.Structures
{
    internal struct Vector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector"/> struct.
        /// </summary>
        /// <param name="X">The x.</param>
        /// <param name="Y">The y.</param>
        internal Vector(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        internal int X;

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        internal int Y;
    }
}