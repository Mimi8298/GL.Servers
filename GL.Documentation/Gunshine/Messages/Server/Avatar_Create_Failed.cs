namespace GL.Servers.GS.Packets.Messages.Server
{
    using GL.Servers.GS.Extensions.List;
    using GL.Servers.GS.Logic;

    internal class Avatar_Create_Failed : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Avatar_Create_Failed"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        internal Avatar_Create_Failed(Device Device) : base(Device)
        {
            this.Identifier     = 20202;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(0);
        }
    }
}