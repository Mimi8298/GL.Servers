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

    internal class Clan_Search : Message
    {
        private string Name;

        private int MinTrophies;
        private int MaxMembers;
        private int MinMembers;
        private int Origin;

        private bool OnlyOpen;

        /// <summary>
        /// Initializes a new instance of the <see cref="Clan_Search"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Clan_Search(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Clan_Search.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            // 00-00-00-06  62-6C-61-62-6C-61  00-00-00-00  00-00-00-00  00-00-00-32  00-00-00-00  00-00-00-00  00-00-00-00  00

            this.Name       = this.Reader.ReadString();

            this.Reader.ReadInt32();

            this.MinMembers = this.Reader.ReadInt32();
            this.MaxMembers = this.Reader.ReadInt32();

            this.Reader.ReadInt32();
            this.Reader.ReadInt32();
            this.Reader.ReadInt32();

            this.OnlyOpen   = this.Reader.ReadBoolean();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            List<Clan> Clans    = Resources.Clans.Values.ToList();

            Clans               = Clans.FindAll(T => T != null && T.Name.ToLower().Contains(this.Name.ToLower()));

            if (Clans.Count > 20)
            {
                Clans.Shuffle(Resources.Random);
                Clans.RemoveRange(20, Clans.Count - 20);
            }

            new Search_Clans_Data(this.Device, this.Name, Clans).Send();
        }
    }
}