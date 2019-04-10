namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class PostmanData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="PostmanData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public PostmanData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public string TapSound
        {
            get; set;
        }

        public string WalkAnim
        {
            get; set;
        }

        public int WalkSpeed
        {
            get; set;
        }

        public int WalkGiftSpeed
        {
            get; set;
        }

        public string DeliverLettersAnim
        {
            get; set;
        }

        public int DeliverLettersAnimDurationMillis
        {
            get; set;
        }

        public string DeliverNothingAnim
        {
            get; set;
        }

        public int DeliverNothingAnimDurationMillis
        {
            get; set;
        }

        public string DeliverGiftAnim
        {
            get; set;
        }

        public int DeliverGiftAnimDurationMillis
        {
            get; set;
        }

        public string RidingAnim
        {
            get; set;
        }

        public int RideSpeed
        {
            get; set;
        }

        public int Capacity
        {
            get; set;
        }

        public int PreserveLetterCapacity
        {
            get; set;
        }

        public int GiftCardLimit
        {
            get; set;
        }

        public int GiftCardProbablity
        {
            get; set;
        }
    }
}
