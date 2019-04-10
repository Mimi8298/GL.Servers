namespace GL.Servers.BS.Packets.Messages.Client
{
    using System.Collections.Generic;

    using GL.Servers.BS.Core;
    using GL.Servers.BS.Core.Network;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Logic.Slots.Items;
    using GL.Servers.BS.Logic.Structure;
    using GL.Servers.BS.Packets.Messages.Server;

    using GL.Servers.Extensions.Binary;
    using GL.Servers.Logic.Enums;

    internal class Battle_End : Message
    {
        private Battle Battle;
        private List<BattlePlayer> Players;

        private bool isNpc;

        /// <summary>
        /// Initializes a new instance of the <see cref="Battle_End"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Battle_End(Device Device, Reader Reader) : base(Device, Reader)
        {
            if (this.Device.State == State.IN_BATTLE)
            {
                this.Battle = Resources.Battles.Get(this.Device.Player.BattleID);
            }
            else
            {
                this.isNpc  = true;
            }

            this.Players = new List<BattlePlayer>(10);
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            // 10-0F-00-01-01-00-00-00-06-42-65-72-6B-61-6E-10-0A-00-00-00-00-00-00-01-31-10-07-00-00-00-00-00-00-01-32-10-03-00-00-00-00-00-00-01-33-10-03-00-01-00-00-00-00-01-34-10-06-00-01-00-00-00-00-01-35

            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
            this.Reader.ReadVInt(); // Negative
            this.Reader.ReadVInt();

            int Count = this.Reader.ReadVInt();

            this.ShowBuffer();

            return;

            for (int i = 0; i < Count; i++)
            {
                BattlePlayer BattlePlayer   = new BattlePlayer();

                BattlePlayer.CharacterType  = this.Reader.ReadVInt();     // Character Type
                BattlePlayer.CharacterID    = this.Reader.ReadVInt();     // Character ID

                this.Reader.ReadVInt();     // Unknown

                BattlePlayer.TeamID         = this.Reader.ReadVInt();     // is Enemy
                BattlePlayer.isBot          = this.Reader.ReadVInt();     // is Bot

                BattlePlayer.Username       = this.Reader.ReadString();   // Name

                this.Players.Add(BattlePlayer);
            }
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            if (this.isNpc)
            {
                new Battle_Result(this.Device, this).Send();
            }
            else
            {
                this.Battle.Stop();
            }
        }
    }
}