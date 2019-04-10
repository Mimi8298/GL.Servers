namespace GL.Servers.CR.Logic
{
    using System;
    using GL.Servers.CR.Core;
    using GL.Servers.CR.Extensions.Game;
    using GL.Servers.CR.Extensions.Helper;
    using GL.Servers.CR.Files;
    using GL.Servers.CR.Files.CSV_Helpers;
    using GL.Servers.CR.Files.Logic;
    using GL.Servers.CR.Logic.Apis;
    using GL.Servers.CR.Logic.Enums;
    
    using GL.Servers.CR.Logic.Mode;
    using GL.Servers.CR.Logic.Slots;

    using GL.Servers.DataStream;
    using GL.Servers.Logic.Enums;
    using GL.Servers.Extensions.List;

    using Newtonsoft.Json;

    internal class Player : PlayerBase
    {
        internal GameMode GameMode;

        // Id

        [JsonProperty] private int _HighID;
        [JsonProperty] private int _LowID;

        [JsonProperty] private int _AllianceHighID;
        [JsonProperty] private int _AllianceLowID;

        [JsonProperty] internal string PassToken;

        // Game

        [JsonProperty] private bool _NameSetByUser;

        [JsonProperty] private int _Diamonds;
        [JsonProperty] private int _FreeDiamonds;
        [JsonProperty] private int _Score;
        [JsonProperty] private int _ExpLevel;
        [JsonProperty] private int _ExpPoints;
        [JsonProperty] private int _NameChangeState;
        [JsonProperty] private int _AllianceRole;

        [JsonProperty] private string _Name;
        [JsonProperty] private string _AllianceName;

        [JsonProperty] internal ArenaData Arena;
        [JsonProperty] internal CommoditySlots CommoditySlots;
        [JsonProperty] internal AllianceBadgeData AllianceBadgeData;

        // DateTime

        [JsonProperty] internal DateTime Update    = DateTime.UtcNow;
        [JsonProperty] internal DateTime Created   = DateTime.UtcNow;
        [JsonProperty] internal DateTime BanTime   = DateTime.UtcNow;

        // Home

        [JsonProperty] internal Home Home;

        // Debug

        [JsonProperty] internal Rank Rank = Rank.Player;

        // Apis

        [JsonProperty] internal Google Google;
        [JsonProperty] internal Facebook Facebook;
        [JsonProperty] internal Gamecenter Gamecenter;
        
        /// <summary>
        /// Gets the player identifier.
        /// </summary>
        internal long PlayerID
        {
            get
            {
                return (long) this._HighID << 32 | (uint) this._LowID;
            }
        }

        /// <summary>
        /// Gets the player high identifier.
        /// </summary>
        internal int HighID
        {
            get
            {
                return this._HighID;
            }
        }

        /// <summary>
        /// Gets the player low identifier.
        /// </summary>
        internal int LowID
        {
            get
            {
                return this._LowID;
            }
        }

        /// <summary>
        /// Gets the alliance identifier.
        /// </summary>
        internal long AllianceID
        {
            get
            {
                return (long) this._HighID << 32 | (uint) this._LowID;
            }
        }

        /// <summary>
        /// Gets the alliance high identifier.
        /// </summary>
        internal int AllianceHighID
        {
            get
            {
                return this._AllianceHighID;
            }
        }

        /// <summary>
        /// Gets the alliance low identifier.
        /// </summary>
        internal int AllianceLowID
        {
            get
            {
                return this._AllianceLowID;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Player"/> is banned.
        /// </summary>
        internal bool Banned
        {
            get
            {
                return this.BanTime > DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player is connected.
        /// </summary>
        internal bool Connected
        {
            get
            {
                if (this.GameMode != null)
                {
                    return this.GameMode.Connected;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets this instance generated checksum.
        /// </summary>
        internal override int Checksum
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the exp level of this player.
        /// </summary>
        internal int ExpLevel
        {
            get
            {
                return this._ExpLevel;
            }
        }

        /// <summary>
        /// Gets the score of this player.
        /// </summary>
        internal int Score
        {
            get
            {
                return this._Score;
            }
        }

        /// <summary>
        /// Gets the name change state of this player.
        /// </summary>
        internal int NameChangeState
        {
            get
            {
                return this._NameChangeState;
            }
        }

        /// <summary>
        /// Gets if the name has been set by user.
        /// </summary>
        internal bool NameSetByUser
        {
            get
            {
                return this._NameSetByUser;
            }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        internal string Name
        {
            get
            {
                return this._Name;
            }
        }

        /// <summary>
        /// Gets the alliance name.
        /// </summary>
        internal string AllianceName
        {
            get
            {
                return this._AllianceName;
            }
        }

        internal Data ExpLevelData
        {
            get
            {
                return CSV.Tables.Get(Gamefile.ExpLevel).GetWithInstanceID(this._ExpLevel - 1);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        internal Player()
        {
            this.Home           = new Home();
            this.CommoditySlots = new CommoditySlots();

            // Apis

            this.Google         = new Google(this);
            this.Facebook       = new Facebook(this);
            this.Gamecenter     = new Gamecenter(this);

            this.Arena          = (ArenaData) CSV.Tables.Get(Gamefile.Arena).Datas[1];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="HighID">The high identifier.</param>
        /// <param name="LowID">The low identifier.</param>
        internal Player(int HighID, int LowID) : this()
        {
            this._HighID        = HighID;
            this._LowID         = LowID;

            this.Initialize();
        }

        /// <summary>
        /// Initializes values of members.
        /// </summary>
        internal void Initialize()
        {
            this.CommoditySlots.Initialize();

            this._ExpLevel = 1;
            this._Diamonds = Globals.StartingDiamonds;
            this._FreeDiamonds = Globals.StartingDiamonds;
        }

        /// <summary>
        /// Adds free diamond.
        /// </summary>
        internal void AddFreeDiamonds(int Count)
        {
            if (Count > 0)
            {
                this._Diamonds += Count;
                this._FreeDiamonds += Count;
            }
        }

        /// <summary>
        /// Adds free gold.
        /// </summary>
        internal void AddFreeGold(int Count)
        {
            if (Count > 0)
            {
                this.CommoditySlots.AddCommodityCount(CommodityType.Resource, CSV.Tables.GoldData, Count);
                this.CommoditySlots.AddCommodityCount(CommodityType.Resource, CSV.Tables.FreeGoldData, Count);
            }
        }

        /// <summary>
        /// Adds free gold.
        /// </summary>
        internal bool HasEnoughResources(ResourceData Data, int Count)
        {
            return this.CommoditySlots.GetCommodityCount(CommodityType.Resource, Data) >= Count;
        }

        /// <summary>
        /// Levels up this player.
        /// </summary>
        private void LevelUp(int ExpLevel)
        {
            ExpLevelData Data = CSV.Tables.Get(Gamefile.ExpLevel).GetWithInstanceID<ExpLevelData>(ExpLevel - 1);

            this.AddFreeDiamonds(Data.DiamondReward);
        }

        /// <summary>
        /// Sets the alliance id.
        /// </summary>
        internal void SetAllianceId(int HighID, int LowID)
        {
            this._AllianceHighID = HighID;
            this._AllianceLowID  = LowID;
        }

        /// <summary>
        /// Sets the alliance name.
        /// </summary>
        internal void SetAllianceName(string Name)
        {
            this._AllianceName = Name;
        }

        /// <summary>
        /// Sets the name of the player.
        /// </summary>
        internal void SetName(string Name)
        {
            this._Name = Name;
        }

        /// <summary>
        /// Sets the role of the player in the alliance.
        /// </summary>=
        internal void SetAllianceRole(int Role)
        {
            this._AllianceRole = Role;
        }

        /// <summary>
        /// Sets the number of diamonds.
        /// </summary>
        internal void SetDiamonds(int Count)
        {
            this._Diamonds = Count;
        }

        /// <summary>
        /// Sets the number of golds.
        /// </summary>
        internal void SetFreeGold(int Count)
        {
            this.CommoditySlots.SetCommodityCount(CommodityType.Resource, CSV.Tables.GoldData, Count);
            this.CommoditySlots.SetCommodityCount(CommodityType.Resource, CSV.Tables.FreeGoldData, Count);
        }

        /// <summary>
        /// Sets the number of diamonds.
        /// </summary>
        internal void SetFreeDiamonds(int Count)
        {
            this._FreeDiamonds = Count;
        }

        /// <summary>
        /// Sets the number of chest.
        /// </summary>
        internal void SetCardsFound(int Count)
        {
            this.CommoditySlots.SetCommodityCount(CommodityType.ProfileResource, CSV.Tables.CardCountData, Count);
        }

        /// <summary>
        /// Sets the number of chest.
        /// </summary>
        internal void SetChestCount(int Count)
        {
            if (Count > 4)
            {
                Logging.Error(this.GetType() , "SetChestCount() - Set chest count out of bounds: "+ Count);
                return;
            }

            this.CommoditySlots.SetCommodityCount(CommodityType.Resource, CSV.Tables.ChestCountData, Count);
        }

        /// <summary>
        /// Sets the favourite spell.
        /// </summary>
        internal void SetFavouriteSpell(SpellData Spell)
        {
            if (Spell != null)
            {
                Data Data = CSV.Tables.Get(Gamefile.Resource).GetData("FavouriteSpell");
                this.CommoditySlots.SetCommodityCount(CommodityType.ProfileResource, Data, Data.GlobalID);
            }
        }

        /// <summary>
        /// Sets the max score.
        /// </summary>
        internal void SetMaxScore(int Count)
        {
            Data Data = CSV.Tables.Get(Gamefile.Resource).GetData("MaxScore");
            this.CommoditySlots.SetCommodityCount(CommodityType.ProfileResource, Data, Data.GlobalID);
        }

        /// <summary>
        /// Sets the number of diamonds.
        /// </summary>
        internal void SetNameChangeState(int State)
        {
            this._NameChangeState = State;
        }

        /// <summary>
        /// Sets if the name is set by user.
        /// </summary>
        internal void SetNameSetByUser(bool Value)
        {
            this._NameSetByUser = Value;
        }

        /// <summary>
        /// Uses the specified diamonds.
        /// </summary>
        internal void UseDiamonds(int Count)
        {
            this._Diamonds -= Count;
            this._FreeDiamonds -= Count;
        }

        /// <summary>
        /// Uses the specified diamonds.
        /// </summary>
        internal void UseFreeGold(int Count)
        {
            this.CommoditySlots.UseCommodity(CommodityType.Resource, CSV.Tables.GoldData, Count);
            this.CommoditySlots.UseCommodity(CommodityType.Resource, CSV.Tables.FreeGoldData, Count);
        }

        /// <summary>
        /// Adds the number of specified exp points.
        /// </summary>
        internal void XpGainHelper(int ExpPoints)
        {
            int MaxExpLevel = CSV.Tables.MaxExpLevel;

            if (CSV.Tables.MaxExpLevel > this._ExpLevel)
            {
                this._ExpPoints += ExpPoints;

                ExpLevelData Data;

                for (int i = this._ExpLevel; i < MaxExpLevel; i++)
                {
                    Data = CSV.Tables.Get(Gamefile.ExpLevel).GetWithInstanceID<ExpLevelData>(this._ExpLevel - 1);

                    if (this._ExpPoints > Data.ExpToNextLevel)
                    {
                        this._ExpPoints -= Data.ExpToNextLevel;
                        ++this._ExpLevel;

                        this.LevelUp(this._ExpLevel - 1);
                    }
                }

                Data = CSV.Tables.Get(Gamefile.ExpLevel).GetWithInstanceID<ExpLevelData>(this._ExpLevel - 1);

                if (this._ExpPoints > Data.ExpToNextLevel)
                {
                    this._ExpPoints = Data.ExpToNextLevel;
                }
            }
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal void Encode(ByteStream Packet, bool Attack = false)
        {
            Packet.AddLogicLong(this._HighID, this._LowID); // Avatar
            Packet.AddLogicLong(this._HighID, this._LowID); // Account
            Packet.AddLogicLong(this._HighID, this._LowID); // Home

            Packet.AddString(this.Name);

            if (!Attack)
            {
                Packet.AddVInt(this.NameChangeState);
            }

            Packet.EncodeLogicData(CSV.Tables.Get(Gamefile.Arena).Datas[1], 54); // DEBUG
            Packet.AddVInt(this._Score);
            Packet.AddVInt(0);
            Packet.AddVInt(0);

            if (!Attack)
            {
                Packet.AddVInt(0);
            }

            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.EncodeLogicData(CSV.Tables.Get(Gamefile.Arena).Datas[1], 54); // DEBUG
            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.EncodeLogicData(CSV.Tables.Get(Gamefile.Arena).Datas[1], 54); // DEBUG

            this.CommoditySlots.Encode(Packet);
            
            if (Attack)
            {
                Packet.AddVInt(this._ExpLevel);
            }
            else
            {
                Packet.AddVInt(this._Diamonds);
                Packet.AddVInt(this._FreeDiamonds);
                Packet.AddVInt(this._ExpPoints);
                Packet.AddVInt(this._ExpLevel);
                Packet.AddVInt(0);

                Packet.AddBoolean(this._NameSetByUser);
                Packet.AddBoolean(false); // ?
            }

            Packet.AddBoolean(false); // ?

            if (this._AllianceLowID > 0)
            {
                Packet.AddBoolean(true);

                Packet.AddLogicLong(this._AllianceHighID, this._AllianceLowID);
                Packet.AddString(this._AllianceName);
                Packet.EncodeLogicData(this.AllianceBadgeData, 16);

                if (!Attack)
                {
                    Packet.AddVInt(this._AllianceRole);
                }
            }
            else 
                Packet.AddBoolean(false);

            Packet.AddRange("16-00-00-00-16-6A-06-00-00".HexaToBytes());

            Packet.AddBoolean(false);
            Packet.AddVInt(1);
            Packet.AddBoolean(false);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.HighID + "-" + this.LowID;
        }
    }
}
