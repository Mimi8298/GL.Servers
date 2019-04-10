namespace GL.Servers.CoC.Packets.Messages.Client.Avatar
{
    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Core.Network;
    using GL.Servers.CoC.Packets.Enums;
    using GL.Servers.CoC.Packets.Messages.Server.Avatar;

    using GL.Servers.Extensions.Binary;

    internal class Ask_For_Avatar_Profile_Message : Message
    {
        internal int AvatarHighID;
        internal int AvatarLowID;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 14325;
            }
        }

        /// <summary>
        /// Gets a value indicating the service node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Avatar;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ask_For_Avatar_Profile_Message"/> class.
        /// </summary>
        public Ask_For_Avatar_Profile_Message(Device Device, Reader Reader) : base(Device, Reader)
        {

        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode()
        {
            this.AvatarHighID = this.Reader.ReadInt32();
            this.AvatarLowID = this.Reader.ReadInt32();

            if (this.Reader.ReadBoolean())
            {
                this.Reader.ReadInt32();
                this.Reader.ReadInt32();
            }

            this.Reader.ReadBoolean();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override async void Process()
        {
            if (this.AvatarHighID >= 0 && this.AvatarLowID > 0)
            {
                Player Player = await Resources.Accounts.LoadPlayerAsync(this.AvatarHighID, this.AvatarLowID);
                Home Home = await Resources.Accounts.LoadHomeAsync(this.AvatarHighID, this.AvatarLowID);

                if (Player != null)
                {
                    new Avatar_Profile_Message(this.Device, Player, Home).Send();
                }
            }
        }
    }
}