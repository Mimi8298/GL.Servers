namespace GL.Servers.CR.Logic.Stream
{
    using System;
    using GL.Servers.CR.Extensions.Helper;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.DataStream;

    internal class StreamEntry
    {
        internal int HighID;
        internal int LowID;

        internal int SenderHighID;
        internal int SenderLowID;
        internal int SenderExpLevel;
        internal string SenderName;
        internal Role SenderRole;

        internal bool Removed;

        internal DateTime CreationDateTime;

        /// <summary>
        /// Gets age in seconds.
        /// </summary>
        internal int AgeSeconds
        {
            get
            {
                return (int) DateTime.UtcNow.Subtract(this.CreationDateTime).TotalSeconds;
            }
        }

        /// <summary>
        /// Gets the stream id.
        /// </summary>
        internal long StreamID
        {
            get
            {
                return this.SenderHighID << 32 | (uint) this.SenderLowID;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamEntry"/> class.
        /// </summary>
        internal StreamEntry()
        {
            this.CreationDateTime = DateTime.UtcNow;
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal void Encode(ByteStream Packet)
        {
            Packet.AddLogicLong(this.HighID, this.LowID);
            Packet.AddLogicLong(this.SenderHighID, this.SenderLowID);
            Packet.AddLogicLong(this.SenderHighID, this.SenderLowID); // HomeID
            Packet.AddString(this.SenderName);
            Packet.AddVInt(this.SenderExpLevel);
            Packet.AddVInt((int) this.SenderRole);
            Packet.AddVInt(this.AgeSeconds);
        }
    }
}