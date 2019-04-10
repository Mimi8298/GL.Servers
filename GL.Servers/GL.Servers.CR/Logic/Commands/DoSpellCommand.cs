namespace GL.Servers.CR.Logic.Commands
{
    using GL.Servers.CR.Extensions.Helper;
    using GL.Servers.CR.Files.Logic;
    using GL.Servers.CR.Logic.Mode;
    using GL.Servers.DataStream;

    internal class DoSpellCommand : Command
    {
        internal int X;
        internal int Y;
        internal SpellData SpellData;

        /// <summary>
        /// Gets the type of this command.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoSpellCommand"/> class.
        /// </summary>
        public DoSpellCommand()
        {
            // DoSpellCommand.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoSpellCommand"/> class.
        /// </summary>
        public DoSpellCommand(SpellData Data, int X, int Y)
        {
            this.SpellData = Data;
            this.X = X;
            this.Y = Y;
        }
        
        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(ByteStream Packet)
        {
            base.Decode(Packet);

            Packet.ReadVInt();
            this.SpellData = Packet.DecodeData<SpellData>();

            if (Packet.ReadBoolean())
            {
                Packet.DecodeLogicData<SpellData>(26);
                Packet.ReadVInt();
            }

            this.X = Packet.ReadVInt();
            this.Y = Packet.ReadVInt();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode(ChecksumEncoder Packet)
        {
            base.Encode(Packet);

            Packet.AddVInt(1);
            Packet.EncodeData(this.SpellData);

            if (true)
            {
                Packet.AddBoolean(true);

                Packet.EncodeLogicData(this.SpellData, 26);
                Packet.AddVInt(1);
            }
            else 
                Packet.AddBoolean(false);

            Packet.AddVInt(this.X);
            Packet.AddVInt(this.Y);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(GameMode GameMode)
        {

        }
    }
}