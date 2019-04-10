namespace GL.Servers.CR.Files.CSV_Reader
{
    using System.Collections.Generic;

    using GL.Servers.CR.Files.CSV_Helpers;
    using GL.Servers.CR.Files.Logic;
    using GL.Servers.CR.Logic.Enums;

    using GL.Servers.Files.CSV_Reader;

    internal class Gamefiles
    {
        internal readonly Dictionary<int, DataTable> DataTables;

        internal int MaxExpLevel;

        internal ResourceData GoldData;
        internal ResourceData FreeGoldData;
        internal ResourceData CardCountData;
        internal ResourceData ChestCountData;
        internal RarityData RarityCommonData;

        internal List<SpellData> Spells;

        /// <summary>
        /// Initializes a new instance of the <see cref="Gamefiles"/> class.
        /// </summary>
        internal Gamefiles()
        {
            this.Spells     = new List<SpellData>(72);
            this.DataTables = new Dictionary<int, DataTable>(CSV.Paths.Count);
        }

        /// <summary>
        /// Gets the spell data by name.
        /// </summary>
        internal SpellData GetSpellDataByName(string Name)
        {
            return this.Spells.Find(T => T.Name == Name);
        }

        /// <summary>
        /// Gets the <see cref="DataTable"/> at the specified index.
        /// </summary>
        /// <param name="Index">The index.</param>
        internal DataTable Get(Gamefile Index)
        {
            return this.Get((int)Index);
        }

        /// <summary>
        /// Gets the <see cref="DataTable"/> at the specified index.
        /// </summary>
        /// <param name="Index">The index.</param>
        internal DataTable Get(int Index)
        {
            if (this.DataTables.ContainsKey(Index))
            {
                return this.DataTables[Index];
            }

            return null;
        }

        /// <summary>
        /// Gets the with global identifier.
        /// </summary>
        /// <param name="GlobalID">The global identifier.</param>
        internal Data GetWithGlobalID(int GlobalID)
        {
            if (this.DataTables.TryGetValue(GlobalID / 1000000, out DataTable Table))
            {
                return Table.GetWithGlobalID(GlobalID);
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