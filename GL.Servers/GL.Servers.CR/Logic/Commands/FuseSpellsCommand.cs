namespace GL.Servers.CR.Logic.Commands
{
    using GL.Servers.CR.Extensions.Helper;
    using GL.Servers.CR.Files;
    using GL.Servers.CR.Files.Logic;
    using GL.Servers.CR.Logic.Mode;
    using GL.Servers.CR.Logic.Spells;
    using GL.Servers.DataStream;

    internal class FuseSpellsCommand : Command
    {
        internal SpellData SpellData;

        /// <summary>
        /// Gets the type of this command.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 504;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FuseSpellsCommand"/> class.
        /// </summary>
        public FuseSpellsCommand()
        {
            // FuseSpellsCommand.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FuseSpellsCommand"/> class.
        /// </summary>
        public FuseSpellsCommand(SpellData Data)
        {
            this.SpellData = Data;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(ByteStream Packet)
        {
            base.Decode(Packet);

            this.SpellData = Packet.DecodeData<SpellData>();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode(ChecksumEncoder Packet)
        {
            base.Encode(Packet);

            Packet.EncodeData(this.SpellData);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(GameMode GameMode)
        {
            Home Home = GameMode.Home;

            if (Home != null)
            {
                Spell Spell = Home.GetSpellByData(this.SpellData);

                if (Spell != null)
                {
                    if (Spell.Level < this.SpellData.MaxLevelIndex)
                    {
                        if (Spell.CanUpgrade)
                        {
                            int Cost = this.SpellData.RarityData.UpgradeCost[Spell.Level];

                            if (GameMode.Player.HasEnoughResources(CSV.Tables.GoldData, Cost))
                            {
                                GameMode.Player.UseFreeGold(Cost);
                                GameMode.Player.XpGainHelper(Spell.UpgradeExp);

                                Spell.UpgradeToNextLevel();
                            }
                        }
                    }
                }
            }
        }
    }
}