namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class GiftData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="GiftData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public GiftData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Type
        {
            get; set;
        }

        public string FileName
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string GiftIconExportName
        {
            get; set;
        }

        public string GoodRewards
        {
            get; set;
        }

        public string GoodIconSlotExportName
        {
            get; set;
        }

        public int GoodAmounts
        {
            get; set;
        }

        public int Probabilities
        {
            get; set;
        }

        public int GiftCardPrice
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

        public string OpenSound
        {
            get; set;
        }

        public string TitleTID
        {
            get; set;
        }

        public string TextTID
        {
            get; set;
        }

        public string Text2TID
        {
            get; set;
        }

        public string Text3TID
        {
            get; set;
        }

        public string OpenGiftMessageTID
        {
            get; set;
        }

        public int UnlockLevel
        {
            get; set;
        }

        public int RepUnlockLevel
        {
            get; set;
        }

        public string WalkGiftAnim
        {
            get; set;
        }

        public string RewardHUDExportName
        {
            get; set;
        }
    }
}
