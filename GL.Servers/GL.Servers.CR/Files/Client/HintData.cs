namespace GL.Servers.CR.Files.Client
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class HintData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="HintData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public HintData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // HintData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string TID
        {
            get; set;
        }

        internal bool NotBeenInClan
        {
            get; set;
        }

        internal bool NotBeenInTournament
        {
            get; set;
        }

        internal bool NotCreatedTournament
        {
            get; set;
        }

        internal int MinNpcWins
        {
            get; set;
        }

        internal int MaxNpcWins
        {
            get; set;
        }

        internal int MinArena
        {
            get; set;
        }

        internal int MaxArena
        {
            get; set;
        }

        internal int MinTrophies
        {
            get; set;
        }

        internal int MaxTrophies
        {
            get; set;
        }

        internal int MinExpLevel
        {
            get; set;
        }

        internal int MaxExpLevel
        {
            get; set;
        }

        internal string iOSTID
        {
            get; set;
        }

        internal string AndroidTID
        {
            get; set;
        }

    }
}