namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class ExpansionFenceData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ExpansionFenceData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ExpansionFenceData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string FileName
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string FenceBelowExportName
        {
            get; set;
        }

        public string FenceRightExportName
        {
            get; set;
        }

        public string FenceCornerExportName
        {
            get; set;
        }

        public string SpecialExportName
        {
            get; set;
        }

        public string SpecialBelowExportName
        {
            get; set;
        }

        public string SpecialRightExportName
        {
            get; set;
        }

        public string SpecialCornerExportName
        {
            get; set;
        }

        public int TileWidth
        {
            get; set;
        }

        public int TileHeight
        {
            get; set;
        }

        public bool TagSpecial
        {
            get; set;
        }

        public bool TagNature
        {
            get; set;
        }

        public bool TagDeco
        {
            get; set;
        }

        public bool TagSanctuary
        {
            get; set;
        }

        public bool HideFromLevelUpScreen
        {
            get; set;
        }

        public string SanctuaryAnimal
        {
            get; set;
        }
    }
}
