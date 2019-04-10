namespace GL.Servers.SP.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SP.Files.CSV_Helpers;

    internal class PlaylistData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="PlaylistData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public PlaylistData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Music
        {
            get; set;
        }

        public bool Loop
        {
            get; set;
        }

        public bool Shuffle
        {
            get; set;
        }
    }
}
