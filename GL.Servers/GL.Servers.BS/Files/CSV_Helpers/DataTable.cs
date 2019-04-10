namespace GL.Servers.BS.Files.CSV_Helpers
{
    using System.Collections.Generic;

    using GL.Servers.BS.Files.CSV_Logic;
    using GL.Servers.Files.CSV_Reader;

    internal class DataTable
    {
        internal List<Data> Datas;
        internal int Index;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataTable"/> class.
        /// </summary>
        internal DataTable()
        {
            this.Datas = new List<Data>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataTable"/> class.
        /// </summary>
        /// <param name="Table">The table.</param>
        /// <param name="Index">The index.</param>
        public DataTable(Table Table, int Index)
        {
            this.Index  = Index;
            this.Datas  = new List<Data>();

            for (int i = 0; i < Table.GetRowCount(); i++)
            {
                Row Row    = Table.GetRowAt(i);
                Data Data  = this.Create(Row);

                this.Datas.Add(Data);
            }
        }

        /// <summary>
        /// Creates the data for the specified row.
        /// </summary>
        /// <param name="Row">The row.</param>
        internal Data Create(Row Row)
        {
            Data Data;

            switch (this.Index)
            {
                case 1:
                {
                    Data = new Globals(Row, this);
                    break;
                }

                case 5:
                {
                    Data = new Resources(Row, this);
                    break;
                }

                case 8:
                {
                    Data = new Alliance_Badges(Row, this);
                    break;
                }

                case 15:
                {
                    Data = new Locations(Row, this);
                    break;
                }

                case 16:
                {
                    Data = new Characters(Row, this);
                    break;
                }

                case 23:
                {
                    Data = new Cards(Row, this);
                    break;
                }

                case 28:
                {
                    Data = new Player_Thumbnails(Row, this);
                    break;
                }

                default:
                {
                    Data = new Data(Row, this);
                    break;
                }
            }

            return Data;
        }

        /// <summary>
        /// Gets the data with identifier.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        internal Data GetDataWithID(int ID)
        {
            return this.Datas[GlobalID.GetID(ID)];
        }

        /// <summary>
        /// Gets the data with instance identifier.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        internal Data GetDataWithInstanceID(int ID)
        {
            return this.Datas[ID];
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="Name">The name.</param>
        internal Data GetData(string Name)
        {
            return this.Datas.Find(Data => Data.Row.Name == Name);
        }
    }
}