namespace GL.Servers.SL.Files.CSV_Helpers
{
    using System.Collections.Generic;

    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.SL.Files.CSV_Logic.Logic;

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
                    Data = new LocaleData(Row, this);
                    break;
                }
                case 2:
                {
                    Data = new ResourceData(Row, this);
                    break;
                }
                case 3:
                {
                    Data = new EffectData(Row, this);
                    break;
                }
                case 4:
                {
                    Data = new ParticleEmitterData(Row, this);
                    break;
                }
                case 5:
                {
                    Data = new GlobalData(Row, this);
                    break;
                }
                case 6:
                {
                    Data = new QuestData(Row, this);
                    break;
                }
                case 10:
                {
                    Data = new WorldData(Row, this);
                    break;
                }
                case 11:
                {
                    Data = new HeroData(Row, this);
                    break;
                }

                case 24:
                {
                    Data = new TauntData(Row, this);
                    break;
                }

                case 25:
                {
                    Data = new DecoData(Row, this);
                    break;
                }

                case 26:
                {
                    Data = new VariableData(Row, this);
                    break;
                }

                case 32:
                {
                    Data = new EnergyPackageData(Row, this);
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