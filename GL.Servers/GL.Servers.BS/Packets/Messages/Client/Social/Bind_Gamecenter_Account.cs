namespace GL.Servers.BS.Packets.Messages.Client
{
    using GL.Servers.BS.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Bind_Gamecenter_Account : Message
    {
        private int Unknown;

        private string GamecenterID;
        private string Certificate;
        private string AppBundle;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bind_Gamecenter_Account"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Bind_Gamecenter_Account(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Bind_Gamecenter_Account.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Unknown        = this.Reader.ReadVInt();

            this.GamecenterID   = this.Reader.ReadString();
            this.Certificate    = this.Reader.ReadString();
            this.AppBundle      = this.Reader.ReadString();
        }

        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override void Process()
        {
            return;

            this.Device.Player.Gamecenter.Identifier    = this.GamecenterID;
            this.Device.Player.Gamecenter.Certificate   = this.Certificate;
            this.Device.Player.Gamecenter.AppBundle     = this.AppBundle;
        }
    }
}