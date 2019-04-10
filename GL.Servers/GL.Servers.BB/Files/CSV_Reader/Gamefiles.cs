namespace GL.Servers.BB.Files.CSV_Reader
{
    using System.Collections.Generic;

    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.BB.Files.Enums;

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
        internal Data GetDataById(int GlobalID)
        {
            int ClassId = GlobalID / 1000000 - 1;

            if (this.DataTables.TryGetValue(ClassId, out DataTable Table))
            {
                int InstanceId = GlobalID % 1000000;

                if (Table.Datas.Count > InstanceId)
                {
                    return Table.Datas[InstanceId];
                }
            }

            return null;
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