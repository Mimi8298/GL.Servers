namespace GL.Servers.CoC.Logic.Clan
{
    using GL.Servers.CoC.Logic.Clan.Enums;
    using GL.Servers.CoC.Logic.Clan.Items;
    using GL.Servers.CoC.Extensions.Helper;

    using GL.Servers.Extensions.List;

    using Newtonsoft.Json.Linq;

    internal class AllianceEventStreamEntry : StreamEntry.StreamEntry
    {
        internal StreamEvent Event;
        internal int ExecuterHighId;
        internal int ExecuterLowId;

        internal string ExecuterName;

        internal override StreamType StreamType
        {
            get
            {
                return StreamType.AllianceEvent;
            }
        }

        public AllianceEventStreamEntry() : base()
        {
            
        }

        public AllianceEventStreamEntry(Member Member, Member Executer, StreamEvent Event) : base(Member)
        {
            this.ExecuterHighId = Executer.HighID;
            this.ExecuterLowId  = Executer.LowID;
            this.ExecuterName   = Executer.Name;

            this.Event          = Event;
        }

        internal override void Encode(ByteWriter Packet)
        {
            base.Encode(Packet);   

            Packet.AddInt((int) this.Event);
            Packet.AddLong(this.ExecuterHighId, this.ExecuterLowId);
            Packet.AddString(this.ExecuterName);
        }

        internal override void Load(JToken Json)
        {
            base.Load(Json);

            JsonHelper.GetJsonNumber(Json, "exc_id_high", out this.ExecuterHighId);
            JsonHelper.GetJsonNumber(Json, "exc_id_low", out this.ExecuterLowId);
            JsonHelper.GetJsonString(Json, "exc_name", out this.ExecuterName);
        }

        internal override JObject Save()
        {
            JObject Json = base.Save();

            Json.Add("exc_id_high", this.ExecuterHighId);
            Json.Add("exc_id_low", this.ExecuterLowId);
            Json.Add("exc_name", this.ExecuterName);

            return Json;
        }
    }
}