namespace GL.Servers.CR.Packets.Messages.Server.Home
{
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.Extensions.List;

    internal class Battle_Report_Stream_Message : Message
    {
        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 24413;
            }
        }

        /// <summary>
        /// Gets the service node of this message.
        /// </summary>
        internal override Node ServiceNode
        {
            get
            {
                return Node.Home;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Battle_Report_Stream_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Player">The player.</param>
        public Battle_Report_Stream_Message(Device Device) : base(Device)
        {
            
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddLong(this.Device.GameMode.Player.PlayerID);
            this.Data.AddVInt(0); // Count
        }
    }
}