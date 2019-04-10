namespace GL.Tools.Finder.Logic
{
    internal interface Supercell
    {
        /// <summary>
        /// Gets or sets the key offset.
        /// </summary>
        long Offset
        {
            get;
            set;
        }

        /// <summary>
        /// Processes the specified file.
        /// </summary>
        /// <param name="File">The file.</param>
        void Process(byte[] File);

        /// <summary>
        /// Patches the specified file.
        /// </summary>
        /// <param name="File">The file.</param>
        void Patch(byte[] File);
    }
}