namespace GL.Servers.CR.Files.Logic
{
    using System;
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;
    using GL.Servers.CR.Logic.Enums;

    internal class TreasureChestData : Data
    {
        internal ArenaData ArenaData;
        internal TreasureChestData BaseTreasureChestData;
        internal SpellData[] GuaranteedSpellsData;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreasureChestData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public TreasureChestData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // TreasureChestData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
		    if (!string.IsNullOrEmpty(this.Arena))
		    {
		        this.ArenaData = CSV.Tables.Get(Gamefile.Arena).GetData<ArenaData>(this.Arena);

		        if (this.ArenaData == null)
		        {
		            throw new Exception("Arena " + this.Arena + " does not exist.");
		        }
		    }

		    if (!string.IsNullOrEmpty(this.BaseChest))
		    {
		        this.BaseTreasureChestData = this.DataTable.GetData<TreasureChestData>(this.BaseChest);
		    }

	    	this.GuaranteedSpellsData = new SpellData[this.GuaranteedSpells.Length];

		    for (int i = 0; i < this.GuaranteedSpells.Length; i++)
		    {
		        this.GuaranteedSpellsData[i] = CSV.Tables.GetSpellDataByName(this.GuaranteedSpells[i]);
		    }
		}

        /// <summary>
        /// Gets the chance for specified rarity.
        /// </summary>
        internal int GetChanceForRarity(RarityData Data)
        {
            int Chance;
            int Unlocked = this.ArenaData.GetUnlockedSpellCountForRarity(Data);

            if (Unlocked > 0)
            {
                if (this.BaseTreasureChestData != null)
                {
                    Chance = this.BaseTreasureChestData.GetChanceForRarity(Data);
                }
                else
                {
                    if (Data.Name == "Legendary")
                    {
                        Chance = this.LegendaryChance;
                    }
                    else
                    {
                        if (Data.Name == "Epic")
                        {
                            Chance = this.EpicChance;
                        }
                        else
                        {
                            if (Data.Name == "Rare")
                            {
                                Chance = this.RareChance;
                            }
                            else
                                Chance = 1;
                        }
                    }
                }
                
                return Chance * this.ArenaData.GetUnlockedSpellCountForRarity(CSV.Tables.RarityCommonData) / this.ArenaData.GetUnlockedSpellCountForRarity(Data);
            }

            return 0;
        }

        /// <summary>
        /// Gets the number of random spell.
        /// </summary>
        /// <returns></returns>
        internal int GetRandomSpellCount()
        {
            int Count;

            if (this.BaseChest != null)
            {
                Count = this.BaseTreasureChestData.RandomSpells;
            }
            else
                Count = this.RandomSpells;

            return this.ArenaData.GetScaledChestReward(Count);
        }

        internal string BaseChest
        {
            get; set;
        }

        internal string Arena
        {
            get; set;
        }

        internal bool InShop
        {
            get; set;
        }

        internal bool InArenaInfo
        {
            get; set;
        }

        internal bool TournamentChest
        {
            get; set;
        }

        internal bool SurvivalChest
        {
            get; set;
        }

        internal int ShopPriceWithoutSpeedUp
        {
            get; set;
        }

        internal int TimeTakenDays
        {
            get; set;
        }

        internal int TimeTakenHours
        {
            get; set;
        }

        internal int TimeTakenMinutes
        {
            get; set;
        }

        internal int TimeTakenSeconds
        {
            get; set;
        }

        internal int RandomSpells
        {
            get; set;
        }

        internal int DifferentSpells
        {
            get; set;
        }

        internal int ChestCountInChestCycle
        {
            get; set;
        }

        internal int RareChance
        {
            get; set;
        }

        internal int EpicChance
        {
            get; set;
        }

        internal int LegendaryChance
        {
            get; set;
        }

        internal int SkinChance
        {
            get; set;
        }

        internal string[] GuaranteedSpells
        {
            get; set;
        }

        internal int MinGoldPerCard
        {
            get; set;
        }

        internal int MaxGoldPerCard
        {
            get; set;
        }

        internal string FileName
        {
            get; set;
        }

        internal string ExportName
        {
            get; set;
        }

        internal string ShopExportName
        {
            get; set;
        }

        internal string GainedExportName
        {
            get; set;
        }

        internal string AnimExportName
        {
            get; set;
        }

        internal string OpenInstanceName
        {
            get; set;
        }

        internal string SlotLandEffect
        {
            get; set;
        }

        internal string OpenEffect
        {
            get; set;
        }

        internal string TapSound
        {
            get; set;
        }

        internal string TapSoundShop
        {
            get; set;
        }

        internal string DescriptionTID
        {
            get; set;
        }

        internal string TID
        {
            get; set;
        }

        internal string NotificationTID
        {
            get; set;
        }

        internal string SpellSet
        {
            get; set;
        }

        internal int Exp
        {
            get; set;
        }

        internal int SortValue
        {
            get; set;
        }

        internal bool SpecialOffer
        {
            get; set;
        }

        internal bool DraftChest
        {
            get; set;
        }

        internal string IconSWF
        {
            get; set;
        }

        internal string IconExportName
        {
            get; set;
        }

        internal bool BoostedChest
        {
            get; set;
        }

    }
}