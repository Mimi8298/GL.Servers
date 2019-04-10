namespace GL.Clients.BS.Packets.Messages.Server
{
    using GL.Clients.BS.Core;
    using GL.Clients.BS.Logic;

    using GL.Servers.Extensions.Binary;
    using GL.Servers.Logic.Enums;

    internal class Own_Home_Data : Message
    {
        private Player Player;

        /// <summary>
        /// Initializes a new instance of the <see cref="Own_Home_Data"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Own_Home_Data(Device Device, Reader Reader) : base(Device, Reader)
        {
            this.Device.State   = State.LOGGED;
            this.Player         = this.Device.Player;
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();

            this.Player.Trophies    = this.Reader.ReadVInt();
            this.Player.Trophies    = this.Reader.ReadVInt();

            this.Player.Experience  = this.Reader.ReadVInt();

            this.Reader.ReadVInt();

            this.Player.Thumbnail   = this.Reader.ReadVInt() * 1000000;
            this.Player.Thumbnail  += this.Reader.ReadVInt();

            for (int i = 0; i < this.Reader.ReadVInt(); i++)
            {
                this.Reader.ReadVInt();
            }


            this.Reader.ReadVInt();
            this.Reader.ReadVInt();

            this.Reader.ReadBoolean();

            this.Reader.ReadVInt(); // Gold Reward

            this.Player.JoystickMode = this.Reader.ReadVInt();
            this.Player.HintsEnabled = this.Reader.ReadBoolean();

            this.Reader.ReadVInt();
            this.Reader.ReadVInt();

            this.Reader.ReadBoolean();

            {
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();

                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
            }

            {
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();

                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
            }

            this.Reader.ReadVInt();
            this.Reader.ReadVInt();

            this.Reader.ReadVInt();

            this.Reader.ReadBoolean();
            this.Reader.ReadBoolean();

            // Objects

            this.Reader.ReadVInt();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            return;

            Logging.Info(this.GetType(), "Experience : " + this.Player.Experience);
            Logging.Info(this.GetType(), "Thumbnail  : " + this.Player.Thumbnail);
            Logging.Info(this.GetType(), "Trophies   : " + this.Player.Trophies);
        }
    }
}