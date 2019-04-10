namespace GL.Clients.BS.Logic
{
    using GL.Clients.BS.Logic.Slots;

    using Newtonsoft.Json;

    internal class Profile
    {
        [JsonProperty("high_id")]           internal int HighID;
        [JsonProperty("low_id")]            internal int LowID;

        [JsonProperty("username")]          internal string Username;

        [JsonProperty("trophies")]          internal int Trophies;
        [JsonProperty("high_trophies")]     internal int HighTrophies;
        [JsonProperty("surv_trophies")]     internal int SurvTrophies;
        [JsonProperty("experience")]        internal int Experience;
        [JsonProperty("gold")]              internal int Wins;
        [JsonProperty("thumbnail")]         internal int Thumbnail;

        [JsonProperty("cards")]             internal Cards Cards;
        [JsonProperty("clan")]              internal Clan Clan;

        /// <summary>
        /// Gets a value indicating whether this instance is in a clan.
        /// </summary>
        internal bool hasClan
        {
            get
            {
                return this.Clan != null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        internal Profile()
        {
            this.Cards = new Cards();
        }
    }

    internal class Clan
    {
        [JsonProperty("high_id")]       internal int HighID;
        [JsonProperty("low_id")]        internal int LowID;

        [JsonProperty("name")]          internal string Name;

        [JsonProperty("badge")]         internal int Badge;
        [JsonProperty("type")]          internal int Type;
        [JsonProperty("member_count")]  internal int MemberCount;
        [JsonProperty("trophies")]      internal int Trophies;
        [JsonProperty("req_trophies")]  internal int RequiredTrophies;

        [JsonProperty("member_role")]   internal int MemberRole;
    }
}