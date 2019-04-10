namespace GL.Servers.BS.Packets.Messages.Client
{
    using GL.Servers.BS.Core;
    using GL.Servers.BS.Core.Network;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Logic.Entries;
    using GL.Servers.BS.Logic.Slots.Items;
    using GL.Servers.BS.Packets.Messages.Server;

    using GL.Servers.Extensions.Binary;

    internal class Clan_Leave : Message
    {
        private Clan Clan;

        /// <summary>
        /// Initializes a new instance of the <see cref="Clan_Leave"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Clan_Leave(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Clan_Leave.
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            if (this.Device.Player.ClanLowID > 0)
            {
                this.Clan = Resources.Clans.Get(this.Device.Player.ClanHighID, this.Device.Player.ClanLowID, Constants.Database, false);

                if (this.Clan != null)
                {
                    Member Member;

                    if (this.Clan.Members.Entries.TryGetValue(this.Device.Player.PlayerID, out Member))
                    {
                        if (Member.PlayerID == this.Device.Player.PlayerID)
                        {
                            if (this.Clan.Members.TryRemove(Member))
                            {
                                Event_Entry Entry = new Event_Entry(Member)
                                {
                                    Status = 4
                                };

                                if (this.Clan.Messages.TryAdd(Entry))
                                {
                                    this.Device.Player.ClanHighID   = 0;
                                    this.Device.Player.ClanLowID    = 0;

                                    new Clan_Leave_OK(this.Device).Send();
                                    new Clan_Info(this.Device, null).Send();
                                }
                                else
                                {
                                    Logging.Error(this.GetType(), "Member successfully left the clan but the event hasn't been added on the chat, TryAdd(Entry) returned false.");
                                }
                            }
                            else
                            {
                                Logging.Error(this.GetType(), "Failed to leave the clan, TryRemove(Member) returned false.");
                            }
                        }
                        else
                        {
                            Logging.Error(this.GetType(), "Failed to leave the clan, the Member ID don't match our Player ID.");
                        }
                    }
                    else
                    {
                        Logging.Error(this.GetType(), "Clan members can't be informed about the leaving of the player, TryGetValue(PlayerID, out Member) returned false.");
                    }
                }
                else
                {
                    Logging.Error(this.GetType(), "Clan was null when Process() has been called.");
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Player was not in a clan when Process() has been called.");
            }
        }
    }
}