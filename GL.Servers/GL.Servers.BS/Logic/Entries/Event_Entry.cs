namespace GL.Servers.BS.Logic.Entries
{
    using System;
    using System.Collections.Generic;

    using GL.Servers.BS.Core;
    using GL.Servers.BS.Logic.Enums;
    using GL.Servers.BS.Logic.Slots.Items;

    using GL.Servers.Extensions.List;

    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    internal class Event_Entry : IEntry
    {
        /// <summary>
        /// Gets the message identifier.
        /// </summary>
        public long MessageID
        {
            get
            {
                return (long) this.Message_HighID << 32 | (long)(uint) this.Message_LowID;
            }
        }

        /// <summary>
        /// Gets the player identifier.
        /// </summary>
        public long PlayerID
        {
            get
            {
                return (long) this.Sender_HighID << 32 | (long)(uint) this.Sender_LowID;
            }
        }

        [JsonProperty("high_id")]           public int Message_HighID   { get; set; }
        [JsonProperty("low_id")]            public int Message_LowID    { get; set; }

        [JsonProperty("sender_high_id")]    public int Sender_HighID    { get; set; }
        [JsonProperty("sender_low_id")]     public int Sender_LowID     { get; set; }

        [JsonProperty("status")]            public int Status           { get; set; }

        [JsonProperty("sender_name")]       public string Sender_Name   { get; set; }
        [JsonProperty("message")]           public string Message       { get; set; }

        [JsonProperty("sender_role")]       public Role Sender_Role     { get; set; }

        [JsonProperty("date")]              public DateTime Sent        { get; set; }

        /// <summary>
        /// Gets this instance type.
        /// </summary>
        public Stream_Type Type
        {
            get
            {
                return Stream_Type.EVENT;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Event_Entry"/> class.
        /// </summary>
        internal Event_Entry()
        {
            this.Sent = DateTime.UtcNow;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Event_Entry"/> class.
        /// </summary>
        /// <param name="Member">The member.</param>
        internal Event_Entry(Member Member) : this()
        {
            if (Member != null)
            {
                this.Sender_HighID  = Member.HighID;
                this.Sender_LowID   = Member.LowID;
                this.Sender_Name    = Member.Username;
                this.Sender_Role    = Member.Role;
            }
            else
            {
                Logging.Error(this.GetType(), "Member was null at Chat_Entry(Member).");
            }
        }

        public byte[] ToBytes
        {
            get
            {
                List<byte> Packet = new List<byte>();

                Packet.AddVInt((int) this.Type);

                Packet.AddVInt(this.Message_HighID);
                Packet.AddVInt(this.Message_LowID);

                Packet.AddVInt(this.Sender_HighID);
                Packet.AddVInt(this.Sender_LowID);

                Packet.AddString(this.Sender_Name);

                Packet.AddVInt((int) this.Sender_Role);

                Packet.AddVInt((int) DateTime.UtcNow.Subtract(this.Sent).TotalSeconds);
                Packet.AddVInt(0);

                // Entry

                Packet.AddVInt(this.Status);
                Packet.AddVInt(0);

                return Packet.ToArray();
            }
        }
    }
}