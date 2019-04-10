namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class BoosterData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="BoosterData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public BoosterData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public string ToolIconExportName
        {
            get; set;
        }

        public string BoosterType
        {
            get; set;
        }

        public string BoosterTarget
        {
            get; set;
        }

        public int BoosterPercentage
        {
            get; set;
        }

        public int BoosterDurationSeconds
        {
            get; set;
        }

        public int UnlockLevel
        {
            get; set;
        }

        public int BoosterStars
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string BoosterValueTID
        {
            get; set;
        }

        public string DescriptionTID
        {
            get; set;
        }

        public string BoosterEffectExportName
        {
            get; set;
        }

        public string BoosterTapEffectExportName
        {
            get; set;
        }

        public bool SpinWheelPrize
        {
            get; set;
        }

        public bool FreeBooster
        {
            get; set;
        }
    }
}
