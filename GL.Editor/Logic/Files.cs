namespace GL.Editor.Logic
{
    using System.Collections.Generic;
    using System.IO;

    using GL.Editor.Logic.Slots.Items;

    internal class Files : Dictionary<string, SCFile>
    {
        internal Dictionary<string, SCTexture> Textures;
        internal Dictionary<string, SCInfo> Infos;

        /// <summary>
        /// Initializes a new instance of the <see cref="Files"/> class.
        /// </summary>
        internal Files()
        {
            this.Textures   = new Dictionary<string, SCTexture>();
            this.Infos      = new Dictionary<string, SCInfo>();

            this.Load();
        }

        /// <summary>
        /// Loads the SC files.
        /// </summary>
        internal void Load()
        {
            foreach (string Path in Directory.GetFiles("Gamefiles", "*_tex.sc"))
            {
                FileInfo PSCTexture     = new FileInfo(Path);
                FileInfo PSCInfo        = new FileInfo(Path.Replace("_tex", string.Empty));
                
                if (PSCInfo.Exists && PSCTexture.Exists)
                {
                    SCFile SCFile = new SCFile(PSCInfo, PSCTexture);

                    if (SCFile.SCInfo != null && SCFile.SCTexture != null)
                    {
                        this.Textures.Add(SCFile.SCTextureI.Name, SCFile.SCTexture);
                        this.Infos.Add(SCFile.SCInfoI.Name, SCFile.SCInfo);
                        this.Add(SCFile.SCTextureI.Name, SCFile);
                    }
                }
            }
        }
    }
}