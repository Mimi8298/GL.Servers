namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class AchievementData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="AchievementData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public AchievementData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // AchievementData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal int Level
        {
            get; set;
        }

        internal string TID
        {
            get; set;
        }

        internal string InfoTID
        {
            get; set;
        }

        internal string Action
        {
            get; set;
        }

        internal int ActionCount
        {
            get; set;
        }

        internal int ExpReward
        {
            get; set;
        }

        internal int DiamondReward
        {
            get; set;
        }

        internal int SortIndex
        {
            get; set;
        }

        internal bool Hidden
        {
            get; set;
        }

        internal string AndroidID
        {
            get; set;
        }

        internal string Type
        {
            get; set;
        }

    }
}