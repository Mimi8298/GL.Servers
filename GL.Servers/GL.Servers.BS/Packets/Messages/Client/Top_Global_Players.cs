namespace GL.Servers.BS.Packets.Messages.Client
{
    using System.Collections.Generic;
    using System.Linq;

    using GL.Servers.BS.Core;
    using GL.Servers.BS.Core.Network;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Packets.Messages.Server;
    using GL.Servers.Extensions.Binary;

    internal class Top_Global_Players : Message
    {
        private bool isLocal;
        private int Type;

        /// <summary>
        /// Initializes a new instance of the <see cref="Top_Global_Players"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Top_Global_Players(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Top_Globals_Players.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.isLocal    = this.Reader.ReadBoolean();
            this.Type       = this.Reader.ReadVInt();

            this.Reader.ReadVInt();
        }

        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override void Process()
        {
            if (this.Type == 1)
            {
                if (this.isLocal)
                {
                    List<Player> Players    = Resources.Players.Values.ToList();

                    Players                 = Players.FindAll(Player => Player != null && !string.IsNullOrEmpty(Player.Name));
                    Players                 = Players.OrderByDescending(Player => Player.Level).ThenByDescending(Player => Player.Info.Trophies).ToList();

                    if (Players.Count > 200)
                    {
                        Players.RemoveRange(200, Players.Count - 200);
                    }

                    new Top_Local_Players_Data(this.Device, Players).Send();
                }
                else
                {
                    List<Player> Players = Resources.Players.Values.ToList();

                    Players = Players.FindAll(Player => Player != null && !string.IsNullOrEmpty(Player.Name));
                    Players = Players.OrderByDescending(Player => Player.Level).ThenByDescending(Player => Player.Info.Trophies).ToList();

                    if (Players.Count > 200)
                    {
                        Players.RemoveRange(200, Players.Count - 200);
                    }

                    new Top_Global_Players_Data(this.Device, Players).Send();
                }
            }
            else
            {
                if (this.isLocal)
                {
                    List<Clan> Clans    = Resources.Clans.Values.ToList();

                    // Clans            = Clans.FindAll(Clan => Clan.Origin == this.Device.Player.Origin);
                    Clans               = Clans.OrderByDescending(Clan => Clan.Trophies).ToList();

                    if (Clans.Count > 200)
                    {
                        Clans.RemoveRange(200, Clans.Count - 200);
                    }

                    new Top_Local_Clans_Data(this.Device, Clans).Send();
                }
                else
                {
                    List<Clan> Clans    = Resources.Clans.Values.ToList();

                    Clans               = Clans.OrderByDescending(Clan => Clan.Trophies).ToList();

                    if (Clans.Count > 200)
                    {
                        Clans.RemoveRange(200, Clans.Count - 200);
                    }

                    new Top_Global_Clans_Data(this.Device, Clans).Send();
                }
            }
        }
    }
}