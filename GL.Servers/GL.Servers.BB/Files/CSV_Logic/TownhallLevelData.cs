namespace GL.Servers.BB.Files.CSV_Logic
{
    using System.Collections.Generic;
    using GL.Servers.BB.Core;
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;

    internal class TownhallLevelData : Data
    {
        internal Dictionary<string, int> Datas;

        /// <summary>
        /// Initializes a new instance of the <see cref="TownhallLevelData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public TownhallLevelData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            this.Datas = new Dictionary<string, int>();
        }

        public string RequiredBuilding { get; set; }

        public int RequiredBuildingLevel { get; set; }

        public int RequiredTroopLevel { get; set; }

        public int BaseLayoutLevel { get; set; }

        public int GetUnlockedBuildingCount(string Name)
        {
            if (this.Datas.TryGetValue(Name, out int Count))
                return Count;

            Logging.Error(this.GetType(), "TownHallLevel::getUnlockedBuildingCount - Building key " + Name + " not exist.");

            return 0;
        }
    }
}