namespace GL.Servers.BS.Packets.Messages.Client
{
    using GL.Servers.BS.Core;
    using GL.Servers.BS.Core.Network;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Logic.Entries;
    using GL.Servers.BS.Logic.Slots.Items;
    using GL.Servers.BS.Packets.Messages.Server;

    using GL.Servers.Extensions.Binary;

    internal class Clan_Join : Message
    {
        private Clan Clan;

        private int HighID;
        private int LowID;

        /// <summary>
        /// Initializes a new instance of the <see cref="Clan_Join"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Clan_Join(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Clan_Join.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.HighID = this.Reader.ReadInt32();
            this.LowID  = this.Reader.ReadInt32();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            if (this.Device.Player.ClanLowID == 0)
            {
                this.Clan = Resources.Clans.Get(this.HighID, this.LowID, Constants.Database, false);

                if (this.Clan != null)
                {
                    if (this.Clan.Members.TryAdd(this.Device.Player))
                    {
                        new Clan_Join_OK(this.Device).Send();
                        new Clan_Data(this.Device, this.Clan).Send();
                        new Clan_Stream(this.Device, this.Clan).Send();
                        new Clan_Info(this.Device, this.Clan).Send();

                        Member Member;

                        if (this.Clan.Members.Entries.TryGetValue(this.Device.Player.PlayerID, out Member))
                        {
                            if (Member.PlayerID == this.Device.Player.PlayerID)
                            {
                                Event_Entry Entry = new Event_Entry(Member)
                                {
                                    Status = 3
                                };

                                this.Clan.Messages.TryAdd(Entry);
                            }
                            else
                            {
                                Logging.Error(this.GetType(), "The Member ID don't match our Player ID.");
                            }
                        }
                        else
                        {
                            Logging.Error(this.GetType(), "Clan members can't be informed about the joining of the player, TryGetValue(PlayerID, out Member) returned false.");
                        }
                    }
                    else
                    {
                        Logging.Error(this.GetType(), "Player can't join the clan, TryAdd(Player) returned false.");
                    }
                }
                else
                {
                    Logging.Error(this.GetType(), "Clan was null when Process() has been called.");
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Player was already in a clan when the message has been called.");
            }
        }
    }
}