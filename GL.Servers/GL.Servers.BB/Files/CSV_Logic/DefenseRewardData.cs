namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class DefenseRewardData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="DefenseRewardData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public DefenseRewardData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int HousingSpaceDestroyed
        {
            get; set;
        }

        public int RewardGems
        {
            get; set;
        }

        public int SecondIntelDrop
        {
            get; set;
        }
    }
}
