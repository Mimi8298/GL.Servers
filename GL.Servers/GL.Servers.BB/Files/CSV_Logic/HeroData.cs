namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class HeroData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="HeroData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public HeroData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string AbilityPassive
        {
            get; set;
        }

        public string AbilityActive
        {
            get; set;
        }

        public int MenuOrder
        {
            get; set;
        }

        public string PortraitSWF
        {
            get; set;
        }

        public string PortraitExportName
        {
            get; set;
        }

        public int UnlockHeroBoatLevel
        {
            get; set;
        }

        public int UnlockRegionIndex
        {
            get; set;
        }

        public int UnlockNodeIndex
        {
            get; set;
        }

        public int MapFrame
        {
            get; set;
        }
    }
}
