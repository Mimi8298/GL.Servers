namespace GL.Servers.CoC.Packets.Messages.Server.Home
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Enums;

    internal class Npc_Data_Message : Message
    {
        internal Home NpcHome;
        internal NpcPlayer NpcPlayer;
        internal Player Visitor;
        internal int Timestamp;
        internal int SecondsSinceLastSave;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 24133;
            }
        }

        /// <summary>
        /// Gets a value indicating the service node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Home;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Npc_Data_Message"/> class.
        /// </summary>
        public Npc_Data_Message(Device Device, Home NpcHome, NpcPlayer NpcPlayer, Player Visitor, int Timestamp, int SecondsSinceLastSave) : base(Device)
        {
            this.NpcHome              = NpcHome;
            this.NpcPlayer           = NpcPlayer;
            this.Visitor              = Visitor;
            this.Timestamp            = Timestamp;
            this.SecondsSinceLastSave = SecondsSinceLastSave;
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(this.SecondsSinceLastSave);
            this.Data.AddInt(this.Timestamp);

            this.NpcHome.Encode(this.Data);
            this.Visitor.Encode(this.Data);

            this.Data.AddInt(0);
            this.Data.AddInt(0);
            
            this.NpcPlayer.Encode(this.Data);

            this.Data.Add(0);
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            this.Device.GameMode.LoadNpcAttackState(this.NpcPlayer, this.NpcHome, this.Visitor, this.Timestamp, this.SecondsSinceLastSave);
        }
    }
}