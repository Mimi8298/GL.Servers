namespace GL.Servers.BS.Packets.Messages.Server
{
    using System.Collections.Generic;
    using System.Linq;

    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Logic.Slots.Items;

    using GL.Servers.Extensions.List;

    internal class Sector_PC : Message
    {
        private Battle Battle;
        private List<Player> Players;

        /*
             131 076 | 84-80-10  01
             131 140 | 84-81-10  01
             131 204 | 84-82-10  04

             131 268 | 84-83-10  01
             131 332 | 84-84-10  01
             131 396 | 84-85-10  00
         */

        private string[] Unknowns =
        {
            "84-80-10  01", "84-81-10  01", "84-82-10  04",
            "84-83-10  01", "84-84-10  01", "84-85-10  00"
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="Sector_PC"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Sector_PC(Device Device, Battle Battle) : base(Device)
        {
            this.Identifier = 20559;
            this.Battle     = Battle;
            this.Players    = Battle.Players.Values.ToList();
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(6);
            this.Data.AddInt(0);
            this.Data.AddInt(0);
            
            this.Data.AddInt(this.Players.Count);

            foreach (Player Player in this.Players)
            {
                this.Data.AddInt(Player.HighID);
                this.Data.AddInt(Player.LowID);

                this.Data.AddString(Player.Name);

                this.Data.AddVInt(this.Players.IndexOf(Player));
                this.Data.AddBool(this.Players.IndexOf(Player) >= 3);

                this.Data.AddHexa("00-00-00-1E");
                this.Data.AddHexa(this.Unknowns[this.Players.IndexOf(Player)]);

                this.Data.AddInt(0);
                
                this.Data.AddVInt(0); // Count
                {
                    // 17-XX-XX
                }
            }

            this.Data.AddVInt(0x00); // 0x28

            this.Data.AddHexa("E9-6D-F5-02");

            this.Data.AddVInt(0x01);
            this.Data.AddVInt(0x01);

            this.Data.AddVInt(0x00);
            this.Data.AddVInt(0x0F);
            this.Data.AddVInt(0x0A);
        }
    }
}
