namespace GL.Servers.BS.Packets.Messages.Client
{
    using GL.Servers.BS.Core;
    using GL.Servers.BS.Core.Network;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Packets.Messages.Server;

    using GL.Servers.Extensions.Binary;

    internal class Ask_Profile : Message
    {
        private int HighID;
        private int LowID;

        /// <summary>
        /// Initializes a new instance of the <see cref="Ask_Profile"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Ask_Profile(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Ask_Profile.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.HighID = this.Reader.ReadInt32();
            this.LowID  = this.Reader.ReadInt32();
        }

        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override void Process()
        {
            Player Player = Resources.Players.Get(null, this.HighID, this.LowID, Constants.Database, false);

            if (Player != null)
            {
                new Profile_Data(this.Device, Player).Send();
            }
            else
            {
                Logging.Error(this.GetType(), this.Device, "Error when asking a player profile, player was not found.");
            }
        }
    }
}