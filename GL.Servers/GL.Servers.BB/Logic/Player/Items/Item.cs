namespace GL.Servers.BB.Logic.Items
{
    using GL.Servers.BB.Extensions.Helper;
    using GL.Servers.BB.Files;
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Extensions.List;
    using Newtonsoft.Json;

    internal class Item
    {
        internal Data Data;
        [JsonProperty] internal int Count;

        [JsonProperty]
        internal int Id
        {
            get
            {
                return this.Data != null ? this.Data.GlobalID : 0;
            }
            set
            {
                if (value != 0)
                {
                    this.Data = CSV.Tables.GetDataById(value);
                }
            }
        }

        /// <summary>
        /// Returns the checksum of the <see cref="Item"/>.
        /// </summary>
        internal virtual int Checksum
        {
            get
            {
                return this.Id + this.Count;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item()
        {
            // Item
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item(Data Data, int Count)
        {
            this.Data = Data;
            this.Count = Count;
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal void Encode(ByteWriter Packet)
        {
            Packet.AddData(this.Data);
            Packet.AddInt(this.Count);
        }

        public override bool Equals(object obj)
        {
            Item Item = obj as Item;

            if (Item != null)
            {
                return this.Data == Item.Data;
            }

            return false;
        }
    }
}