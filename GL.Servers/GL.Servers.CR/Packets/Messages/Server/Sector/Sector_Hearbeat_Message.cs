namespace GL.Servers.CR.Packets.Messages.Server.Sector
{
    using System.Collections.Generic;
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Commands;
    using GL.Servers.CR.Logic.Commands.Manager;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.DataStream;

    internal class Sector_Hearbeat_Message : Message
    {
        internal int ServerTurn;
        internal int Checksum;
        internal List<Command> Commands;

        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 21902;
            }
        }

        /// <summary>
        /// Gets the service node of this message.
        /// </summary>
        internal override Node ServiceNode
        {
            get
            {
                return Node.Sector;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sector_Hearbeat_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Sector_Hearbeat_Message(Device Device, int ServerTurn, int Checksum, List<Command> Commands) : base(Device)
        {
            this.ServerTurn = ServerTurn;
            this.Checksum = Checksum;
            this.Commands = Commands;
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddVInt(this.ServerTurn);
            this.Data.AddVInt(this.Checksum);

            if (this.Commands.Count > 0)
            {
                this.Data.AddVInt(this.Commands.Count);

                ChecksumEncoder Encoder = new ChecksumEncoder(this.Data);

                this.Commands.ForEach(Command =>
                {
                    CommandManager.EncodeCommand(Command, Encoder);
                });
            }
            else 
                this.Data.AddVInt(0);
        }
    }
}