namespace GL.Servers.CR.Logic.Entries
{
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class AllianceHeaderEntry
    {
        internal int HighID;
        internal int LowID;
        internal int Type;
        internal int Donations;
        internal int NumberOfMembers;
        internal int RequiredScore;
        internal int Score;

        internal string Name;

        internal Data Region;

        /// <summary>
        /// Initializes a new instance of the <see cref="AllianceHeaderEntry"/> class.
        /// </summary>
        public AllianceHeaderEntry()
        {
            
        }
    }
}