namespace GL.Servers.CoC.Packets.Messages.Server.Alliance
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic.Clan;
    using GL.Servers.CoC.Packets.Enums;

    internal class Joinable_Alliance_List_Message : Message
    {
        private Alliance[] Alliances;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 24304;
            }
        }

        /// <summary>
        /// Gets a value indicating the service node.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Alliance;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Joinable_Alliance_List_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Joinable_Alliance_List_Message(Device Device, Alliance[] Alliances) : base(Device)
        {
            this.Alliances     = Alliances;
        }
        
        internal override void Encode()
        {
            this.Data.AddInt(this.Alliances.Length);

            for (int i = 0; i < this.Alliances.Length; i++)
            {
                this.Alliances[i].Encode(this.Data);
            }
        }
    }
}