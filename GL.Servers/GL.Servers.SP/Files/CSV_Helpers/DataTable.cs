namespace GL.Servers.SP.Files.CSV_Helpers
{
    using System.Collections.Generic;

    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.SP.Files.CSV_Logic;

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
                    Data = new GlobalData(Row, this);
                    break;
                }
                case 3:
                {
                    Data = new BillingPackageData(Row, this);
                    break;
                }
                case 4:
                {
                    Data = new AchievementData(Row, this);
                    break;
                }
                case 5:
                {
                    Data = new Data(Row, this);
                    break;
                }
                case 6:
                {
                    Data = new SoundData(Row, this);
                    break;
                }
                case 7:
                {
                    Data = new EffectData(Row, this);
                    break;
                }
                case 8:
                {
                    Data = new ParticleEmitterData(Row, this);
                    break;
                }
                case 9:
                {
                    Data = new ResourceData(Row, this);
                    break;
                }
                case 10:
                {
                    Data = new PuzzleData(Row, this);
                    break;
                }
                case 11:
                {
                    Data = new BlockData(Row, this);
                    break;
                }
                case 12:
                {
                    Data = new MonsterData(Row, this);
                    break;
                }
                case 14:
                {
                    Data = new LevelData(Row, this);
                    break;
                }
                case 15:
                {
                    Data = new TutorialData(Row, this);
                    break;
                }
                case 16:
                {
                    Data = new HeroData(Row, this);
                    break;
                }
                case 17:
                {
                    Data = new BoostData(Row, this);
                    break;
                }
                case 18:
                {
                    Data = new AttackTypeData(Row, this);
                    break;
                }
                case 19:
                {
                    Data = new SpecialMoveData(Row, this);
                    break;
                }
                case 20:
                {
                    Data = new PlaylistData(Row, this);
                    break;
                }
                case 21:
                {
                    Data = new ProjectileData(Row, this);
                    break;
                }
                case 22:
                {
                    Data = new MatchPatternData(Row, this);
                    break;
                }
                case 23:
                {
                    Data = new FacebookErrorData(Row, this);
                    break;
                }
                case 24:
                {
                    Data = new GateData(Row, this);
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
