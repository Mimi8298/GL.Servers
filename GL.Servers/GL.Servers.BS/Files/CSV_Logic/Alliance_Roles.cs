namespace GL.Servers.BS.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.BS.Files.CSV_Helpers;

    internal class Alliance_Roles : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="Alliance_Roles"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public Alliance_Roles(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Name
        {
            get; set;
        }

        public int Level
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public bool CanInvite
        {
            get; set;
        }

        public bool CanSendMail
        {
            get; set;
        }

        public bool CanChangeAllianceSettings
        {
            get; set;
        }

        public bool CanAcceptJoinRequest
        {
            get; set;
        }

        public bool CanKick
        {
            get; set;
        }

        public bool CanBePromotedToLeader
        {
            get; set;
        }

        public bool CanPromoteToOwnLevel
        {
            get; set;
        }
    }
}
