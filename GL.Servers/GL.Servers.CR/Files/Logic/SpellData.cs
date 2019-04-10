namespace GL.Servers.CR.Files.Logic
{
    using System;

    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;
    using GL.Servers.CR.Logic.Enums;

    internal class SpellData : Data
    {
        internal RarityData RarityData;
        internal ArenaData UnlockArenaData;

        internal DateTime ReleaseDateTime;

        /// <summary>
        /// Gets the max level index.
        /// </summary>
        internal int MaxLevelIndex
        {
            get
            {
                return this.RarityData.LevelCount - 1;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpellData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public SpellData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // SpellBuildingData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
        internal override void LoadingFinished()
        {
            this.RarityData = CSV.Tables.Get(Gamefile.Rarity).GetData<RarityData>(this.Rarity);

            if (this.RarityData == null)
            {
                throw new Exception("Spell " + this.GlobalID + " rarity is NULL.");
            }

            this.UnlockArenaData = CSV.Tables.Get(Gamefile.Arena).GetData<ArenaData>(this.UnlockArena);

            if (this.UnlockArenaData == null)
            {
                throw new Exception("UnlockArena not defined for spell " + this.GlobalID + ".");
            }

            if (!string.IsNullOrEmpty(this.ReleaseDate))
            {
                if (this.ReleaseDate == "Soon")
                {
                    this.ReleaseDateTime = new DateTime(2020, 1, 1);
                }
                else if (!DateTime.TryParse(this.ReleaseDate, out this.ReleaseDateTime))
                {
                    throw new Exception("ReleaseDate must be in format YYYY-MM-DD!");
                }
            }

            if (!this.NotInUse)
            {
                CSV.Tables.Spells.Add(this);
            }
        }

        /// <summary>
        /// Gets if the spell is unlocked for specified arena.
        /// </summary>
        internal bool IsUnlockedInArena(ArenaData Data)
        {
            if (Data.TrainingCamp != this.UnlockArenaData.TrainingCamp)
            {
                return false;
            }

            return Data.TrophyLimit >= this.UnlockArenaData.TrophyLimit;
        }

        internal string IconSWF
        {
            get; set;
        }

        internal string IconFile
        {
            get; set;
        }

        internal string UnlockArena
        {
            get; set;
        }

        internal string Rarity
        {
            get; set;
        }

        internal int ManaCost
        {
            get; set;
        }

        internal bool ManaCostFromSummonerMana
        {
            get; set;
        }

        internal bool NotInUse
        {
            get; set;
        }

        internal bool Mirror
        {
            get; set;
        }

        internal int CustomDeployTime
        {
            get; set;
        }

        internal string SummonCharacter
        {
            get; set;
        }

        internal int SummonNumber
        {
            get; set;
        }

        internal int SummonCharacterLevelIndex
        {
            get; set;
        }

        internal string SummonCharacterSecond
        {
            get; set;
        }

        internal int SummonCharacterSecondCount
        {
            get; set;
        }

        internal int SummonRadius
        {
            get; set;
        }

        internal int Radius
        {
            get; set;
        }

        internal int Height
        {
            get; set;
        }

        internal string Projectile
        {
            get; set;
        }

        internal bool SpellAsDeploy
        {
            get; set;
        }

        internal bool CanPlaceOnBuildings
        {
            get; set;
        }

        internal int InstantDamage
        {
            get; set;
        }

        internal int DurationSeconds
        {
            get; set;
        }

        internal int InstantHeal
        {
            get; set;
        }

        internal int HealPerSecond
        {
            get; set;
        }

        internal string Effect
        {
            get; set;
        }

        internal int Pushback
        {
            get; set;
        }

        internal int MultipleProjectiles
        {
            get; set;
        }

        internal string CustomFirstProjectile
        {
            get; set;
        }

        internal int BuffTime
        {
            get; set;
        }

        internal int BuffTimeIncreasePerLevel
        {
            get; set;
        }

        internal int BuffNumber
        {
            get; set;
        }

        internal string BuffType
        {
            get; set;
        }

        internal string BuffOnDamage
        {
            get; set;
        }

        internal bool OnlyOwnTroops
        {
            get; set;
        }

        internal bool OnlyEnemies
        {
            get; set;
        }

        internal bool CanDeployOnEnemySide
        {
            get; set;
        }

        internal bool TouchdownLimitedDeploy
        {
            get; set;
        }

        internal string CastSound
        {
            get; set;
        }

        internal string AreaEffectObject
        {
            get; set;
        }

        internal string TID
        {
            get; set;
        }

        internal string TID_INFO
        {
            get; set;
        }

        internal string IndicatorFileName
        {
            get; set;
        }

        internal string IndicatorEffect
        {
            get; set;
        }

        internal bool HideRadiusIndicator
        {
            get; set;
        }

        internal string DestIndicatorEffect
        {
            get; set;
        }

        internal string ReleaseDate
        {
            get; set;
        }

        internal int ElixirProductionStopTime
        {
            get; set;
        }

        internal bool DarkMirror
        {
            get; set;
        }

        internal bool StatsUnderInfo
        {
            get; set;
        }

    }
}