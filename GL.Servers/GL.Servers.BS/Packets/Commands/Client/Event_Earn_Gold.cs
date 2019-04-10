namespace GL.Servers.BS.Packets.Commands.Client
{
    using GL.Servers.BS.Logic;

    using GL.Servers.Extensions.Binary;

    internal class Event_Earn_Gold : Command
    {
        private int MapID;
        private int EventID;

        /// <summary>
        /// Initializes a new instance of the <see cref="Event_Earn_Gold"/> class.
        /// </summary>
        /// <param name="Reader">The reader.</param>
        /// <param name="Device">The device.</param>
        /// <param name="CommandID">The command identifier.</param>
        public Event_Earn_Gold(Reader Reader, Device Device, int CommandID) : base(Reader, Device, CommandID)
        {
            // Event_Earn_Gold.
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode()
        {
            this.ReadHeader();

            this.EventID    = this.Reader.ReadVInt();
            this.MapID      = this.Reader.ReadVInt();
        }
    }
}
 