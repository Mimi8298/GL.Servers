namespace GL.Servers.BS.Logic.Slots.Items
{
    internal class Image
    {
        internal string Path;
        internal string Checksum;
        internal string File;

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        /// <param name="Path">The path.</param>
        /// <param name="Checksum">The checksum.</param>
        /// <param name="File">The file.</param>
        internal Image(string Path, string Checksum, string File)
        {
            this.Path     = Path;
            this.Checksum = Checksum;
            this.File     = File; 
        }
    }
}