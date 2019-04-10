namespace GL.Servers.CoC.Logic
{
    using GL.Servers.CoC.Extensions.Game;
    using GL.Servers.CoC.Extensions.Helper;
    using GL.Servers.CoC.Files.CSV_Helpers;

    using Newtonsoft.Json.Linq;

    using GL.Servers.CoC.Files;
    using GL.Servers.CoC.Logic.Enums;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;

    internal class UnitUpgradeComponent : Component
    {
        /// <summary>
        /// Gets a value indicating the component type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 9;
            }
        }

        internal Timer Timer;
        internal Data UnitData;

        internal int UnitType;

        /// <summary>
        /// Gets a value indicating whether a upgrade is ongoing.
        /// </summary>
        internal bool UpgradeOnGoing
        {
            get
            {
                return this.Timer != null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitUpgradeComponent"/> class.
        /// </summary>
        public UnitUpgradeComponent(GameObject GameObject) : base(GameObject)
        {
            // UnitUpgradeComponent.
        }

        /// <summary>
        /// Cancels the upgrade.
        /// </summary>
        internal void CancelUpgrade()
        {
            Player Player = this.Parent.Level.Player;

            if (Player != null)
            {
                if (this.UpgradeOnGoing)
                {
                    if (this.UnitData.GetDataType() == 4)
                    {
                        CharacterData Character = (CharacterData) this.UnitData;

                        int CurrentUpgrade = Player.GetUnitUpgradeLevel(this.UnitData);

                        if (Character.UpgradeCost[CurrentUpgrade] > 0)
                        {
                            Player.Resources.Add(Character.UpgradeResourceData, Character.UpgradeCost[CurrentUpgrade] * 50 / 100);
                        }
                    }
                    else
                    {
                        CharacterData Character = (CharacterData) this.UnitData;

                        int CurrentUpgrade = Player.GetUnitUpgradeLevel(this.UnitData);

                        if (Character.UpgradeCost[CurrentUpgrade] > 0)
                        {
                            Player.Resources.Add(Character.UpgradeResourceData, Character.UpgradeCost[CurrentUpgrade] * 50 / 100);
                        }
                    }

                    this.UnitData = null;
                    this.UnitType = 0;
                    this.Timer = null;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the unit can be upgraded.
        /// </summary>
        internal bool CanUpgrade(Data Data)
        {
            int DataType = Data.GetDataType();

            if (DataType == 3 || DataType == 26)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets a value indicating whether you can start upgrading.
        /// </summary>
        internal bool CanStartUpgrading(Data Data)
        {
            Player Player     = this.Parent.Level.Player;
            Building Building = (Building) this.Parent;

            if (Player != null)
            {
                if (!this.UpgradeOnGoing)
                {
                    if (this.CanUpgrade(Data))
                    {
                        if (Data.GetDataType() == 4)
                        {
                            CharacterData Character = (CharacterData) Data;

                            if (Character.UnitOfType == 1)
                            {
                                if (!Character.IsUnlockedForBarrackLevel(this.Parent.Level.ComponentManager.MaxBarrackLevel))
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                if (!Character.IsUnlockedForBarrackLevel(this.Parent.Level.ComponentManager.MaxDarkBarrackLevel))
                                {
                                    return false;
                                }
                            }

                            if (Character.LaboratoryLevel[Player.GetUnitUpgradeLevel(Character)] >= Building.GetUpgradeLevel() && !Building.Constructing)
                            {
                                return true;
                            }

                            return false;
                        }

                        SpellData Spell = (SpellData) Data;
                        
                        if (Spell.UnitOfType == 1)
                        {
                            if (!Spell.IsUnlockedForSpellForgeLevel(this.Parent.Level.ComponentManager.MaxBarrackLevel))
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (!Spell.IsUnlockedForSpellForgeLevel(this.Parent.Level.ComponentManager.MaxDarkBarrackLevel))
                            {
                                return false;
                            }
                        }

                        if (Spell.LaboratoryLevel[Player.GetUnitUpgradeLevel(Spell)] >= Building.GetUpgradeLevel() && !Building.Constructing)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Finishs the upgrading.
        /// </summary>
        internal void FinishUpgrading()
        {
            Player Player = this.Parent.Level.Player;

            if (Player != null)
            {
                if (this.UpgradeOnGoing)
                {
                    Player.IncreaseUnitUpgradeLevel(this.UnitData);

                    this.UnitData = null;
                    this.UnitType = 0;
                    this.Timer = null;
                }
            }
        }

        /// <summary>
        /// Starts upgrading of specified unit.
        /// </summary>
        internal void StartUpgrading(Data Data)
        {
            Player Player = this.Parent.Level.Player;

            if (Player != null)
            {
                if (this.CanStartUpgrading(Data))
                {
                    int Time;

                    if (Data.GetDataType() == 4)
                        Time = ((CharacterData) Data).GetUpgradeTime(Player.GetUnitUpgradeLevel(Data));
                    else
                        Time = ((SpellData) Data).GetUpgradeTime(Player.GetUnitUpgradeLevel(Data));

                    this.Timer = new Timer();
                    this.Timer.StartTimer(this.Parent.Level.Time, Time);

                    this.UnitData = Data;
                    this.UnitType = Data.GetDataType() > 4 ? 1 : 0;
                }
            }
        }

        /// <summary>
        /// Speeds up the unit upgrade.
        /// </summary>
        internal void SpeedUp()
        {
            if (this.UpgradeOnGoing)
            {
                int RemainingSeconds = this.Timer.GetRemainingSeconds(this.Parent.Level.Time);

                if (RemainingSeconds > 0)
                {
                    Player Player = this.Parent.Level.Player;

                    if (Player != null)
                    {
                        int Cost = GamePlayUtil.GetSpeedUpCost(RemainingSeconds, 0, 100);

                        if (Cost > 0)
                        {
                            if (Player.HasEnoughDiamonds(Cost))
                            {
                                Player.UseDiamonds(Cost);
                                this.FinishUpgrading();
                            }
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Creates a fast forward of time.
        /// </summary>
        internal override void FastForwardTime(int Secs)
        {
            if (this.UpgradeOnGoing)
            {
                this.Timer.FastForward(Secs);
            }
        }

        /// <summary>
        /// Loads this instance from json.
        /// </summary>
        internal override void Load(JToken Json)
        {
            JToken UnitUpgrade = Json["unit_upg"];

            if (UnitUpgrade != null)
            {
                if (JsonHelper.GetJsonNumber(UnitUpgrade, "unit_type", out this.UnitType))
                {
                    if (JsonHelper.GetJsonNumber(UnitUpgrade, "id", out int Id) && JsonHelper.GetJsonNumber(UnitUpgrade, "t", out int Time))
                    {
                        this.UnitData = this.UnitType > 0 ? CSV.Tables.Get(Gamefile.Spell).GetDataWithID(Id) : CSV.Tables.Get(Gamefile.Character).GetDataWithID(Id);

                        if (this.UnitData != null)
                        {
                            this.Timer = new Timer();
                            this.Timer.StartTimer(this.Parent.Level.Time, Time);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        internal override void Save(JObject Json)
        {
            JObject UnitUpgrade = new JObject();

            if (this.UpgradeOnGoing)
            {
                UnitUpgrade.Add("unit_type", this.UnitType);
                UnitUpgrade.Add("id", this.UnitData.GlobalID);
                UnitUpgrade.Add("t", this.Timer.GetRemainingSeconds(this.Parent.Level.Time));
            }

            Json.Add("unit_upg", UnitUpgrade);
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal override void Tick()
        {
            if (this.UpgradeOnGoing)
            {
                if (this.Timer.GetRemainingSeconds(this.Parent.Level.Time) <= 0)
                {
                    this.FinishUpgrading();
                }
            }
        }
    }
}