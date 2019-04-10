namespace GL.Servers.SL.Packets.Messages.Server
{
    using GL.Servers.Extensions.List;

    using GL.Servers.SL.Logic;

    internal class GameCenter_Account_Bound : Message
    {
        private int ResultCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameCenter_Account_Bound"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Command">The command.</param>
        public GameCenter_Account_Bound(Device Device, int ResultCode) : base(Device)
        {
            this.Identifier = 24211;
            this.ResultCode = ResultCode;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(this.ResultCode);
        }
    }
}