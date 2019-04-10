namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class PersistentEventRewardData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="PersistentEventRewardData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public PersistentEventRewardData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public int BuildingsDestroyed
        {
            get; set;
        }

        public int WoodReward
        {
            get; set;
        }

        public int StoneReward
        {
            get; set;
        }

        public int MetalReward
        {
            get; set;
        }

        public int ProtopartReward
        {
            get; set;
        }
    }
}
