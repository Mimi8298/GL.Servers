namespace GL.Servers.BB.Packets.Messages.Server.Player
{
    using GL.Servers.BB.Logic;
    using GL.Servers.BB.Logic.Manager;

    using GL.Servers.Extensions.List;

    internal class Available_Server_Command_Message : Message
    {
        private readonly Command Command;

        internal override short Type
        {
            get
            {
                return 24111;
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