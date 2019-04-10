namespace GL.Servers.CR.Packets.Messages.Client.Home
{
    using System.Collections.Generic;

    using GL.Servers.CR.Core;
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Commands;
    using GL.Servers.CR.Logic.Commands.Manager;
    using GL.Servers.CR.Logic.Commands.Server;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.DataStream;
    using GL.Servers.Logic.Enums;
    using GL.Servers.Extensions.Binary;

    internal class End_Client_Turn_Message : Message
    {
        private int Tick;
        private int Checksum;
        private int Count;

        private List<Command> Commands;

        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 14102;
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
        /// Initializes a new instance of the <see cref="End_Client_Turn_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public End_Client_Turn_Message(Device Device, ByteStream ByteStream) : base(Device, ByteStream)
        {
            // End_Client_Turn_Message.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Tick       = this.Data.ReadVInt();
            this.Checksum   = this.Data.ReadVInt();
            this.Count      = this.Data.ReadVInt();

            if (this.Count <= 512)
            {
                if (this.Count > 0)
                {
                    this.Commands = new List<Command>(this.Count);

                    for (int i = 0; i < this.Count; i++)
                    {
                        Command Command = CommandManager.DecodeCommand(this.Data);

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

        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override void Process()
        {
            if (this.Device.GameMode.Time.SubTick <= this.Tick)
            {
                if (this.Commands != null)
                {
                    while (this.Commands.Count > 0)
                    {
                        Command Command = this.Commands[0];

                        if (this.Device.GameMode.CommandManager.IsCommandAllowedInCurrentState(Command))
                        {
                            if (Command.ExecuteTick >= this.Device.GameMode.Time.SubTick)
                            {
                                if (Command.IsServerCommand)
                                {
                                    ServerCommand RCommand = (ServerCommand) Command;

                                    if (this.Device.GameMode.CommandManager.AvailableServerCommands.ContainsKey(RCommand.Id))
                                    {
                                        ServerCommand ACommand = (ServerCommand) this.Device.GameMode.CommandManager.AvailableServerCommands[RCommand.Id];

                                        ChecksumEncoder Encoder1 = new ChecksumEncoder(null);
                                        ChecksumEncoder Encoder2 = new ChecksumEncoder(null);

                                        CommandManager.EncodeCommand(RCommand, Encoder1);
                                        CommandManager.EncodeCommand(ACommand, Encoder2);

                                        int RChecksum = Encoder1.Checksum;
                                        int AChecksum = Encoder2.Checksum;

                                        if (RChecksum != AChecksum)
                                        {
                                            // Console.WriteLine(BitConverter.ToString(Encoder1.ToArray()));
                                            // Console.WriteLine(BitConverter.ToString(Encoder2.ToArray()));

                                            Logging.Error(this.GetType(), "Execute command failed! Received server command does not have the same checksum that available server command. (r_checksum:" + RChecksum + " a_checksum:" + AChecksum + ")");
                                            goto Remove;
                                        }
                                    }
                                    else
                                    {
                                        Logging.Error(this.GetType(), "Execute command failed! Received server command is not available! (type=" + RCommand.Type + ")");
                                        goto Remove;
                                    }
                                }

                                Command.Execute(this.Device.GameMode);
                            }
                            else
                                Logging.Error(this.GetType(), this.Device, "Execute command failed! Command should be already executed. (type=" + Command.Type + " client_tick=" + Command.ExecuteTick + " server_tick=" + this.Device.GameMode.Time.SubTick + ")");
                        }

                        Remove:

                        this.Commands.RemoveAt(0);
                    }
                }

                this.Device.GameMode.Time.SubTick = this.Tick;
                this.Device.GameMode.Tick();

                if (this.Device.State != State.IN_BATTLE)
                {
                    if (this.Checksum != this.Device.GameMode.Checksum)
                    {
#if DEBUG
                        Logging.Error(this.GetType(), "Client - Server are Out Of Sync! (client_checksum:" + this.Checksum + " server_checksum:" + this.Device.GameMode.Checksum + ")");
#endif
                        // new Out_Of_Sync_Message(this.Device, this.Checksum, this.Device.GameMode.Checksum).Send();
                    }
                }
            }
            else
            {
                Logging.Error(this.GetType(), this.Device, "Execute command failed! SubTick is not valid.");
            }
        }
    }
}