namespace GL.Servers.CoC.Logic.Attack
{
    using GL.Servers.CoC.Files.CSV_Helpers;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;
    using GL.Servers.CoC.Logic.Slots;
    using GL.Servers.CoC.Logic;
 
    using Newtonsoft.Json.Linq;

    internal class BattleLog
    {
        internal Level Level;

        internal bool AllianceUsed;

        internal int VillageType;

        internal int AttackerScore;
        internal int DefenderScore;
        internal int OriginalAttackerScore;
        internal int OriginalDefenderScore;

        internal int DeployedHousingSpace;

        internal ResourceSlots Loot;
        internal DataSlots Units;
        internal DataSlots Spells;
        internal DataSlots CastleUnits;
        internal DataSlots Levels;

        /// <summary>
        /// Initializes a new instance of the <see cref="BattleLog"/> class.
        /// </summary>
        public BattleLog()
        {
            this.Loot = new ResourceSlots();
            this.Units = new DataSlots();
            this.Spells = new DataSlots();
            this.CastleUnits = new DataSlots();
            this.Levels = new DataSlots();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BattleLog"/> class.
        /// </summary>
        public BattleLog(Level Level) : this()
        {
            this.Level = Level;
        }

        internal JObject GenerateJson()
        {
            JObject Json = new JObject();

            Json.Add("villageType", this.VillageType);
            Json.Add("loot", this.Loot.Save());
            Json.Add("units", this.Units.Save());
            Json.Add("cc_units", this.CastleUnits.Save());
            Json.Add("spells", this.Spells.Save());
            Json.Add("levels", this.Levels.Save());

            JObject Stats = new JObject();

            Stats.Add("townhallDestroyed", false);
            Stats.Add("battleEnded", true);
            Stats.Add("allianceUsed", this.AllianceUsed);

            Stats.Add("destructionPercentage", 0);
            Stats.Add("battleTime", this.Level.Time.TotalSecs);
            Stats.Add("attackerScore", this.AttackerScore);
            Stats.Add("defenderScore", this.DefenderScore);
            Stats.Add("originalAttackerScore", this.OriginalAttackerScore);
            Stats.Add("originalDefenderScore", this.OriginalDefenderScore);
            Stats.Add("lootMultiplierByTownHallDiff", 0);
            Stats.Add("deployedHousingSpace", this.DeployedHousingSpace);
            Stats.Add("armyDeploymentPercentage", 0);
            Stats.Add("attackerStars", 0);

            Json.Add("stats", Stats);

            return Json;
        }
        
        internal void IncreaseStolenResourceCount(ResourceData Resource, int Count)
        {
            if (!Resource.PremiumCurrency)
            {
                this.Loot.Add(Resource, Count);
            }
        }

        internal void DeployCastleUnits(DataSlots Units)
        {
            Units.ForEach(Slot =>
            {
                if (Slot.Count > 0)
                {
                    this.CastleUnits.Add(Slot.Data, Slot.Count);
                    this.DeployedHousingSpace += ((CharacterData) Slot.Data).HousingSpace * Slot.Count;
                }
            });
        }

        internal void IncrementDeployedAttackerUnits(CharacterData Character)
        {
            this.Units.Add(Character, 1);
            this.DeployedHousingSpace += Character.HousingSpace;
        }

        internal void IncrementDeployedSpell(SpellData Spell)
        {
            this.Spells.Add(Spell, 1);
        }

        internal void SetCombatItemLevel(Data Data, int Level)
        {
            this.Levels.Set(Data.GlobalID, Level);
        }

        internal void SetAvailableResourcesForAttacker(Player Defender)
        {
            // TODO To Implement       
        }
    }
}