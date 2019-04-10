namespace GL.Clients.BS.Logic
{
    using Newtonsoft.Json;

    internal class Player
    {
        [JsonProperty("high_id")]           internal int HighID;
        [JsonProperty("low_id")]            internal int LowID;

        [JsonProperty("username")]          internal string Username;
        [JsonProperty("token")]             internal string Token;

        [JsonProperty("trophies")]          internal int Trophies;
        [JsonProperty("high_trophies")]     internal int HighTrophies;
        [JsonProperty("surv_trophies")]     internal int SurvTrophies;
        [JsonProperty("experience")]        internal int Experience;
        [JsonProperty("gold")]              internal int Gold;
        [JsonProperty("joystick_mode")]     internal int JoystickMode;
        [JsonProperty("thumbnail")]         internal int Thumbnail;

        [JsonProperty("hints_enabled")]     internal bool HintsEnabled;

        /* LOGIN INFOS */

        internal string MasterHash          = "666c3e197819bc610652c2385d8e4bd569c18b13";

        internal string AdvertiseID         = "00000000-0000-0000-0000-000000000000";
        internal string OpenUDID            = "00000000-0000-0000-0000-000000000000";

        internal int Major                  = 4;
        internal int Minor                  = 7;
        internal int Revision               = 0;

        internal string Model               = "iPhone9,3";
        internal string OSVersion           = "10.3.1";
        internal string Region              = "fr-FR";

        internal string MACAddress          = null;

        internal bool Android               = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        internal Player()
        {
            // Player.
        }
    }
}