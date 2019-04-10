namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class EasterEggSpawnerData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="EasterEggSpawnerData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public EasterEggSpawnerData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int UnlockLevel
        {
            get; set;
        }

        public int SpawnTimeMinSeconds
        {
            get; set;
        }

        public int SpawnTimeMaxSeconds
        {
            get; set;
        }

        public int SpawnLocationFishingWaterX
        {
            get; set;
        }

        public int SpawnLocationFishingWaterY
        {
            get; set;
        }
    }
}
