namespace GL.Servers.CoC.Files.CSV_Helpers
{
    using System.Collections.Generic;

    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CoC.Files.CSV_Logic.Client;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;

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
                    Data = new BuildingData(Row, this);
                    break;
                }

                case 2:
                {
                    Data = new LocaleData(Row, this);
                    break;
                }

                case 3:
                {
                    Data = new ResourceData(Row, this);
                    break;
                }

                case 4:
                {
                    Data = new CharacterData(Row, this);
                    break;
                }

                case 6:
                {
                    Data = new ProjectileData(Row, this);
                    break;
                }

                case 7:
                {
                    Data = new BuildingClassData(Row, this);
                    break;
                }

                case 8:
                {
                    Data = new ObstacleData(Row, this);
                    break;
                }

                case 9:
                {
                    Data = new EffectData(Row, this);
                    break;
                }

                case 10:
                {
                    Data = new ParticleEmitterData(Row, this);
                    break;
                }

                case 11:
                {
                    Data = new ExperienceLevelData(Row, this);
                    break;
                }

                case 12:
                {
                    Data = new TrapData(Row, this);
                    break;
                }

                case 13:
                {
                    Data = new AllianceBadgeData(Row, this);
                    break;
                }

                case 14:
                {
                    Data = new GlobalData(Row, this);
                    break;
                }

                case 15:
                {
                    Data = new TownhallLevelData(Row, this);
                    break;
                }

                case 16:
                {
                    Data = new AlliancePortalData(Row, this);
                    break;
                }

                case 17:
                {
                    Data = new NpcData(Row, this);
                    break;
                }

                case 18:
                {
                    Data = new DecoData(Row, this);
                    break;
                }

                case 19:
                {
                    Data = new ResourcePackData(Row, this);
                    break;
                }

                case 20:
                {
                    Data = new ShieldData(Row, this);
                    break;
                }

                case 21:
                {
                    Data = new MissionData(Row, this);
                    break;
                }

                case 22:
                {
                    Data = new BillingPackageData(Row, this);
                    break;
                }

                case 23:
                {
                    Data = new AchievementData(Row, this);
                    break;
                }

                case 25:
                {
                    Data = new FaqData(Row, this);
                    break;
                }

                case 26:
                {
                    Data = new SpellData(Row, this);
                    break;
                }

                case 27:
                {
                    Data = new HintData(Row, this);
                    break;
                }

                case 28:
                {
                    Data = new HeroData(Row, this);
                    break;
                }

                case 29:
                {
                    Data = new LeagueData(Row, this);
                    break;
                }

                case 30:
                {
                    Data = new NewData(Row, this);
                    break;
                }

                case 34:
                {
                    Data = new AllianceBadgeLayerData(Row, this);
                    break;
                }

                case 37:
                {
                    Data = new VariableData(Row, this);
                    break;
                }

                case 38:
                {
                    Data = new GemBundleData(Row, this);
                    break;
                }

                case 39:
                {
                    Data = new VillageObjectData(Row, this);
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
