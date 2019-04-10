namespace GL.Editor.Logic.Slots
{
    using System.Collections.Generic;

    using GL.Editor.Logic.Slots.Items;

    internal class Chunks : List<Chunk>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Chunks"/> class.
        /// </summary>
        internal Chunks() : base(16)
        {
            // Chunks.
        }
    }
}