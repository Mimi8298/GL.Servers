namespace GL.Servers.BS.Packets.Messages.Client
{
    using System.Collections.Generic;
    using System.Linq;

    using GL.Servers.BS.Core;
    using GL.Servers.BS.Core.Network;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Packets.Messages.Server;

    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;

    internal class Clan_Joinable : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Clan_Joinable"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Clan_Joinable(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Clan_Joinable.
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            List<Clan> Clans    = Resources.Clans.Values.ToList();

            Clans               = Clans.FindAll(T => T != null && T.Members.Entries.Count > 0 && T.Members.Entries.Count < 50);

            if (Clans.Count > 20)
            {
                Clans.Shuffle(Resources.Random);
                Clans.RemoveRange(20, Clans.Count - 20);
            }

            new Joinable_Clans_Data(this.Device, Clans).Send();
        }
    }
}