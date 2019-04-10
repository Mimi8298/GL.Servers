namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class AllianceRoleData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="AllianceRoleData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public AllianceRoleData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // AllianceRoleData.
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

        internal bool CanInvite
        {
            get; set;
        }

        internal bool CanSendMail
        {
            get; set;
        }

        internal bool CanChangeAllianceSettings
        {
            get; set;
        }

        internal bool CanAcceptJoinRequest
        {
            get; set;
        }

        internal bool CanKick
        {
            get; set;
        }

        internal bool CanBePromotedToLeader
        {
            get; set;
        }

        internal bool CanPromoteToOwnLevel
        {
            get; set;
        }

    }
}