namespace GL.Servers.SL.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SL.Files.CSV_Helpers;

    internal class CollectableData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="CollectableData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public CollectableData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public string SWF
        {
            get; set;
        }

        public string ExportName
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

        public int TriggerRadius
        {
            get; set;
        }

        public string PickUpEffect
        {
            get; set;
        }

        public string AppearEffect
        {
            get; set;
        }

        public string BouncingStopsEffect
        {
            get; set;
        }

        public int ColorMulR
        {
            get; set;
        }

        public int ColorMulG
        {
            get; set;
        }

        public int ColorMulB
        {
            get; set;
        }

        public int ColorAddR
        {
            get; set;
        }

        public int ColorAddG
        {
            get; set;
        }

        public int ColorAddB
        {
            get; set;
        }

        public string RewardResource
        {
            get; set;
        }

        public int RewardAmount
        {
            get; set;
        }

        public int DisappearTurns
        {
            get; set;
        }
    }
}
