namespace GL.Servers.GS.Packets.Messages.Server
{
    using GL.Servers.GS.Extensions.List;
    using GL.Servers.GS.Logic;

    internal class Avatar_Data : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Avatar_Data"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        internal Avatar_Data(Device Device) : base(Device)
        {
            this.Identifier     = 20204;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(1);            // ID
            this.Data.AddString("Berkan");  // Name
            this.Data.AddString("Berkan");  // Unknown
            return;
            this.Data.AddInt(1);            // Unknown
            this.Data.Add(1);               // Daily Reward Claimed?
            this.Data.AddInt(3);            // Unknown
            this.Data.Add(1);               // Unknown
            this.Data.AddInt(1);            // Unknown
            this.Data.AddInt(4);            // Unknown
            this.Data.AddInt(1);            // Unknown
            this.Data.AddInt(1);            // Unknown
            this.Data.AddInt(1);            // Unknown
            this.Data.AddInt(1);            // Unknown 
            this.Data.AddInt(1);            // Unknown
        }
    }
}