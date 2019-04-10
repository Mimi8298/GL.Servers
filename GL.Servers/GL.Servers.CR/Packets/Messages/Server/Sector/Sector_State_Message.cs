namespace GL.Servers.CR.Packets.Messages.Server.Sector
{
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.CR.Logic.Mode;

    using GL.Servers.DataStream;

    internal class Sector_State_Message : Message
    {
        internal GameMode GameMode;

        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 21903;
            }
        }

        /// <summary>
        /// Gets the service node of this message.
        /// </summary>
        internal override Node ServiceNode
        {
            get
            {
                return Node.Sector;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sector_State_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Sector_State_Message(Device Device, GameMode GameMode) : base(Device)
        {
            this.GameMode = GameMode;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddBoolean(false); // Compressed

            this.Data.AddVInt(0x2A);

            this.Data.AddVInt(this.GameMode.Battle.Players.Count); // Player Count

            this.GameMode.Battle.Players.ForEach(Player =>
            {
                Player.Encode(this.Data, true);
                this.Data.AddBoolean(Player.PlayerID == this.Device.MessageManager.AccountID);
            });

            this.Data.AddVInt(0x2B);

            this.GameMode.Encode(new ChecksumEncoder(this.Data));
        }
    }
}