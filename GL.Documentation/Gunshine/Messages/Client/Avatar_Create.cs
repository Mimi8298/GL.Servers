namespace GL.Servers.GS.Packets.Messages.Client
{
    using GL.Servers.GS.Core.Network;
    using GL.Servers.GS.Extensions.Binary;
    using GL.Servers.GS.Logic;
    using GL.Servers.GS.Packets.Messages.Server;

    internal class Avatar_Create : Message
    {
        internal string Username;

        internal int CharacterClass;
        internal int Gender;
        internal int SkinColor;
        internal int Face;
        internal int HairColor;
        internal int FacialHairCut;

        /// <summary>
        /// Initializes a new instance of the <see cref="Avatar_Create"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Avatar_Create(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Avatar_Create.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Username       = this.Reader.ReadString();

            this.CharacterClass = this.Reader.ReadInt32();
            this.Gender         = this.Reader.ReadInt32();
            this.SkinColor      = this.Reader.ReadInt32();
            this.Face           = this.Reader.ReadInt32();
            this.HairColor      = this.Reader.ReadInt32();
            this.FacialHairCut  = this.Reader.ReadInt32();

            for (int i = 0; i < 10; i++)
            {
                this.Reader.ReadInt32();
            }
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            this.ShowValues();
            new Avatar_Data(this.Device).Send();
        }
    }
}
 