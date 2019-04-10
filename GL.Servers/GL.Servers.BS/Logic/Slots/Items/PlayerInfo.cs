namespace GL.Servers.BS.Logic.Slots.Items
{
    using System;
    using System.Collections.Generic;

    using GL.Servers.BS.Files.CSV_Helpers;

    using GL.Servers.Extensions.List;

    using Newtonsoft.Json;

    internal class PlayerInfo
    {
        internal Player Player;

        [JsonProperty("trophies")]      internal int Trophies       = 4000;
        [JsonProperty("high_trophies")] internal int HighTrophies   = 4000;
        [JsonProperty("surv_trophies")] internal int SurvTrophies   = 4000;
        [JsonProperty("experience")]    internal int Experience;
        [JsonProperty("gold")]          internal int Gold;
        [JsonProperty("joystick_mode")] internal int JoystickMode   = 1;

        [JsonProperty("hints_enabled")] internal bool HintsEnabled;

        [JsonProperty("thumbnail")]     internal int Thumbnail      = 28000000;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerInfo"/> class.
        /// </summary>
        /// <param name="Player">The player.</param>
        public PlayerInfo(Player Player)
        {
            this.Player = Player;
        }

        /// <summary>
        /// Encodes the specified packet.
        /// </summary>
        /// <param name="Packet">The packet.</param>
        public void Encode(List<byte> Packet)
        {
            DateTime UTCNow = DateTime.UtcNow;

            Packet.AddVInt(UTCNow.Year * 1000 + UTCNow.DayOfYear);
            Packet.AddVInt(UTCNow.Second + UTCNow.Minute * 60 + UTCNow.Hour * 3600);

            Packet.AddVInt(this.Trophies);
            Packet.AddVInt(this.Trophies);

            Packet.AddVInt(999999999); // Exp
            Packet.AddVInt(0);

            Packet.AddVInt(GlobalID.GetType(this.Thumbnail));
            Packet.AddVInt(GlobalID.GetID(this.Thumbnail));

            Packet.AddVInt(1); // Array
            {
                Packet.AddVInt(1);
            }
            
            Packet.AddVInt(0); // Array (data reference)
            Packet.AddVInt(0); // Array (data reference)
            
            Packet.AddBool(false);
            
            int GoldReward = this.Player.Resources.Get(5000001) - this.Gold;

            if (GoldReward < 0)
            {
                GoldReward = 0;
            }

            Packet.AddVInt(GoldReward);

            Packet.AddVInt(this.JoystickMode);
            Packet.AddBool(this.HintsEnabled);

            Packet.AddVInt(0);
            Packet.AddVInt((int) TimeSpan.FromHours(0).TotalSeconds);

            Packet.AddBool(false);

            {
                Packet.AddHexa("8C-01");
                Packet.AddHexa("1E");

                Packet.AddHexa("90-01");
                Packet.AddHexa("28");
            }

            {
                Packet.AddHexa("8C-04");
                Packet.AddHexa("28");

                Packet.AddHexa("A1-04");
                Packet.AddHexa("32");
            }

            Packet.AddData(2, 6);

            Packet.AddVInt(0);

            Packet.AddBools(true, true);
        }

        internal void Tick()
        {
            this.Gold = this.Player.Resources.Get(Enums.Resource.Gold);
        }
    }
}