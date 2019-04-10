namespace GL.Servers.CR.Files.Client
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class MusicData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="MusicData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public MusicData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // MusicData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string FileName
        {
            get; set;
        }

        internal int Volume
        {
            get; set;
        }

        internal bool Loop
        {
            get; set;
        }

        internal int PlayCount
        {
            get; set;
        }

        internal int FadeOutTimeSec
        {
            get; set;
        }

        internal int DurationSec
        {
            get; set;
        }

    }
}