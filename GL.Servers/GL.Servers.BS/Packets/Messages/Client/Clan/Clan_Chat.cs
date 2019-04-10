namespace GL.Servers.BS.Packets.Messages.Client
{
    using GL.Servers.BS.Core;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Logic.Entries;
    using GL.Servers.BS.Logic.Slots.Items;

    using GL.Servers.Extensions.Binary;

    internal class Clan_Chat : Message
    {
        private Clan Clan;

        private string Message;

        /// <summary>
        /// Initializes a new instance of the <see cref="Clan_Chat"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Clan_Chat(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Clan_Chat.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Message = this.Reader.ReadString();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            if (string.IsNullOrWhiteSpace(this.Message))
            {
                Logging.Error(this.GetType(), "Message was either null or empty, we are aborting.");
            }
            else if (this.Message.Length > 255)
            {
                Logging.Error(this.GetType(), "Message length was superior to 255, we are aborting.");
            }
            else
            {
                if (this.Device.Player.ClanLowID > 0)
                {
                    this.Clan = Resources.Clans.Get(this.Device.Player.ClanHighID, this.Device.Player.ClanLowID, Constants.Database, false);

                    if (this.Clan != null)
                    {
                        Member Member;

                        if (this.Clan.Members.Entries.TryGetValue(this.Device.Player.PlayerID, out Member))
                        {
                            Chat_Entry Entry = new Chat_Entry(Member)
                            {
                                Message = this.Message
                            };

                            if (!this.Clan.Messages.TryAdd(Entry))
                            {
                                Logging.Error(this.GetType(), "Failed to add the specified chat message, TryAdd(Entry) returned false.");
                            }
                        }
                        else
                        {
                            Logging.Error(this.GetType(), "Failed at Process(), Member instance was null because TryGetValue(PlayerID, out Member) returned false.");
                        }
                    }
                    else
                    {
                        Logging.Error(this.GetType(), "Clan was null when Process() has been called.");
                    }
                }
                else
                {
                    Logging.Error(this.GetType(), "Player was not in a clan when the message has been called.");
                }
            }
        }
    }
}