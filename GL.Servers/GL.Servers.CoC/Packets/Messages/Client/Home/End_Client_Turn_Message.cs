namespace GL.Servers.CoC.Packets.Messages.Client.Home
{
    using System.Collections.Generic;

    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic.Manager;
    using GL.Servers.CoC.Packets.Enums;

    using GL.Servers.Extensions.Binary;

    internal class End_Client_Turn_Message : Message
    {
        internal int SubTick;
        internal int Checksum;
        internal int CommandCount;

        internal List<Command> Commands;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 14102;
            }
        }

        /// <summary>
        /// Gets a value indicating the server node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Home;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="End_Client_Turn_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public End_Client_Turn_Message(Device Device, Reader Reader) : base(Device, Reader)
        {
            // EndClientTurnMessage.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.SubTick      = this.Reader.ReadInt32();
            this.Checksum     = this.Reader.ReadInt32();
            this.CommandCount = this.Reader.ReadInt32();

            if (this.CommandCount <= 512)
            {
                if (this.CommandCount > 0)
                {
                    this.Commands = new List<Command>(this.CommandCount);

                    for (int i = 0; i < this.CommandCount; i++)
                    {
                        Command Command = CommandManager.DecodeCommand(this.Reader);

                        if (Command != null)
                        {
                            this.Commands.Add(Command);
                        }
                        else
                        {
                            this.CommandCount = this.Commands.Count;
                            break;
                        }
                    }
                }
            }
            else
                Logging.Error(this.GetType(), "Command count is too high! (" + this.CommandCount + ")");
        }

        internal override void Process()
        {
            if (this.CommandCount > 0)
            {
                do
                {
                    Command Command = this.Commands[0];

                    if (this.Device.GameMode.CommandManager.IsCommandAllowedInCurrentState(Command))
                    {
                        if (Command.IsServerCommand)
                        {
                            ServerCommand ServerCommand = (ServerCommand) Command;

                            if (this.Device.GameMode.CommandManager.ServerCommands.TryGetValue(ServerCommand.Id, out ServerCommand OriginalCommand))
                            {
                                if (OriginalCommand.Checksum != ServerCommand.Checksum)
                                {
                                    return;
                                }

                                this.Device.GameMode.CommandManager.ServerCommands.Remove(ServerCommand.Id);
                            }
                            else
                            {
                                Logging.Error(this.GetType(), this.Device, "Execute command failed! Command " + Command.Type + " is not available.");
                                return;
                            }
                        }

                        if (Command.ExecuteSubTick <= this.SubTick)
                        {
                            if (this.Device.GameMode.Time.SubTick <= Command.ExecuteSubTick)
                            {
                                this.Device.GameMode.Time.SubTick = Command.ExecuteSubTick;
                                this.Device.GameMode.Level.Tick();

                                this.Commands.Remove(Command);

                                Command.Execute(this.Device.GameMode.Level);

                                continue;
                            }

                            Logging.Error(this.GetType(), this.Device, "Execute command failed! Command should have been executed already. (type=" + Command.Type + ", command_tick=" + Command.ExecuteSubTick + ", server_tick=" + this.Device.GameMode.Time.SubTick + ")");
                        }
                        else Logging.Error(this.GetType(), this.Device, "Execute command failed! Command should have been executed already. (type=" + Command.Type + ", command_tick=" + Command.ExecuteSubTick + ", server_tick=" + this.SubTick + ")");
                    }

                    this.Commands.Clear();

                } while (this.Commands.Count > 0);
            }

            this.Device.GameMode.Time.SubTick = this.SubTick;
            this.Device.GameMode.Level.Tick();

            // Logging.Info(this.GetType(), "Client Time : MS:" + this.Device.Time.TotalMS + "  SECS:" + this.Device.Time.TotalSecs + ".");
        }
    }
}