namespace GL.Servers.BB.Logic.Manager
{
    using System.Collections.Generic;
    using GL.Servers.BB.Core;
    using GL.Servers.BB.Logic.Enums;
    using GL.Servers.BB.Packets;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;

    internal class CommandManager
    {
        internal Level Level;

        internal List<Command> Commands;
        internal Dictionary<int, Command> ServerCommands;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandManager"/> class.
        /// </summary>
        public CommandManager(Level Level)
        {
            this.Level = Level;
        }

        /// <summary>
        /// Creates and return the specified command.
        /// </summary>
        /// <param name="Type">The command type.</param>
        /// <returns>The command.</returns>
        internal static Command CreateCommand(int Type)
        {
            Command Command = Factory.CreateCommand(Type);

            if (Command == null)
            {
                Logging.Error(typeof(CommandManager), "CreateCommand() - Unknown command type: " + Type);
            }

            return Command;
        }

        /// <summary>
        /// Creates and return the decoded command.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>The command.</returns>
        internal static Command DecodeCommand(Reader reader)
        {
            Command Command = CommandManager.CreateCommand(reader.ReadInt32());

            if (Command != null)
            {
                Command.Decode(reader);
            }

            return Command;
        }

        /// <summary>
        /// Encodes the command.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static void EncodeCommand(Command command, ByteWriter writer)
        {
            writer.AddInt(command.Type);
            command.Encode(writer);
        }

        /// <summary>
        /// Verifies that the command is allowed in the current state.
        /// </summary>
        /// <param name="Command">The command</param>
        /// <returns></returns>
        internal bool IsCommandAllowedInCurrentState(Command command)
        {
            if (command.IsDebugCommand)
            {
                Logging.Error(typeof(CommandManager), "Execute command failed! Debug commands are not allowed when debug is off. (type=" + command.Type + ")");
                return false;
            }

            if (this.Level.GameMode.State == State.Home && !command.AllowInHomeState)
            {
                Logging.Error(typeof(CommandManager), "Execute command failed! Command is not allowed in home state. (type=" + command.Type + ")");
                return false;
            }

            if (this.Level.GameMode.State == State.Visit && !command.AllowInVisitState)
            {
                Logging.Error(typeof(CommandManager), "Execute command failed! Command is not allowed in visit state. (type=" + command.Type + ")");
                return false;
            }

            if ((this.Level.GameMode.State == State.Attack || this.Level.GameMode.State == State.Defend) && !command.AllowInAttackState)
            {
                Logging.Error(typeof(CommandManager), "Execute command failed! Command is not allowed in attack state. (type=" + command.Type + ")");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Executes a queue of commands.
        /// </summary>
        /// <param name="Commands">The queue.</param>
        internal void ExecuteCommands(Queue<Command> Commands)
        {
            if (Commands != null)
            {
                for (int i = Commands.Count - 1; i >= 0; i--)
                {
                    Command Command = Commands.Dequeue();

                    if (Command.ExecuteSubTick >= this.Level.GameMode.Time.SubTick)
                    {
                        if (this.IsCommandAllowedInCurrentState(Command))
                        {
                            this.Level.GameMode.UpdateSubTick(Command.ExecuteSubTick);
                            Command.Execute(this.Level);
                        }
                        else
                            Logging.Error(this.GetType(), "Execute command failed! Command not allowed in current state. (type=" + Command.Type + " current_state=" + (int) this.Level.GameMode.State + ")");
                    }
                    else
                        Logging.Error(this.GetType(), "Execute command failed! Command should have been executed already. (type=" + Command.Type + " server_tick=" + this.Level.GameMode.Time.SubTick + " command_tick=" + Command.ExecuteSubTick + ")");
                }
            }
        }
    }
}