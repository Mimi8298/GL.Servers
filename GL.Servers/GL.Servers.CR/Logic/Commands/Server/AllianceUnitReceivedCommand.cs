namespace GL.Servers.CR.Logic.Commands.Server
{
    using GL.Servers.CR.Extensions.Helper;
    using GL.Servers.CR.Extensions.Utils;
    using GL.Servers.CR.Files.Logic;
    using GL.Servers.CR.Logic.Mode;
    using GL.Servers.CR.Logic.Spells;
    using GL.Servers.DataStream;

    internal class AllianceUnitReceivedCommand : ServerCommand
    {
        internal string Sender;
        internal SpellData Data;

        /// <summary>
        /// Gets the type of this command.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 208;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AllianceUnitReceivedCommand"/> class.
        /// </summary>
        public AllianceUnitReceivedCommand()
        {
            // AllianceUnitReceivedCommand.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimRewardCommand"/> class.
        /// </summary>
        public AllianceUnitReceivedCommand(string Sender, SpellData Data)
        {
            this.Sender = Sender;
            this.Data = Data;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(ByteStream Packet)
        {
            this.Sender = Packet.ReadString();
            this.Data = Packet.DecodeData<SpellData>();

            base.Decode(Packet);
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode(ChecksumEncoder Packet)
        {
            Packet.AddString(this.Sender);
            Packet.EncodeData(this.Data);

            base.Encode(Packet);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(GameMode GameMode)
        {
            Player Player = GameMode.Player;

            if (Player != null)
            {
                Spell Spell = GameMode.Home.GetSpellByData(this.Data);

                if (Spell == null)
                {
                    Spell = new Spell(this.Data);
                    Spell.SetCreateTime(TimeUtil.MinutesSince1970);

                    GameMode.Home.AddSpell(Spell);
                }
                else
                    Spell.AddMaterialCount(1);
            }
        }
    }
}