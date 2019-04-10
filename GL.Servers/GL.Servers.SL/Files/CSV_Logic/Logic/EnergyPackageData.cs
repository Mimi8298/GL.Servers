namespace GL.Servers.SL.Files.CSV_Logic.Logic
{
    using System.Collections.Generic;
    using GL.Servers.Files.CSV_Reader;
	using GL.Servers.SL.Files.CSV_Helpers;

    internal class EnergyPackageData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="EnergyPackageData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public EnergyPackageData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public string InfoTID
        {
            get; set;
        }

        public List<int> IncreaseInMaxEnergy
        {
            get; set;
        }

        public List<int> Diamonds
        {
            get; set;
        }

        public string IconSWF
        {
            get; set;
        }

        public string IconExportName
        {
            get; set;
        }

        public bool HideWhenOnStartingEnergy
        {
            get; set;
        }
    }
}
