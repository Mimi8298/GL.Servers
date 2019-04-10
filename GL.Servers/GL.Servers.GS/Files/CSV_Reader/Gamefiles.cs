namespace GL.Servers.GS.Files.CSV_Reader
{
    using System.Collections.Generic;

    using GL.Servers.GS.Files.CSV_Helpers;
    using GL.Servers.GS.Logic.Enums;
    using GL.Servers.Files.CSV_Reader;

    internal class Gamefiles
    {
        internal readonly Dictionary<int, DataTable> DataTables;

        /// <summary>
        /// Initializes a new instance of the <see cref="Gamefiles"/> class.
        /// </summary>
        internal Gamefiles()
        {
            this.DataTables = new Dictionary<int, DataTable>(CSV.Gamefiles.Count);
        }

        /// <summary>
        /// Gets the <see cref="DataTable"/> at the specified index.
        /// </summary>
        /// <param name="Index">The index.</param>
        internal DataTable Get(int Index)
        {
            return this.DataTables[Index];
        }

        /// <summary>
        /// Gets the <see cref="DataTable"/> at the specified index.
        /// </summary>
        /// <param name="Index">The index.</param>
        internal DataTable Get(Gamefile Index)
        {
            return this.DataTables[(int) Index];
        }

        /// <summary>
        /// Gets the with global identifier.
        /// </summary>
        /// <param name="GlobalID">The global identifier.</param>
        /// <returns></returns>
        internal Data GetWithGlobalID(int GlobalID)
        {
            int Type = 0;

            while (GlobalID >= 1000000)
            {
                Type += 1;
                GlobalID -= 1000000;
            }

            return this.DataTables[Type].GetDataWithInstanceID(GlobalID);
        }

        /// <summary>
        /// Initializes the specified table.
        /// </summary>
        /// <param name="Table">The table.</param>
        /// <param name="Index">The index.</param>
        internal void Initialize(Table Table, int Index)
        {
            this.DataTables.Add(Index, new DataTable(Table, Index));
        }
    }
}