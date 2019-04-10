namespace GL.Servers.CoC.Logic.Manager
{
    using System.Threading;
    using System.Collections.Generic;

    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Packets;
    using GL.Servers.CoC.Logic.Mode.Enums;

    using GL.Servers.Extensions.List;
    using GL.Servers.Extensions.Binary;

    internal class CommandManager
    {
        internal bool ChangeNameOnGoing;
        internal int NextServerCommandId;

        internal Level Level;
        internal Dictionary<int, ServerCommand> ServerCommands;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandManager"/> class.
        /// </summary>
        public CommandManager(Level Level)
        {
            this.Level          = Level;
            this.ServerCommands = new Dictionary<int, ServerCommand>(512);
        }

        /// <summary>
        /// Adds a server command.
        /// </summary>
        internal void AddCommand(ServerCommand Command, bool OnClient = false)
        {
            if (Command.IsServerCommand && !OnClient)
            {
                this.ServerCommands.Add(Command.Id = Interlocked.Increment(ref this.NextServerCommandId), Command);
            }
        }

        /// <summary>
        /// Creates and decodes a command.
        /// </summary>
        internal static Command DecodeCommand(Reader Reader)
        {
            Command Command = Factory.CreateCommand(Reader.ReadInt32());

            if (Command != null)
            {
                Command.Decode(Reader);
            }

            return Command;
        }

        /// <summary>
        /// Encodes a command.
        /// </summary>
        internal static void EncodeCommand(Command Command, ByteWriter Packet)
        {
            Packet.AddInt(Command.Type);
            Command.Encode(Packet);
        }

        /// <summary>
        /// Gets a value indicating whether the command is allowed in current state.
        /// </summary>
        internal bool IsCommandAllowedInCurrentState(Command Command)
        {
            int Type = Command.Type;

            if (Version.IsProd)
            {
                if (Type >= 1000)
                {
                    Logging.Error(this.GetType(), "Execute command failed! Debug commands are not allowed when debug is off.");
                    return false;
                }
            }

            switch (this.Level.GameMode.State)
            {
                case State.Home:
                {
                    if (Type < 500 || Type >= 700)
                    {
                        return false;
                    }

                    break;
                }

                case State.Attack:
                case State.Defend:
                {
                    if (Type < 700)
                    {
                        return false;
                    }

                    break;
                }
            }

            return true;
        }
    }
}