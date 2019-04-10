namespace GL.Editor.Logic.Slots.Items
{
    using System.IO;

    internal class SCFile
    {
        internal    FileInfo   SCTextureI;
        internal    SCTexture  SCTexture;

        internal    FileInfo    SCInfoI;
        internal    SCInfo      SCInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SCFile"/> class.
        /// </summary>
        internal SCFile()
        {
            // File.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SCFile"/> class.
        /// </summary>
        /// <param name="SCInfo">The sc information.</param>
        /// <param name="SCTexture">The sc texture.</param>
        internal SCFile(FileInfo SCInfo, FileInfo SCTexture)
        {
            this.SCInfoI    = SCInfo;
            this.SCTextureI = SCTexture;

            this.SCInfo     = new SCInfo(this);
            this.SCTexture  = new SCTexture(this);

            this.SCTexture.Load();
            this.SCInfo.Load();
        }
    }
}