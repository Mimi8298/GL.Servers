namespace GL.Servers.BS.Packets.Messages.Client
{
    using System.Collections.Generic;
    using System.Net.Sockets;

    using GL.Servers.BS.Core;
    using GL.Servers.BS.Core.Network;
    using GL.Servers.BS.Files.CSV_Helpers;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Logic.Slots.Items;
    using GL.Servers.BS.Packets.Messages.Server;

    using GL.Servers.Extensions.Binary;
    using GL.Servers.Logic.Enums;

    internal class Matchmaking : Message
    {
        private IEnumerable<Player> Players;

        /// <summary>
        /// Gets the card global identifier.
        /// </summary>
        private long Card
        {
            get
            {
                return GlobalID.Create(this.CardType, this.CardID);
            }
        }

        private int CardType;
        private int CardID;

        private int MapIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="Matchmaking"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Matchmaking(Device Device, Reader Reader) : base(Device, Reader)
        {
            this.Device.State = State.MATCHMAKING;
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Reader.ReadVInt();

            this.CardType   = this.Reader.ReadVInt();
            this.CardID     = this.Reader.ReadVInt();

            this.Reader.ReadVInt();

            this.MapIndex   = this.Reader.ReadVInt();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            Resources.Battles.Waiting.Enqueue(this.Device.Player);
            
            for (int i = 0; i < 5; i++)
            {
                Device Device   = new Device(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp));
                Player Player   = new Player(Device, 0, 50 + (i * 10));
                Player.Name     = "Bot #" + (i + 1);
                Device.Player   = Player;
                Device.State    = State.MATCHMAKING;

                Resources.Battles.Waiting.Enqueue(Player);
            }

            this.Players = Resources.Battles.Waiting.Dequeue(6);

            if (this.Players != null)
            {
                Battle Battle = new Battle(this.Players);

                foreach (Player Player in this.Players)
                {
                    Player.Device.State = State.IN_BATTLE;
                }

                Battle.Start();
            }
            else
            {
                new Matchmaking_Data(this.Device, this.Device.Player).Send();
            }
        }
    }
}