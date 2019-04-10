namespace GL.Servers.CR.Packets.Messages.Client.Sector
{
    using GL.Servers.CR.Logic;
    using GL.Servers.DataStream;

    internal class Request_Sector_State_Message : Message
    {
        internal int ClientTick;

        /// <summary>
        /// Initializes a new instance of the <see cref="Request_Sector_State_Message"/> class.
        /// </summary>
        public Request_Sector_State_Message(Device Device, ByteStream Packet) : base(Device, Packet)
        {
            // Request_Sector_State_Message   
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode()
        {
            this.ClientTick = this.Data.ReadVInt();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddVInt(this.ClientTick);
        }
    }
}