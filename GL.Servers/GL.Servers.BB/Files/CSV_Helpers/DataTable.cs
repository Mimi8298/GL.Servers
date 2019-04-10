namespace GL.Servers.BB.Files.CSV_Helpers
{
    using System.Collections.Generic;
    using GL.Servers.BB.Files.CSV_Logic;
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
                case 0:
                {
                    Data = new BuildingData(Row, this);
                    break;
                }

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
                    Data = new CharacterData(Row, this);
                    break;
                }

                case 6:
                {
                    Data = new BuildingClassData(Row, this);
                    break;
                }

                case 7:
                {
                    Data = new ObstacleData(Row, this);
                    break;
                }

                case 8:
                {
                    Data = new EffectData(Row, this);
                    break;
                }

                case 9:
                {
                    Data = new ParticleEmitterData(Row, this);
                    break;
                }

                case 10:
                {
                    Data = new ExperienceLevelData(Row, this);
                    break;
                }

                case 11:
                {
                    Data = new TrapData(Row, this);
                    break;
                }

                case 12:
                {
                    Data = new AllianceBadgeData(Row, this);
                    break;
                }

                case 13:
                {
                    Data = new GlobalData(Row, this);
                    break;
                }

                case 14:
                {
                    Data = new TownhallLevelData(Row, this);
                    break;
                }

                case 15:
                {
                    Data = new PrototypeData(Row, this);
                    break;
                }

                case 16:
                {
                    Data = new NpcData(Row, this);
                    break;
                }

                case 17:
                {
                    Data = new DecoData(Row, this);
                    break;
                }

                case 18:
                {
                    Data = new ResourcePackData(Row, this);
                    break;
                }

                case 20:
                {
                    Data = new MissionData(Row, this);
                    break;
                }

                case 21:
                {
                    Data = new BillingPackageData(Row, this);
                    break;
                }

                case 22:
                {
                    Data = new AchievementData(Row, this);
                    break;
                }

                case 25:
                {
                    Data = new SpellData(Row, this);
                    break;
                }

                case 26:
                {
                    Data = new HintData(Row, this);
                    break;
                }

                case 27:
                {
                    Data = new LandingShipData(Row, this);
                    break;
                }

                case 28:
                {
                    Data = new ArtifactData(Row, this);
                    break;
                }

                case 29:
                {
                    Data = new ArtifactBonusData(Row, this);
                    break;
                }

                case 30:
                {
                    Data = new DeepseaParameterData(Row, this);
                    break;
                }

                case 31:
                {
                    Data = new ExplorationCostData(Row, this);
                    break;
                }

                case 34:
                {
                    Data = new ResourceShipData(Row, this);
                    break;
                }

                case 35:
                {
                    Data = new LootBoxData(Row, this);
                    break;
                }

                case 36:
                {
                    Data = new LiberatedIncomeData(Row, this);
                    break;
                }

                case 37:
                {
                    Data = new RegionData(Row, this);
                    break;
                }

                case 38:
                {
                    Data = new DefenseRewardData(Row, this);
                    break;
                }

                case 39:
                {
                    Data = new LocatorData(Row, this);
                    break;
                }

                case 40:
                {
                    Data = new EventData(Row, this);
                    break;
                }

                case 41:
                {
                    Data = new FootstepData(Row, this);
                    break;
                }

                case 42:
                {
                    Data = new PersistentEventRewardData(Row, this);
                    break;
                }

                case 43:
                {
                    Data = new CommunityLinkData(Row, this);
                    break;
                }

                case 44:
                {
                    Data = new ShieldData(Row, this);
                    break;
                }

                case 45:
                {
                    Data = new AbTestData(Row, this);
                    break;
                }

                case 46:
                {
                    Data = new LetterData(Row, this);
                    break;
                }

                case 47:
                {
                    Data = new RankData(Row, this);
                    break;
                }

                case 48:
                {
                    Data = new CountryData(Row, this);
                    break;
                }

                case 51:
                {
                    Data = new BoomboxData(Row, this);
                    break;
                }

                case 52:
                {
                    Data = new HeroData(Row, this);
                    break;
                }

                case 53:
                {
                    Data = new HeroAbilityData(Row, this);
                    break;
                }

                case 54:
                {
                    Data = new OfferData(Row, this);
                    break;
                }

                case 55:
                {
                    Data = new DeepLinkData(Row, this);
                    break;
                }

                case 56:
                {
                    Data = new SectorData(Row, this);
                    break;
                }

                case 57:
                {
                    Data = new SectorBonusData(Row, this);
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