namespace GL.Servers.BS.Logic.Slots.Items
{
    using System.Collections.Generic;
    using GL.Servers.BS.Extensions;
    using GL.Servers.BS.Files.CSV_Logic;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;

    internal class BrawlBox
    {
        internal Cards CardData;
        internal Resources ResourceData;

        internal int BackgroundId;
        internal int Count;

        internal void Decode(Reader Reader)
        {
            this.BackgroundId = Reader.ReadVInt();
            this.ResourceData = Reader.ReadData<Resources>();
            this.Count        = Reader.ReadVInt();
            this.CardData     = Reader.ReadData<Cards>();
        }

        internal void Encode(List<byte> Packet)
        {
            Packet.AddVInt(this.BackgroundId);
            Packet.AddData(this.ResourceData);
            Packet.AddVInt(this.Count);
            Packet.AddData(this.CardData);
        }
    }
}