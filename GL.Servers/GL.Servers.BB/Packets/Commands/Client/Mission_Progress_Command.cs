namespace GL.Servers.BB.Packets.Commands.Client
{
    using GL.Servers.BB.Extensions.Helper;
    using GL.Servers.BB.Files.CSV_Logic;
    using GL.Servers.BB.Logic;
    using GL.Servers.Extensions.List;
    using GL.Servers.Extensions.Binary;

    internal class Mission_Progress_Command : Command
    {
        internal MissionData Mission;

        /// <summary>
        /// Gets the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 595;
            }
        }

        /// <summary>
        /// Gets a value indicate whether the command is allowed in attack state.
        /// </summary>
        internal override bool AllowInAttackState
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicate whether the command is allowed in home state.
        /// </summary>
        internal override bool AllowInHomeState
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicate whether the command is allowed in visit state.
        /// </summary>
        internal override bool AllowInVisitState
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicate whether the command is a debug command.
        /// </summary>
        internal override bool IsDebugCommand
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicate whether the command is a server command.
        /// </summary>
        internal override bool IsServerCommand
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mission_Progress_Command"/> class.
        /// </summary>
        public Mission_Progress_Command() : base()
        {
            // Mission_Progress_Command.
        }

        /// <summary>
        /// Decodes this command.
        /// </summary>
        /// <param name="reader"></param>
        internal override void Decode(Reader reader)
        {
            this.Mission = reader.ReadData<MissionData>();
            base.Decode(reader);
        }

        /// <summary>
        /// Encodes this command.
        /// </summary>
        /// <param name="reader"></param>
        internal override void Encode(ByteWriter writer)
        {
            writer.AddData(this.Mission);
            base.Encode(writer);
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="Level">The level.</param>
        internal override void Execute(Level Level)
        {
            if (this.Mission != null)
            {
                if (Level.MissionManager.Mission != null)
                {
                    Level.MissionManager.Mission.StateChangeConfirmed();
                }
            }
        }
    }
}