namespace GL.Servers.CR.Packets.Messages.Server.Home
{
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Commands;
    using GL.Servers.CR.Logic.Commands.Manager;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.DataStream;
    using GL.Servers.Extensions.List;

    internal class Available_Server_Command_Message : Message
    {
        private Command Command;

        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 24111;
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
        /// Initializes a new instance of the <see cref="Available_Server_Command_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Command">The command.</param>
        public Available_Server_Command_Message(Device Device, Command Command) : base(Device)
        {
            this.Command    = Command;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            CommandManager.EncodeCommand(this.Command, this.Data);
        }
    }
}