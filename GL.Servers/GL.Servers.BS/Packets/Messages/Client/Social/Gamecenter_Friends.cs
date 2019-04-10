namespace GL.Servers.BS.Packets.Messages.Client
{
    using GL.Servers.BS.Core;
    using GL.Servers.BS.Core.Network;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Packets.Messages.Server;
    using GL.Servers.Extensions.Binary;

    internal class Gamecenter_Friends : Message
    {
        private string[] Friends;

        /// <summary>
        /// Initializes a new instance of the <see cref="Gamecenter_Friends"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Gamecenter_Friends(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Gamecenter_Friends.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            int Count = this.Reader.ReadVInt();

            if (Count >= 0 && Count <= 50)
            {
                this.Friends = new string[Count];

                for (int i = 0; i < Count; i++)
                {
                    this.Friends[i] = this.Reader.ReadString();
                }
            }
            else
            {
                Logging.Error(this.GetType(), this.Device, "Error when reading the friend list, the count[" + Count + "] is either inferior to zero or superior to 50.");
            }
        }

        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override void Process()
        {
            if (this.Friends == null)
            {
                this.Friends = new string[0];
            }

            new Friends_List(this.Device, this.Friends).Send();
        }
    }
}