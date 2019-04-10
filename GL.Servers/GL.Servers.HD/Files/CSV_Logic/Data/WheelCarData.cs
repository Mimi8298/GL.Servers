namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class WheelCarData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="WheelCarData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public WheelCarData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public int RequiredLevel
        {
            get; set;
        }

        public string StoppedExportName
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

        public string TapSound
        {
            get; set;
        }

        public int WheelVelocityMin
        {
            get; set;
        }

        public int WheelVelocityMax
        {
            get; set;
        }

        public int WheelFrictionMin
        {
            get; set;
        }

        public int WheelFrictionMax
        {
            get; set;
        }

        public int WheelMinAcceptedVelocity
        {
            get; set;
        }

        public int WheelNumSlots
        {
            get; set;
        }

        public int WheelFreeSpinSlot
        {
            get; set;
        }

        public int WheelJackpotSlot
        {
            get; set;
        }

        public int WheelJackpotAsDiamonds
        {
            get; set;
        }

        public int WheelSpinPriceMultiplier
        {
            get; set;
        }

        public int ExpensiveDiamondThreshold
        {
            get; set;
        }

        public int WheelNumExpensiveItems
        {
            get; set;
        }

        public int WheelNumDecoItems
        {
            get; set;
        }

        public int WheelNumVoucherItems
        {
            get; set;
        }

        public int WheelNumBoosterItems
        {
            get; set;
        }

        public int WheelMaxRandomVelocity
        {
            get; set;
        }

        public int WaitUntilLeaveSeconds
        {
            get; set;
        }

        public int MaxProbabilityAddFromOrder
        {
            get; set;
        }

        public int TierMax
        {
            get; set;
        }
    }
}
