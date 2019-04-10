namespace GL.Servers.BB.Packets.Messages.Server.Base
{
    using GL.Servers.BB.Logic;
    using GL.Servers.BB.Logic.Enums;

    using GL.Servers.Extensions.List;

    internal class Server_Hello_Message : Message
    {
        internal override short Type
        {
            get
            {
                return 20100;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Server_Hello_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Server_Hello_Message(Device Device) : base (Device)
        {
            this.Device.State   = DeviceState.Login;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddBytes(this.Device.Crypto.SessionKey);
        }
    }
}
