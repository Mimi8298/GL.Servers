namespace GL.Clients.CoC.Packets.Messages.Server
{
    using GL.Clients.CoC.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Own_Home_Data : Message
    {
        internal int Trophies;
        internal int Experience;
        internal int Thumbnail;

        internal int CoinBoostSeconds;
        internal int CoinDoublerRemaining;

        /// <summary>
        /// Initializes a new instance of the <see cref="Own_Home_Data"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Own_Home_Data(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Own_Home_Data.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();

            this.Trophies   = this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Experience = this.Reader.ReadVInt();

            this.Reader.ReadVInt();
            this.Thumbnail  = this.Reader.ReadVInt();

            this.Reader.ReadVInt();
            this.Reader.ReadVInt();

            this.Reader.ReadInt32();

            this.Reader.ReadVInt(); // Gold Reward

            this.Reader.ReadVInt();
            this.Reader.ReadVInt();

            this.CoinDoublerRemaining   = this.Reader.ReadVInt();
            this.CoinBoostSeconds       = this.Reader.ReadVInt();
            this.Reader.ReadVInt();

            this.Reader.ReadVInt();

            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();

            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();

            int Maps = this.Reader.ReadVInt();

            for (int i = 0; i < Maps; i++)
            {
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();

                this.Reader.ReadString();
            }

            int Events = this.Reader.ReadVInt();

            for (int i = 0; i < Events; i++)
            {
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();

                this.Reader.ReadString();
            }

            int Unknowns = this.Reader.ReadVInt();

            for (int i = 0; i < Unknowns; i++)
            {
                this.Reader.ReadVInt();
            }

            int Levels = this.Reader.ReadVInt();

            for (int i = 0; i < Levels; i++)
            {
                this.Reader.ReadVInt();

                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();

                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
            }


            /* ----------------------------------------- */

            this.Reader.ReadInt32();
            this.Reader.ReadInt32();

            this.Reader.ReadVInt();
            this.Reader.ReadVInt();

            this.Reader.ReadInt32();

            this.Reader.ReadString();
            this.Reader.ReadVInt();

            this.Reader.ReadString();
            this.Reader.ReadVInt();

            int Resources = this.Reader.ReadVInt();

            for (int i = 0; i < Resources; i++)
            {
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
            }

            int Deck = this.Reader.ReadVInt();

            for (int i = 0; i < Deck; i++)
            {
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
            }

            int Deck2 = this.Reader.ReadVInt();

            for (int i = 0; i < Deck2; i++)
            {
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
            }

            int Stats = this.Reader.ReadVInt();

            for (int i = 0; i < Stats; i++)
            {
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
                this.Reader.ReadVInt();
            }

            this.Reader.ReadInt32();
            this.Reader.ReadInt32();
            this.Reader.ReadInt32();

            /* ------------------------ */

            this.Reader.ReadVInt();
        }
    }
}