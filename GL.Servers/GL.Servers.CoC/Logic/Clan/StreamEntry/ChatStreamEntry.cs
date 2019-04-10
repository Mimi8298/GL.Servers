namespace GL.Servers.CoC.Logic.Clan.StreamEntry
{
    using GL.Servers.CoC.Logic.Clan.Enums;
    using GL.Servers.CoC.Logic.Clan.Items;

    using GL.Servers.Extensions.List;

    using Newtonsoft.Json.Linq;

    internal class ChatStreamEntry : StreamEntry
    {
        internal string Message;

        internal override StreamType StreamType
        {
            get
            {
                return StreamType.Chat;
            }
        }

        public ChatStreamEntry() : base()
        {
            
        }

        public ChatStreamEntry(Member Member, string Message) : base(Member)
        {
            this.Message = Message;
        }

        internal override void Encode(ByteWriter Packet)
        {
            base.Encode(Packet);   
            Packet.AddString(this.Message);
        }

        internal override void Load(JToken Json)
        {
            base.Load(Json);
            this.Message = (string) Json["message"] ?? string.Empty;
        }

        internal override JObject Save()
        {
            JObject Json = base.Save();       
            Json.Add("message", this.Message);
            return Json;
        }
    }
}