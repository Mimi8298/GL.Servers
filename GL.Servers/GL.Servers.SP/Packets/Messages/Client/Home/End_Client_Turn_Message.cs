namespace GL.Servers.SP.Packets.Messages.Client.Home
{
    using System.Collections.Generic;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.SP.Core;
    using GL.Servers.SP.Core.Network;
    using GL.Servers.SP.Logic;
    using GL.Servers.SP.Logic.Manager;
    using GL.Servers.SP.Packets.Enums;
    using GL.Servers.SP.Packets.Messages.Server.Home;

    internal class End_Client_Turn_Message : Message
    {
        internal int SubTick;
        internal int Checksum;

        internal List<Command> Commands;

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
            // End_Client_Turn_Message.
        }

        internal override void Decode()
        {
            this.SubTick = this.Reader.ReadInt32();
            this.Checksum = this.Reader.ReadInt32();

            int Count = this.Reader.ReadInt32();

            if (Count <= 512)
            {
                if (Count > 0)
                {
                    this.Commands = new List<Command>(Count);

                    for (int i = 0; i < Count; i++)
                    {
                        Command Command = CommandManager.DecodeCommand(this.Reader);

                        if (Command != null)
                        {
                            this.Commands.Add(Command);
                        }
                        else
                            break;
                    }
                }
            }
        }

        internal override void Process()
        {
            if (this.Commands != null)
            {
                while (this.Commands.Count != 0)
                {
                    Command Command = this.Commands[0];

                    if (Command.ExecuteSubTick > this.Device.GameMode.Time.ClientSubTick)
                    {
                        if (Command.IsServerCommand)
                        {
                            ServerCommand ServerCommand = (ServerCommand) Command;

                            if (ServerCommand.Id != -1)
                            {
                                if (this.Device.GameMode.CommandManager.ServerCommands.TryGetValue(ServerCommand.Id, out ServerCommand Original))
                                {
                                    if (Original.Checksum != ServerCommand.Checksum)
                                    {
                                        goto Remove;
                                    }
                                }
                                else
                                    goto Remove;
                            }
                            else
                                goto Remove;
                        }

                        this.Device.GameMode.Time.ClientSubTick = Command.ExecuteSubTick;
                        this.Device.GameMode.Tick();

                        Command.Execute(this.Device.GameMode);
                    }
                    else
                    {
                        Logging.Error(this.GetType(), "Execute command failed! Command should be already executed. (client_subtick=" + Command.ExecuteSubTick + " server_subtick=" + this.Device.GameMode.Time.ClientSubTick + ")");
                    }

                    Remove:

                    this.Commands.RemoveAt(0);
                }
            }

            this.Device.GameMode.Time.ClientSubTick = this.SubTick;
            this.Device.GameMode.Tick();
            
            if (this.Device.GameMode.Checksum != this.Checksum)
            {
                Logging.Error(this.GetType(), "Client - Server are Out Of Sync! (client_checksum:" + this.Checksum + " server_checksum:" + this.Device.GameMode.Checksum + ")");
                new Out_Of_Sync_Message(this.Device).Send();
            }
        }
    }
}