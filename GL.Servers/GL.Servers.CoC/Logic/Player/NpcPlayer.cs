namespace GL.Servers.CoC.Logic
{
    using GL.Servers.CoC.Extensions.Helper;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;
    using GL.Servers.CoC.Logic.Slots;
    using GL.Servers.Extensions.List;

    internal class NpcPlayer : PlayerBase
    {
        internal NpcData Data;
        internal ResourceSlots Resources;

        /// <summary>
        /// Gets the alliance name of npc player.
        /// </summary>
        internal string AllianceNameTID
        {
            get
            {
                return this.Data.AllianceName;
            }
        }

        /// <summary>
        /// Gets the exp level of npc player;
        /// </summary>
        internal int ExpLevel
        {
            get
            {
                return this.Data.ExpLevel;
            }
        }

        /// <summary>
        /// Gets the name of npc player;
        /// </summary>
        internal string NameTID
        {
            get
            {
                return this.Data.PlayerName;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpcPlayer"/> class.
        /// </summary>
        internal NpcPlayer()
        {
            // NpcPlayer.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpcPlayer"/> class.
        /// </summary>
        internal NpcPlayer(NpcData Data)
        {
            this.SetNpcData(Data);
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal void Encode(ByteWriter Packet)
        {
            Packet.AddData(this.Data);
        }

        /// <summary>
        /// Sets the npc data.
        /// </summary>
        internal void SetNpcData(NpcData Data)
        {
            this.Data = Data;

            this.Resources.Set(3000001, this.Data.Gold);
            this.Resources.Set(3000002, this.Data.Elixir);
        }
    }
}
