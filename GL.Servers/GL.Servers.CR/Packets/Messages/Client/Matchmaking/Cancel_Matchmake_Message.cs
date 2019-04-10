namespace GL.Servers.CR.Packets.Messages.Client.Matchmaking
{
    using GL.Servers.CR.Core;
    using GL.Servers.CR.Core.Network;
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.CR.Packets.Messages.Server.Home;
    using GL.Servers.CR.Packets.Messages.Server.Matchmaking;
    using GL.Servers.DataStream;
    using GL.Servers.Extensions.Binary;

    using State = GL.Servers.CR.Logic.Mode.Enums.State;

    internal class Cancel_Matchmake_Message : Message
    {
        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 14107;
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
        /// Initializes a new instance of the <see cref="Cancel_Matchmake_Message"/> class.
        /// </summary>
        public Cancel_Matchmake_Message(Device Device, ByteStream ByteStream) : base(Device, ByteStream)
        {
            // Cancel_Matchmake_Message.
        }
        
        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override void Process()
        {
            if (this.Device.GameMode.Player != null)
            {
                if (Resources.BattleManager.Waitings.TryRemove(this.Device.GameMode.Player.PlayerID, out _))
                {
                    new Cancel_Matchmake_Done_Message(this.Device).Send();
                }
            }
        }
    }
}