namespace GL.Servers.CR.Logic.Commands.Manager
{
    using System.Collections.Generic;

    using GL.Servers.CR.Core;
    using GL.Servers.CR.Core.Network;
    using GL.Servers.CR.Logic.Commands;
    using GL.Servers.CR.Logic.Commands.Server;
    using GL.Servers.CR.Logic.Mode;
    using GL.Servers.CR.Logic.Mode.Enums;
    using GL.Servers.CR.Packets.Messages.Server.Home;

    using GL.Servers.DataStream;

    public class CommandManager
    {
        internal int Seed;
        internal GameMode GameMode;
        internal List<Command> Commands;
        internal Dictionary<int, Command> AvailableServerCommands;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandManager"/> class.
        /// </summary>
        internal CommandManager(GameMode GameMode)
        {
            this.GameMode = GameMode;
            this.Commands = new List<Command>(64);
            this.AvailableServerCommands = new Dictionary<int, Command>(8);
        }

        /// <summary>
        /// Adds a available server command.
        /// </summary>
        internal void AddCommand(Command Command)
        {
            if (Command.IsServerCommand)
            {
                ServerCommand ACommand = (ServerCommand) Command;

                if (this.GameMode.Connected)
                {
                    this.AvailableServerCommands.Add(ACommand.Id = ++this.Seed, ACommand);
                    new Available_Server_Command_Message(this.GameMode.Device, ACommand).Send();
                }
                else
                    Command.Execute(this.GameMode);
            }
            else
                Logging.Info(this.GetType(), "AddCommand() - Command is not a server command.");
        }

        /// <summary>
        /// Gets if the specified command is allowed in current state.
        /// </summary>
        internal bool IsCommandAllowedInCurrentState(Command Command)
        {
            if (Command.Type < 1000)
            {
                if (Command.Type >= 500 && Command.Type < 600)
                {
                    if (this.GameMode.State != State.Home)
                    {
                        Logging.Error(this.GetType(), this.GameMode.Device, "Execute command failed! Command is only allowed in home state. Command: " + Command.Type);
                    }
                    else if (this.GameMode.State == State.Visit)
                    {
                        Logging.Error(this.GetType(), this.GameMode.Device, "Execute command failed! Command is only allowed in visit state. Command: " + Command.Type);
                    }
                }
            }
            else
                Logging.Error(this.GetType(), this.GameMode.Device, "Execute command failed! Debug commands are not allowed when debug is off.");

            return true;
        }

        /// <summary>
        /// Creates the specified command.
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        internal static Command CreateCommand(int Type)
        {
            if (Type == 1000)
            {
                Logging.Error(typeof(CommandManager), "CreateCommand() - Debug command is not allowed when debug is off.");
            }
            
            if (Type < 300)
            {
                if (Type >= 200)
                {
                    switch (Type)
                    {
                        case 201:
                            return new ChangeAvatarNameCommand();
                        case 202:
                            return new DiamondsAddedCommand();
                        case 208:
                            return new AllianceUnitReceivedCommand();
                        case 210:
                            return new ClaimRewardCommand();
                        case 215:
                            return new TransactionsRevokedCommand();
                    }
                }

                switch (Type)
                {
                    case 1:
                        return new DoSpellCommand();
                }
            }

            switch (Type)
            {
                case 500:
                    return new SwapSpellsCommand();
                case 501:
                    return new SelectDeckCommand();
                case 504:
                    return new FuseSpellsCommand();
                case 508:
                    return new UpdateLastShownLevelUpCommand();
                case 525:
                    return new StartMatchmakingCommand();
                case 527:
                    return new PageOpenedCommand();
            }

            Logging.Info(typeof(CommandManager), "CreateCommand() - Command type " + Type + " does not exist.");

            return null;
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal void Encode(ChecksumEncoder Encoder)
        {
            Encoder.EnableCheckSum(false);

            Encoder.AddVInt(this.Commands.Count);
            this.Commands.ForEach(Command =>
            {
                Command.Encode(Encoder);
            });

            Encoder.EnableCheckSum(true);
        }

        /// <summary>
        /// Decodes a command.
        /// </summary>
        internal static Command DecodeCommand(ByteStream Stream)
        {
            Command Command = CommandManager.CreateCommand(Stream.ReadVInt());

            if (Command != null)
            {
                Command.Decode(Stream);
            }

            return Command;
        }

        /// <summary>
        /// Encodes the specified command.
        /// </summary>
        internal static void EncodeCommand(Command Command, ChecksumEncoder Encoder)
        {
            Encoder.AddVInt(Command.Type);
            Command.Encode(Encoder);
        }
    }
}