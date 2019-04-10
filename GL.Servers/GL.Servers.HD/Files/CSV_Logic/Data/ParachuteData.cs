namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class ParachuteData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ParachuteData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ParachuteData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public string BurstExportName
        {
            get; set;
        }

        public string TapSound
        {
            get; set;
        }

        public int SpeedHorizontal
        {
            get; set;
        }

        public int SpeedVertical
        {
            get; set;
        }

        public int MinDistance
        {
            get; set;
        }

        public int MaxDistance
        {
            get; set;
        }

        public int MinAngle
        {
            get; set;
        }

        public int MaxAngle
        {
            get; set;
        }

        public int MaxScale
        {
            get; set;
        }

        public int BurstTime
        {
            get; set;
        }
    }
}
