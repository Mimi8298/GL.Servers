namespace GL.Servers.SL.Packets.Messages.Server
{
    using System.Text;

    using GL.Servers.SL.Logic;
    using GL.Servers.Extensions.List;

    internal class Server_Error : Message
    {
        private string Message;
        private StringBuilder Reason;

        /// <summary>
        /// Initializes a new instance of the <see cref="Server_Error"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Message">The message.</param>
        /// <param name="Initial">if set to <c>true</c> [initial].</param>
        public Server_Error(Device Device, string Message = "", bool Initial = false) : base(Device)
        {
            this.Identifier = 24115;
            this.Message    = Message;
            this.Reason     = new StringBuilder();

            if (!Initial)
            {
                this.Reason.AppendLine("Your game threw an exception on our servers,\nplease contact one of the GL Servers Developers with these following informations :");

                if (this.Device.Player != null)
                {
                    this.Reason.AppendLine("Your Player Name    : " + this.Device.Player.Name + ".");
                    this.Reason.AppendLine("Your Player ID      : " + this.Device.Player.HighID + '-' + this.Device.Player.LowID + ".");
                }

                this.Reason.AppendLine();
                this.Reason.AppendLine("Trace : ");
            }

            this.Reason.AppendLine(this.Message);
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddString(this.Reason.ToString());
        }
    }
}