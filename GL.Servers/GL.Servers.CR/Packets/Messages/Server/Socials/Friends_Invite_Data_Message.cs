namespace GL.Servers.CR.Packets.Messages.Server.Socials
{
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.Extensions.List;

    internal class Friends_Invite_Data_Message : Message
    {
        private string Token;

        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 20107;
            }
        }

        /// <summary>
        /// Gets the service node of this message.
        /// </summary>
        internal override Node ServiceNode
        {
            get
            {
                return Node.Account;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Friends_Invite_Data_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Token">The token.</param>
        public Friends_Invite_Data_Message(Device Device, string Token) : base(Device)
        {
            this.Token      = Token;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddBoolean(true);
            this.Data.AddString(this.Token);
        }
    }
}