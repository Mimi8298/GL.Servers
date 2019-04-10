namespace GL.Servers.BS.Packets.Messages.Server
{
    using GL.Servers.BS.Logic;
    using GL.Servers.Extensions.List;

    internal class Bind_Facebook_Account_OK : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bind_Facebook_Account_OK"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        internal Bind_Facebook_Account_OK(Device Device) : base(Device)
        {
            this.Identifier = 24201;
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