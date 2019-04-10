namespace GL.Servers.BS.Packets.Messages.Server
{
    using GL.Servers.BS.Logic;
    using GL.Servers.Extensions.List;

    internal class Change_Name_Failed : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Change_Name_Failed"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Change_Name_Failed(Device Device) : base (Device)
        {
            this.Identifier = 20205;
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
