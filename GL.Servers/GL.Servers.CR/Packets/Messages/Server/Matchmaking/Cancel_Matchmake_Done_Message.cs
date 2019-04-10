namespace GL.Servers.CR.Packets.Messages.Server.Matchmaking
{
    using System.Text;

    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.CR.Packets.Messages.Server.Home;

    using GL.Servers.Extensions.List;

    internal class Cancel_Matchmake_Done_Message : Message
    {
        private string Message;
        private StringBuilder Reason;

        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 24125;
            }
        }

        /// <summary>
        /// Gets the service node of this message.
        /// </summary>
        internal override Node ServiceNode
        {
            get
            {
                return Node.Matchmaking;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cancel_Matchmake_Done_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Message">The message.</param>
        /// <param name="Initial">if set to <c>true</c> [initial].</param>
        public Cancel_Matchmake_Done_Message(Device Device) : base(Device)
        {
            // Cancel_Matchmaking_Done_Message.
        }
    }
}