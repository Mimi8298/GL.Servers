namespace GL.Servers.BS.Packets.Messages.Client
{
    using GL.Servers.BS.Core;
    using GL.Servers.BS.Core.Network;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Packets.Messages.Server;

    using GL.Servers.Extensions.Binary;

    internal class Clan_Ask : Message
    {
        private int HighID;
        private int LowID;

        /// <summary>
        /// Initializes a new instance of the <see cref="Clan_Ask"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Clan_Ask(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Clan_Ask.
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
            Logging.Info(this.GetType(), "Requested profile of " + this.HighID + "-" + this.LowID + ".");

            Clan Clan = Resources.Clans.Get(this.HighID, this.LowID, Constants.Database, false);

            if (Clan != null)
            {
                new Clan_Data(this.Device, Clan).Send();
            }
            else
            {
                Logging.Error(this.GetType(), this.Device, "Error when asking a clan profile, clan was not found.");
            }
        }
    }
}