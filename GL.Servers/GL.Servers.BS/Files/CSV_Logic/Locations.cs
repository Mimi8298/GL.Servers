namespace GL.Servers.BS.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.BS.Files.CSV_Helpers;

    internal class Locations : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="Locations"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public Locations(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Name
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string TileSetPrefix
        {
            get; set;
        }

        public int TileSetRedAdd
        {
            get; set;
        }

        public int TileSetGreenAdd
        {
            get; set;
        }

        public int TileSetBlueAdd
        {
            get; set;
        }

        public int TileSetRedMul
        {
            get; set;
        }

        public int TileSetGreenMul
        {
            get; set;
        }

        public int TileSetBlueMul
        {
            get; set;
        }

        public string IconSWF
        {
            get; set;
        }

        public string IconExportName
        {
            get; set;
        }

        public string GameMode
        {
            get; set;
        }

        public string AllowedMaps
        {
            get; set;
        }

        public int ShadowR
        {
            get; set;
        }

        public int ShadowG
        {
            get; set;
        }

        public int ShadowB
        {
            get; set;
        }

        public int ShadowA
        {
            get; set;
        }

        public string Music
        {
            get; set;
        }
    }
}
