namespace GL.Servers.BB.Packets.Messages.Client.Player
{
    using System;
    using System.Collections.Generic;

    using GL.Servers.BB.Core;
    using GL.Servers.BB.Logic;
    using GL.Servers.BB.Logic.Manager;
    using GL.Servers.BB.Packets.Enums;
    using GL.Servers.Extensions.Binary;

    internal class End_Client_Turn_Message : Message
    {
        private int SubTick;
        private int Checksum;
        private int Seed;

        private Queue<Command> Commands;

        internal override ServiceNode ServiceNode
        {
            get
            {
                return ServiceNode.Player;
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

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.SubTick    = this.Reader.ReadInt32();
            this.Checksum   = this.Reader.ReadInt32();
            this.Seed       = this.Reader.ReadInt32();

            this.Reader.ReadByte();

            int Count        = this.Reader.ReadInt32();

            if (Count <= 512)
            {
                if (Count > 0)
                {
                    this.Commands = new Queue<Command>(Count);

                    for (int i = 0; i < Count; i++)
                    {
                        Command Command = CommandManager.DecodeCommand(this.Reader);

                        if (Command != null)
                        {
                            this.Commands.Enqueue(Command);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            if (this.Commands != null)
            {
                this.Device.GameMode.CommandManager.ExecuteCommands(this.Commands);
            }

            this.Device.GameMode.UpdateSubTick(this.SubTick);
        }
    }
}