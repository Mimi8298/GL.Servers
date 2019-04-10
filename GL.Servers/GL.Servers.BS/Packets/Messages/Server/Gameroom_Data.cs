namespace GL.Servers.BS.Packets.Messages.Server
{
    using System.Linq;

    using GL.Servers.BS.Logic;

    using GL.Servers.Extensions.List;

    internal class Gameroom_Data : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Gameroom_Data"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Gameroom_Data(Device Device) : base(Device)
        {
            this.Identifier = 24124;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            // 00-00-03-00
            // 00-00-01-00

            // 00-ED-00-84-D5-98-94-0B-EF-CA-67-01-0F-01-01

            // 00-00-00-01
            // 00-00-09-00
            // 00-00-00-06-42-65-72-6B-61-6E
            
            // 02-10-00-00-05-05-00-03-00-00-00

            this.Data.AddHexa("00-00-03-00-00-00");

            this.Data.AddHexa("00-08-00-01");

            this.Data.AddVInt(1497568580);      // 84-D5-98-94-0B
            this.Data.AddHexa("EF-CA-67-01");   // -200017

            this.Data.AddHexa("0F  01  01");

            this.Data.AddInt(this.Device.Player.HighID);
            this.Data.AddInt(this.Device.Player.LowID);
            this.Data.AddString(this.Device.Player.Name);
            this.Data.AddVInt(2);

            this.Data.AddRange(this.Device.Player.Deck.First().Value.ToBytes);
            this.Data.AddVInt(3); // 4 = In Matchmake   3 = Present   2 = Other Screen   1 = In Battle   0 = Offline   (TID_MEMBER_STATUS_)

            this.Data.AddHexa("00-00-00");
        }
    }
}