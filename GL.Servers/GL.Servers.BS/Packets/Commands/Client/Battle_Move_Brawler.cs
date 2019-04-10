namespace GL.Servers.BS.Packets.Commands.Client
{
    using GL.Servers.BS.Core;
    using GL.Servers.BS.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Battle_Move_Brawler : Command
    {
        private int Action;

        private int PositionX;
        private int PositionY;

        /// <summary>
        /// Initializes a new instance of the <see cref="Battle_Move_Brawler"/> class.
        /// </summary>
        /// <param name="Reader">The reader.</param>
        /// <param name="Device">The device.</param>
        /// <param name="CommandID">The command identifier.</param>
        public Battle_Move_Brawler(Reader Reader, Device Device, int CommandID) : base(Reader, Device, CommandID)
        {
            // Battle_Move_Brawler.
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode()
        {
            this.Action     = this.Reader.ReadVInt();

            this.PositionX  = this.Reader.ReadVInt();
            this.PositionY  = this.Reader.ReadVInt();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            Logging.Info(this.GetType(), " --  Move :");
            Logging.Info(this.GetType(), " --- Position X : " + this.PositionX);
            Logging.Info(this.GetType(), " --- Position Y : " + this.PositionY);
        }
    }
}
 