namespace GL.Clients.BS.Packets.Messages.Client
{
    using System.Collections.Generic;

    using GL.Clients.BS.Logic;

    using GL.Servers.Extensions.List;
    using GL.Servers.Logic.Enums;

    internal class Execute_Commands : Message
    {
        private int Checksum;
        private int Tick;

        private List<Command> Commands;

        /// <summary>
        /// Initializes a new instance of the <see cref="Execute_Commands"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Execute_Commands(Device Device) : base(Device)
        {
            this.Identifier = 14102;
            this.Commands   = new List<Command>(0);
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddBool(this.Device.State == State.IN_BATTLE);
            this.Data.AddVInt(this.Tick);
            this.Data.AddVInt(this.Checksum);

            this.Data.AddVInt(this.Commands.Count);

            foreach (Command Command in this.Commands)
            {
                this.Data.AddRange(Command.ToBytes);
            }
        }
    }
}