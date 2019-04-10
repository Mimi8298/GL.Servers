using System.Collections.Generic;

namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.BB.Files.Enums;
    using GL.Servers.Files.CSV_Reader;

    internal class BuildingData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="BuildingData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public BuildingData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string BuildingClass
        {
            get; set;
        }

        public List<int> BuildTimeD
        {
            get; set;
        }

        public List<int> BuildTimeH
        {
            get; set;
        }

        public List<int> BuildTimeM
        {
            get; set;
        }

        public List<int> BuildTimeS
        {
            get; set;
        }

        public List<string> BuildCost
        {
            get; set;
        }

        public List<int> TownHallLevel
        {
            get; set;
        }

        public List<string> MaxStoredResource
        {
            get; set;
        }

        public int UnlockProtoAtLevel
        {
            get; set;
        }

        public string PrototypeCost
        {
            get; set;
        }

        public List<int> HousingSpace
        {
            get; set;
        }

        public string ProducesResource
        {
            get; set;
        }

        public List<int> ResourcePerHour
        {
            get; set;
        }

        public List<int> ResourceMax
        {
            get; set;
        }

        public List<int> UnitProduction
        {
            get; set;
        }

        public bool UpgradesUnits
        {
            get; set;
        }

        public List<int> Hitpoints
        {
            get; set;
        }

        public List<int> RegenTime
        {
            get; set;
        }

        public List<int> AttackRange
        {
            get; set;
        }

        public List<int> AttackRate
        {
            get; set;
        }

        public List<int> Damage
        {
            get; set;
        }

        public int MinAttackRange
        {
            get; set;
        }

        public int DamageRadius
        {
            get; set;
        }

        public int AttackSpread
        {
            get; set;
        }

        public int BoostTimeMs
        {
            get; set;
        }

        public int SpeedBoost
        {
            get; set;
        }

        public int ArmorBoost
        {
            get; set;
        }

        public int DamageBoost
        {
            get; set;
        }

        public int CombatSetupTimeMs
        {
            get; set;
        }

        public int CombatTeardownTimeMs
        {
            get; set;
        }

        public int CombatTeardownDelayMs
        {
            get; set;
        }

        public bool ProtectedOutsideCombat
        {
            get; set;
        }

        public string ExportNameSetup
        {
            get; set;
        }

        public string SetupEffect
        {
            get; set;
        }

        public int TurretAngleSetup
        {
            get; set;
        }

        public string ExportNameTeardown
        {
            get; set;
        }

        public string TeardownEffect
        {
            get; set;
        }

        public int TurretAngleTeardown
        {
            get; set;
        }

        public string ExportNameProtected
        {
            get; set;
        }

        public int AmplifierRange
        {
            get; set;
        }

        public int AmplifierDamage
        {
            get; set;
        }

        public List<int> HQShieldPercentage
        {
            get; set;
        }

        public int LaserDistance
        {
            get; set;
        }

        public bool CreatesArtifacts
        {
            get; set;
        }

        public int ArtifactType
        {
            get; set;
        }

        public List<int> ArtifactCapacity
        {
            get; set;
        }

        public List<int> PrototypeCapacity
        {
            get; set;
        }

        public List<int> ArtifactStorageCapacity
        {
            get; set;
        }

        public List<int> DeepseaDepth
        {
            get; set;
        }

        public List<int> StartingEnergy
        {
            get; set;
        }

        public List<int> EnergyGain
        {
            get; set;
        }

        public bool CanNotMove
        {
            get; set;
        }

        public List<int> ExplorableRegions
        {
            get; set;
        }

        public int ReloadTime
        {
            get; set;
        }

        public int ShotsBeforeReload
        {
            get; set;
        }

        public int ResourceProtectionPercent
        {
            get; set;
        }

        public int DamageOverFiveSeconds
        {
            get; set;
        }

        public List<int> XpGain
        {
            get; set;
        }

        public int StunTimeMS
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string InfoTID
        {
            get; set;
        }

        public string SubtitleTID
        {
            get; set;
        }

        public string SWF
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string ExportNameTop
        {
            get; set;
        }

        public string ExportNameNpc
        {
            get; set;
        }

        public string ExportNameConstruction
        {
            get; set;
        }

        public string BarrelType
        {
            get; set;
        }

        public string ModelName
        {
            get; set;
        }

        public string TextureName
        {
            get; set;
        }

        public string EnemyTextureName
        {
            get; set;
        }

        public string MeshName
        {
            get; set;
        }

        public string ShadowModelName
        {
            get; set;
        }

        public string ShadowMeshName
        {
            get; set;
        }

        public string ShadowTextureName
        {
            get; set;
        }

        public int Width
        {
            get; set;
        }

        public int Height
        {
            get; set;
        }

        public bool Passable
        {
            get; set;
        }

        public string ExportNameBuildAnim
        {
            get; set;
        }

        public string DestroyEffect
        {
            get; set;
        }

        public string ShatterEffect
        {
            get; set;
        }

        public string AttackEffect
        {
            get; set;
        }

        public string AttackEffect2
        {
            get; set;
        }

        public string AttackEffect3
        {
            get; set;
        }

        public int TurretX
        {
            get; set;
        }

        public string HitEffect
        {
            get; set;
        }

        public string HitEffect2
        {
            get; set;
        }

        public string Projectile
        {
            get; set;
        }

        public string ExportNameDamaged
        {
            get; set;
        }

        public string ExportNameBase
        {
            get; set;
        }

        public string ExportNameBaseIce
        {
            get; set;
        }

        public string ExportNameBaseFire
        {
            get; set;
        }

        public string ExportNameBaseCoop
        {
            get; set;
        }

        public string ExportNameBaseCrab
        {
            get; set;
        }

        public string PickUpEffect
        {
            get; set;
        }

        public string PlacingEffect
        {
            get; set;
        }

        public string DefenderCharacter
        {
            get; set;
        }

        public int DefenderCount
        {
            get; set;
        }

        public int DefenderZ
        {
            get; set;
        }

        public string MissEffect
        {
            get; set;
        }

        public int VillagerProbability
        {
            get; set;
        }

        public string BuildingReadyEffect
        {
            get; set;
        }

        public int ProjSkewing
        {
            get; set;
        }

        public int ProjYScaling
        {
            get; set;
        }

        public int ProjXOffset
        {
            get; set;
        }

        public bool RandomFrame
        {
            get; set;
        }

        public string PrisonerCharacter
        {
            get; set;
        }

        public int PrisonerCount
        {
            get; set;
        }

        public int PrisonerLevel
        {
            get; set;
        }

        public string ExportNameDestruct
        {
            get; set;
        }

        public int DestructDelayMs
        {
            get; set;
        }

        public int DestructRadius
        {
            get; set;
        }

        public int DestructDamage
        {
            get; set;
        }

        public int DestructSpeedBoost
        {
            get; set;
        }

        public int DestructDamageBoost
        {
            get; set;
        }

        public int DestructBoostDurationMs
        {
            get; set;
        }

        public string DestructEffect
        {
            get; set;
        }

        public string SpawnCharacter
        {
            get; set;
        }

        public bool Invisible
        {
            get; set;
        }

        public int TroopPresets
        {
            get; set;
        }

        public int AntiAirAttacks
        {
            get; set;
        }

        public int TauntRange
        {
            get; set;
        }

        public int Push
        {
            get; set;
        }

        public int PanicDurationMs
        {
            get; set;
        }

        public bool AttackMovingOnly
        {
            get; set;
        }

        public bool IsHeroBoat
        {
            get; set;
        }

        public bool CreatesPrototype()
        {
            return this.PrototypeCapacity[0] > 0;
        }

        public bool StoresArtifactes()
        {
            return this.ArtifactStorageCapacity[0] > 0;
        }

        public int GetArtifactStorageCapacity(int Level)
        {
            return this.ArtifactCapacity[Level];
        }

        public int GetHitpoints(int Level)
        {
            return this.Hitpoints[Level];
        }

        public int GetUnitProduction(int Level)
        {
            return this.UnitProduction[Level];
        }

        public int GetDamage(int Level)
        {
            return this.Damage[Level];
        }

        public int GetAttackRate(int Level)
        {
            return this.AttackRate[Level];
        }

        public int GetAttackRange(int Level)
        {
            return this.AttackRange[Level];
        }

        public ResourceBundle GetBuildCost(int Level)
        {
            return new ResourceBundle(this.BuildCost[Level]);
        }

        public int GetUnitStorageCapacity(int Level)
        {
            return this.HousingSpace[Level];
        }

        public int GetConstructionTime(int Level)
        {
            return this.BuildTimeD[Level] * 86400 + this.BuildTimeH[Level] * 3600 + this.BuildTimeM[Level] * 60 + this.BuildTimeS[Level];
        }

        public int GetWidth()
        {
            return this.Width;
        }

        public int GetHeight()
        {
            return this.Height;
        }
        
        public int GetUpgradeLevelCount()
        {
            return this.BuildCost.Count;
        }

        public BuildingClassData GetBuildingClassData()
        {
            return (BuildingClassData) CSV.Tables.Get(Gamefile.BuildingClass).GetData(this.BuildingClass);
        }

        public bool CanHaveShield()
        {
            return this.BuildingClass == "Town Hall";
        }

        public bool IsTownHallOrCommandCenter()
        {
            return this.BuildingClass == "Town Hall" || this.BuildingClass == "Command Center";
        }

        public bool IsArtifact()
        {
            return this.BuildingClass == "Artifact";
        }

        public bool IsPrison()
        {
            return !string.IsNullOrEmpty(this.PrisonerCharacter);
        }

        public bool IsDeepsea()
        {
            return this.DeepseaDepth[0] > 0;
        }

        public bool IsShieldGenerator()
        {
            return this.HQShieldPercentage[0] > 0;
        }

        public bool IsPrototype()
        {
            return this.BuildingClass == "Prototype";
        }

        internal bool IsLandingBoat()
        {
            if (this.GetUnitStorageCapacity(0) > 0)
            {
                return true;
            }

            return false;
        }

        internal bool IsGunBoat()
        {
            if (this.StartingEnergy[0] > 0)
            {
                return true;
            }

            return false;
        }

        internal bool CanProducesResource()
        {
            return this.ProducesResource != null;
        }

        internal bool IsMapRoom()
        {
            return this.ExplorableRegions[0] > 0;
        }
    }
}
