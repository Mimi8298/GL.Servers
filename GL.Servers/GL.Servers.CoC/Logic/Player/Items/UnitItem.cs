namespace GL.Servers.CoC.Logic.Items
{
    using GL.Servers.CoC.Files.CSV_Helpers;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    internal class UnitItem : Item
    {
        [JsonProperty] internal int Level;

        internal override int Checksum
        {
            get
            {
                return base.Checksum + this.Level;
            }
        }

        public UnitItem() : base()
        {
            
        }

        public UnitItem(Data Data, int Count, int Level) : base(Data, Count)
        {
            this.Level = Level;
        }

        internal override void Decode(Reader Reader)
        {
            base.Decode(Reader);
            this.Level = Reader.ReadInt32();
        }

        internal override void Encode(ByteWriter Writer)
        {
            base.Encode(Writer);
            Writer.AddInt(this.Level);
        }

        internal override JObject Save()
        {
            JObject Json = base.Save();

            Json.Add("lvl", this.Level);

            return Json;
        }

        public override bool Equals(object obj)
        {
            UnitItem Item = obj as UnitItem;

            if (Item != null)
            {
                return Item.Data == this.Data && Item.Level == this.Level;
            }

            return false;
        }
    }
}