namespace GL.Servers.BS.Packets.Messages.Client
{
    using GL.Servers.BS.Core.Network;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Packets.Messages.Server;
    using GL.Servers.Extensions.Binary;

    internal class Unbind_Facebook_Account : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Unbind_Facebook_Account"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Unbind_Facebook_Account(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Unbind_Facebook_Account.
        }

        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override void Process()
        {
            if (this.Device.Player.Facebook.Connected)
            {
                this.Device.Player.Facebook.FBClient    = null;
                this.Device.Player.Facebook.Identifier  = null;
                this.Device.Player.Facebook.Token       = null;

                if (!this.Device.Player.Facebook.Connected)
                {
                    new Unbind_Facebook_Account_OK(this.Device).Send();
                }
            }
            else
            {
                new Unbind_Facebook_Account_OK(this.Device).Send();
            }
        }
    }
}