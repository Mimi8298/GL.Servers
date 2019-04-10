namespace GL.Servers.CR.Files.Client
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class EventOutputData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="EventOutputData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public EventOutputData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // EventOutputData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal int Id
        {
            get; set;
        }

        internal int Channels
        {
            get; set;
        }

        internal int DurationMillis
        {
            get; set;
        }

    }
}