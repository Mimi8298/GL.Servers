namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class SpecialBoatRewardData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="SpecialBoatRewardData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public SpecialBoatRewardData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int FirstSpecialBoatDay
        {
            get; set;
        }

        public int FirstSpecialBoatHour
        {
            get; set;
        }

        public int MaxSpecialBoatsInWeek
        {
            get; set;
        }

        public int MinBoatsBetweenSpecialBoat
        {
            get; set;
        }

        public int MaxBoatsBetweenSpecialBoat
        {
            get; set;
        }

        public bool ForceSpecialBoatOnFirstOfMonth
        {
            get; set;
        }
    }
}
