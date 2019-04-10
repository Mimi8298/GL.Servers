namespace GL.Servers.CR.Files.CSV_Helpers
{
    using System.Collections.Generic;

    using GL.Servers.CR.Core;
    using GL.Servers.CR.Files.Client;
    using GL.Servers.CR.Files.Logic;
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

                if (Data != null)
                {
                    this.Datas.Add(Data);
                }
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
                    Data = new BillingPackageData(Row, this);
                    break;
                }

                case 3:
                case 20:
                {
                    Data = new GlobalData(Row, this);
                    break;
                }

                case 4:
                {
                    Data = new SoundData(Row, this);
                    break;
                }

                case 5:
                {
                    Data = new ResourceData(Row, this);
                    break;
                }

                case 9:
                {
                    Data = new CharacterBuffData(Row, this);
                    break;
                }

                case 10:
                {
                    Data = new ProjectileData(Row, this);
                    break;
                }

                case 11:
                {
                    Data = new EffectData(Row, this);
                    break;
                }

                case 12:
                {
                    Data = new PredefinedDeckData(Row, this);
                    break;
                }
                    
                case 14:
                {
                    Data = new RarityData(Row, this);
                    break;
                }

                case 15:
                {
                    Data = new LocationData(Row, this);
                    break;
                }

                case 16:
                {
                    Data = new AllianceBadgeData(Row, this);
                    break;
                }
                    
                case 18:
                {
                    Data = new NpcData(Row, this);
                    break;
                }

                case 19:
                {
                    Data = new TreasureChestData(Row, this);
                    break;
                }

                case 21:
                {
                    Data = new ParticleEmitterData(Row, this);
                    break;
                }

                case 22:
                {
                    Data = new AreaEffectObjectData(Row, this);
                    break;
                }

                case 26:
                case 27:
                case 28:
                case 29:
                {
                    Data = new SpellData(Row, this);
                    break;
                }

                case 34:
                case 35:
                {
                    Data = new CharacterData(Row, this);
                    break;
                }

                case 40:
                {
                    Data = new HealthBarData(Row, this);
                    break;
                }

                case 41:
                {
                    Data = new MusicData(Row, this);
                    break;
                }

                case 42:
                {
                    Data = new DecoData(Row, this);
                    break;
                }

                case 43:
                {
                    Data = new GambleChestData(Row, this);
                    break;
                }

                case 45:
                case 48:
                {
                    Data = new TutorialData(Row, this);
                    break;
                }

                case 46:
                {
                    Data = new ExpLevelData(Row, this);
                    break;
                }
                    
                case 50:
                {
                    Data = new BackgroundDecoData(Row, this);
                    break;
                }

                case 51:
                {
                    Data = new SpellSetData(Row, this);
                    break;
                }

                case 52:
                {
                    Data = new ChestOrderData(Row, this);
                    break;
                }

                case 53:
                {
                    Data = new TauntData(Row, this);
                    break;
                }

                case 54:
                {
                    Data = new ArenaData(Row, this);
                    break;
                }

                case 55:
                {
                    Data = new ResourcePackData(Row, this);
                    break;
                }

                case 56:
                {
                    Data = new Data(Row, this);
                    break;
                }

                case 57:
                {
                    Data = new RegionData(Row, this);
                    break;
                }

                case 58:
                {
                    Data = new NewsData(Row, this);
                    break;
                }

                case 59:
                {
                    Data = new AllianceRoleData(Row, this);
                    break;
                }

                case 60:
                {
                    Data = new AchievementData(Row, this);
                    break;
                }

                case 61:
                {
                    Data = new HintData(Row, this);
                    break;
                }

                case 62:
                {
                    Data = new HelpshiftData(Row, this);
                    break;
                }

                default:
                {
                    Logging.Info(this.GetType(), "Invalid data table id: " + this.Index + ".");
                    Data = null;
                    break;
                }
            }

            return Data;
        }

        /// <summary>
        /// Gets the data with identifier.
        /// </summary>
        /// <param name="Identifier">The identifier.</param>
        /// <returns></returns>
        internal Data GetWithInstanceID(int Identifier)
        {
            if (this.Datas.Count > Identifier)
                return this.Datas[Identifier];
            return null;
        }

        /// <summary>
        /// Gets the data with identifier.
        /// </summary>
        /// <param name="Identifier">The identifier.</param>
        /// <returns></returns>
        internal T GetWithInstanceID<T>(int Identifier) where T : Data
        {
            if (this.Datas.Count > Identifier)
                return this.Datas[Identifier] as T;
            return null;
        }

        /// <summary>
        /// Gets the data with identifier.
        /// </summary>
        /// <param name="Identifier">The identifier.</param>
        /// <returns></returns>
        internal Data GetWithGlobalID(int GlobalID)
        {
            return this.GetWithInstanceID(GlobalID % 1000000);
        }

        /// <summary>
        /// Gets the data with identifier.
        /// </summary>
        /// <param name="Identifier">The identifier.</param>
        /// <returns></returns>
        internal T GetWithGlobalID<T>(int GlobalID) where T : Data
        {
            return this.GetWithInstanceID(GlobalID % 1000000) as T;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="Name">The name.</param>
        internal Data GetData(string Name)
        {
            return this.Datas.Find(Data => Data.Name == Name);
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="Name">The name.</param>
        internal T GetData<T>(string Name) where T : Data
        {
            return this.Datas.Find(Data => Data.Name == Name) as T;
        }
    }
}