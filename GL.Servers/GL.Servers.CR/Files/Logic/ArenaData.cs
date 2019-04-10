namespace GL.Servers.CR.Files.Logic
{
    using System.Collections.Generic;
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Enums;

    internal class ArenaData : Data
    {
        internal List<SpellData>[] UnlockedSpellsData;

        /// <summary>
        /// Gets the previous arena data.
        /// </summary>
        internal ArenaData PreviousArena
        {
            get
            {
                ArenaData Previous = null;

                this.DataTable.Datas.ForEach(Data =>
                {
                    ArenaData ArenaData = (ArenaData) Data;

                    if (ArenaData.TrainingCamp && !this.TrainingCamp || ArenaData.TrophyLimit < this.DemoteTrophyLimit)
                    {
                        if (Previous != null)
                        {
                            if (ArenaData.TrophyLimit > Previous.TrophyLimit)
                            {
                                Previous = ArenaData;
                            }
                        }
                        else
                            Previous = ArenaData;
                    }
                });

                return Previous;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArenaData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ArenaData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // ArenaData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished2()
		{
            this.UnlockedSpellsData = new List<SpellData>[CSV.Tables.Get(Gamefile.Rarity).Datas.Count];

		    for (int i = 0; i < this.UnlockedSpellsData.Length; i++)
		    {
		        this.UnlockedSpellsData[i] = new List<SpellData>();

		        foreach (SpellData Data in CSV.Tables.Get(Gamefile.SpellCharacter).Datas)
		        {
		            if (Data.IsUnlockedInArena(this))
		            {
		                if (Data.RarityData == CSV.Tables.Get(Gamefile.Rarity).Datas[i])
		                {
		                    this.UnlockedSpellsData[i].Add(Data);
                        }
		            }
		        }

		        foreach (SpellData Data in CSV.Tables.Get(Gamefile.SpellBuilding).Datas)
		        {
		            if (Data.IsUnlockedInArena(this))
		            {
		                if (Data.RarityData == CSV.Tables.Get(Gamefile.Rarity).Datas[i])
		                {
		                    this.UnlockedSpellsData[i].Add(Data);
		                }
		            }
		        }

		        foreach (SpellData Data in CSV.Tables.Get(Gamefile.SpellOther).Datas)
		        {
		            if (Data.IsUnlockedInArena(this))
		            {
		                if (Data.RarityData == CSV.Tables.Get(Gamefile.Rarity).Datas[i])
		                {
		                    this.UnlockedSpellsData[i].Add(Data);
		                }
		            }
		        }

		        foreach (SpellData Data in CSV.Tables.Get(Gamefile.SpellOther).Datas)
		        {
		            if (Data.IsUnlockedInArena(this))
		            {
		                if (Data.RarityData == CSV.Tables.Get(Gamefile.Rarity).Datas[i])
		                {
		                    this.UnlockedSpellsData[i].Add(Data);
		                }
		            }
		        }
            }
		}

        /// <summary>
        /// Gets the unlocked spell count for specified rarity.
        /// </summary>
        internal int GetUnlockedSpellCountForRarity(RarityData Rarity)
        {
            return this.UnlockedSpellsData[Rarity.Instance].Count;
        }

        /// <summary>
        /// Returns count scaled by chest roward multiplier.
        /// </summary>
        internal int GetScaledChestReward(int Count)
        {
            if (this.PreviousArena == null || this.PreviousArena.ChestRewardMultiplier >= this.ChestRewardMultiplier)
            {
                return (this.ChestRewardMultiplier * Count + 50) / 100;
            }

            long v6 = (1374389535L * (this.ChestRewardMultiplier * Count + 50)) >> 32;
            return Math.Max(this.PreviousArena.GetScaledChestReward(Count) + 1, (int) (((int) v6 >> 5) + (v6 >> 31)));
        }

        internal string TID
        {
            get; set;
        }

        internal string SubtitleTID
        {
            get; set;
        }

        internal int Arena
        {
            get; set;
        }

        internal string ChestArena
        {
            get; set;
        }

        internal string TvArena
        {
            get; set;
        }

        internal bool IsInUse
        {
            get; set;
        }

        internal bool TrainingCamp
        {
            get; set;
        }

        internal bool PVEArena
        {
            get; set;
        }

        internal int TrophyLimit
        {
            get; set;
        }

        internal int DemoteTrophyLimit
        {
            get; set;
        }

        internal int SeasonTrophyReset
        {
            get; set;
        }

        internal int ChestRewardMultiplier
        {
            get; set;
        }

        internal int BoostedCrownChestRewardMultiplier
        {
            get; set;
        }

        internal int ChestShopPriceMultiplier
        {
            get; set;
        }

        internal int RequestSize
        {
            get; set;
        }

        internal int MaxDonationCountCommon
        {
            get; set;
        }

        internal int MaxDonationCountRare
        {
            get; set;
        }

        internal int MaxDonationCountEpic
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

        internal string MainMenuIconExportName
        {
            get; set;
        }

        internal string SmallIconExportName
        {
            get; set;
        }

        internal int MatchmakingMinTrophyDelta
        {
            get; set;
        }

        internal int MatchmakingMaxTrophyDelta
        {
            get; set;
        }

        internal int MatchmakingMaxSeconds
        {
            get; set;
        }

        internal string PvpLocation
        {
            get; set;
        }

        internal string TeamVsTeamLocation
        {
            get; set;
        }

        internal int DailyDonationCapacityLimit
        {
            get; set;
        }

        internal int BattleRewardGold
        {
            get; set;
        }

        internal string ReleaseDate
        {
            get; set;
        }

        internal string SeasonRewardChest
        {
            get; set;
        }

        internal string QuestCycle
        {
            get; set;
        }

        internal string ForceQuestChestCycle
        {
            get; set;
        }

    }
}