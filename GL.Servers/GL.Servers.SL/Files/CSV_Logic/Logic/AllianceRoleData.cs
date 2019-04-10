namespace GL.Servers.SL.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SL.Files.CSV_Helpers;

    internal class AllianceRoleData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="AllianceRoleData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public AllianceRoleData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
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
