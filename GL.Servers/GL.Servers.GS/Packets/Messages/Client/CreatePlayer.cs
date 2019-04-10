namespace GL.Servers.GS.Packets.Messages.Client
{
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;

    using GL.Servers.GS.Core.Network;
    using GL.Servers.GS.Logic;
    using GL.Servers.GS.Logic.Enums;

    using GL.Servers.GS.Packets.Messages.Server;

    internal class CreatePlayer : Message
    {
        internal string Name;

        internal int CharacterData;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePlayer"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public CreatePlayer(Device Device, Reader Reader) : base(Device, Reader)
        {
            this.Node = ServiceNode.SERVICE_NODE_TYPE_AVATAR_CONTAINER;
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.ShowBuffer();
            return;

            this.Name          = this.Reader.ReadString();
            this.CharacterData = this.Reader.ReadInt32();

            for (int Count = this.Reader.ReadInt32(); Count > 0; Count--)
            {
                this.Reader.ReadInt32();
            }
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddString(this.Name);

            this.Data.AddInt(this.CharacterData);
            this.Data.AddInt(0);
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            // new Unknown20205(this.Device).Send();
            // new Create_Player_OK(this.Device).Send();
            new Create_Player_Failed(this.Device).Send();
        }
    }
}
