namespace GL.Servers.BB.Logic.Map
{
    using GL.Servers.BB.Core;
    using GL.Servers.Extensions.List;
    using GL.Servers.BB.Files.CSV_Logic;
    using Newtonsoft.Json;

    internal class MapNode
    {
        internal MapRegion Region;
        [JsonProperty] internal ResourceBundle Loot;
        
        [JsonProperty] internal int Type;
        [JsonProperty] internal int Score;

        [JsonProperty] internal string Name;
        [JsonProperty] internal string Note;

        internal bool IsAttackable
        {
            get
            {
                if (this.Type <= 17)
                    return ((65822 >> this.Type) & 1) > 0;
                return false;
            }
        }

        internal int Checksum
        {
            get
            {
                return this.Type + (this.Type >> 32);
            }
        }

        public MapNode()
        {
            this.Loot = new ResourceBundle();
            this.Loot.Init();
        }

        public MapNode(MapRegion Region, int Type) : this()
        {
            this.Region = Region;
            this.Type   = Type;
        }

        internal void Encode(ByteWriter Packet)
        {
            Packet.AddInt(0);
            Packet.AddInt(this.Score);

            Packet.AddInt(this.Type);

            Packet.AddString(this.Name);

            for (int i = 0; i < 5; i++)
            {
                Packet.AddInt(this.Loot.Resources[i].Count);
            }

            Packet.AddInt(0);

            Packet.AddBoolean(false);

            Packet.AddInt(0);
            Packet.AddInt(0);
            Packet.AddInt(0);

            Packet.AddString(this.Note);

            Packet.AddInt(0);
        }

        internal void SetData(int HighID, int LowID, string Name, int Type, int Score)
        {
            if (Type == 16 || this.Type != 16)
            {
                this.Name = Name;
                this.Type = Type;
                this.Score = Score;
            }
            else 
                Logging.Error(this.GetType(), "SetData() - This should never happen! You should never change the Megabase node to something else.");
        }

        internal void SetLoot(ResourceBundle Bundle)
        {
            this.Loot.CopyFrom(Bundle);
        }
    }
}