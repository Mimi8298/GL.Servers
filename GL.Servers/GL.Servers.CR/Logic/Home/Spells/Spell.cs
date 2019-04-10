namespace GL.Servers.CR.Logic.Spells
{
    using System;
    using GL.Servers.CR.Extensions.Helper;
    using GL.Servers.CR.Files.Logic;
    using GL.Servers.DataStream;

    using Newtonsoft.Json.Linq;

    internal class Spell
    {
        private SpellData _Data;

        private int _Count;
        private int _Level;
        private int _CreateTime;
        private int _RecentUseCount;

        private int _NewCount;

        private bool _NewFlag;
        private bool _NewUpgrade;

        /// <summary>
        /// Gets the data of this spell.
        /// </summary>
        internal SpellData Data
        {
            get
            {
                return this._Data;
            }
        }

        /// <summary>
        /// Gets the material count.
        /// </summary>
        internal int Count
        {
            get
            {
                return this._Count;
            }
        }

        /// <summary>
        /// Gets the level index.
        /// </summary>
        internal int Level
        {
            get
            {
                return this._Level;
            }
        }

        /// <summary>
        /// Gets the create time.
        /// </summary>
        internal int CreateTime
        {
            get
            {
                return this._CreateTime;
            }
        }

        /// <summary>
        /// Gets the recent use count.
        /// </summary>
        internal int RecentUseCount
        {
            get
            {
                return this._RecentUseCount;
            }
        }

        /// <summary>
        /// Gets if this spell can be upgraded.
        /// </summary>
        internal bool CanUpgrade
        {
            get
            {
                return this.Count >= this.MaterialCountForNextLevel;
            }
        }

        /// <summary>
        /// Gets the recent use count.
        /// </summary>
        internal int MaterialCountForNextLevel
        {
            get
            {
                if (this._Level >= this._Data.MaxLevelIndex)
                {
                    return this._Data.MaxLevelIndex;
                }

                return this._Data.RarityData.UpgradeMaterialCount[this._Level];
            }
        }

        /// <summary>
        /// Gets the number of xp gain for upgrade.
        /// </summary>
        internal int UpgradeExp
        {
            get
            {
                return this._Data.RarityData.UpgradeExp[this._Level];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Spell"/> class.
        /// </summary>
        public Spell(SpellData Data)
        {
            this._Data = Data;
        }

        /// <summary>
        /// Adds the specified material count.
        /// </summary>
        internal void AddMaterialCount(int Count)
        {
            this._Count += Count;
        }

        /// <summary>
        /// Upgrades this spell to next level.
        /// </summary>
        internal void UpgradeToNextLevel()
        {
            if (this.CanUpgrade)
            {
                this._Count -= this.MaterialCountForNextLevel;
                ++this._Level;
                this._NewUpgrade = false;
            }
        }

        /// <summary>
        /// Adds the specified material count.
        /// </summary>
        internal void SetCreateTime(int CreateTime)
        {
            this._CreateTime = CreateTime;
        }

        /// <summary>
        /// Adds the specified material count.
        /// </summary>
        internal void SetMaterialCount(int Count)
        {
            this._Count = Count;
        }

        /// <summary>
        /// Adds the specified material count.
        /// </summary>
        internal void SetShowNewIcon(bool Show)
        {
            this._NewFlag = Show;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        internal Spell Clone()
        {
            return new Spell(this._Data)
            {
                _Count = this._Count,
                _CreateTime = this._CreateTime,
                _Level = this._Level,
                _NewCount = this._NewCount,
                _NewFlag = this._NewFlag,
                _NewUpgrade = this._NewUpgrade,
                _RecentUseCount = this._RecentUseCount
            };
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal void Decode(ByteStream Reader)
        {
            this._Data = Reader.DecodeData<SpellData>();
            this._Level = Reader.ReadVInt();
            this._CreateTime = Reader.ReadVInt();
            this._Count = Reader.ReadVInt();
            this._NewCount = Reader.ReadVInt();
            this._RecentUseCount = Reader.ReadVInt();
            this._NewUpgrade = Reader.ReadBoolean();
            this._NewFlag = Reader.ReadBoolean();
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal void Encode(ChecksumEncoder Packet)
        {
            Packet.EncodeLogicData(this._Data, 26);
            Packet.AddVInt(this._Level);
            Packet.AddVInt(this._CreateTime);
            Packet.AddVInt(this._Count);
            Packet.AddVInt(this._NewCount);
            Packet.AddVInt(this._RecentUseCount);
            Packet.AddBoolean(this._NewUpgrade);
            Packet.AddBoolean(this._NewFlag);
        }

        /// <summary>
        /// Loads this instance from json.
        /// </summary>
        internal void Load(JToken Json)
        {
            JsonHelper.GetJsonData(Json, "d", out this._Data);
            JsonHelper.GetJsonNumber(Json, "t", out this._CreateTime);
            JsonHelper.GetJsonNumber(Json, "c", out this._Count);
            JsonHelper.GetJsonNumber(Json, "l", out this._Level);
            JsonHelper.GetJsonNumber(Json, "newc", out this._NewCount);
            JsonHelper.GetJsonNumber(Json, "rcnt", out this._RecentUseCount);
            JsonHelper.GetJsonBoolean(Json, "newf", out this._NewFlag);
            JsonHelper.GetJsonBoolean(Json, "newu", out this._NewUpgrade);
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        internal JObject Save()
        {
            JObject Json = new JObject();

            if (this._Data != null)
            {
                Json.Add("d", this._Data.GlobalID);
            }

            Json.Add("t", this._CreateTime);
            Json.Add("c", this._Count);
            Json.Add("l", this._Level);
            Json.Add("newc", this._NewCount);
            Json.Add("rnct", this._RecentUseCount);

            Json.Add("newf", this._NewFlag);
            Json.Add("newu", this._NewUpgrade);

            return Json;
        }

        internal bool Equals(Spell Spell)
        {
            return Spell._Data == this._Data;
        }

        public override bool Equals(object obj)
        {
            if (obj is Spell Spell)
            {
                return Spell._Data == this._Data;
            }

            return false;
        }
    }
}