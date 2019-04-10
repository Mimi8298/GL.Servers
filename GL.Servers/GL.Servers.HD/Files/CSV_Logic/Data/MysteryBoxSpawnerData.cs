namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class MysteryBoxSpawnerData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="MysteryBoxSpawnerData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public MysteryBoxSpawnerData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int UnlockLevel
        {
            get; set;
        }

        public int MinSpawnTimeOpened
        {
            get; set;
        }

        public int MaxSpawnTimeOpened
        {
            get; set;
        }

        public int MinSpawnTimeClosed
        {
            get; set;
        }

        public int MaxSpawnTimeClosed
        {
            get; set;
        }

        public int MinSpawnTimeOpenedFriend
        {
            get; set;
        }

        public int MaxSpawnTimeOpenedFriend
        {
            get; set;
        }

        public int MinSpawnTimeClosedFriend
        {
            get; set;
        }

        public int MaxSpawnTimeClosedFriend
        {
            get; set;
        }

        public int FirstMysteryBoxAtGregTileIndex
        {
            get; set;
        }
    }
}
