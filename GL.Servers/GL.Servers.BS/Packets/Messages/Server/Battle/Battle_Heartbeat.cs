namespace GL.Servers.BS.Packets.Messages.Server
{
    using System.Collections.Generic;

    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Logic.Slots.Items;

    using GL.Servers.Extensions.List;

    internal class Battle_Heartbeat : Message
    {
        private Battle Battle;

        private int Tick;

        private string[] BattleHeartbeats;

        /// <summary>
        /// Initializes a new instance of the <see cref="Battle_Heartbeat"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Battle">The battle.</param>
        /// <param name="Tick">The tick.</param>
        public Battle_Heartbeat(Device Device, Battle Battle, int Tick = 0) : base(Device)
        {
            this.Identifier = 24109;
            this.Battle     = Battle;
            this.Tick       = Tick;
            this.BattleHeartbeats = System.IO.File.ReadAllLines("Gamefiles/Heartbeats.bin");
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            /*
            string Data = this.BattleHeartbeats[this.Tick - 1];
            Data = Data.Replace("-", string.Empty);
            Data = Data.Substring(7 * 2, Data.Length - (7 * 2));
            this.Data.AddHexa(Data);
            return;
            */
            this.Data.AddVInt(this.Tick);
            this.Data.AddVInt(this.Tick / 10); // A3-07 = 483
            this.Data.AddVInt(2);

            List<byte> Battle = new List<byte>(2048);

            Battle.AddVInt(15712535);

            Battle.AddHexa("01-C0-E0-3A-00-E0-01-34-00-3E-00-F8-01-18-F8-71-01-FE-37-30-06-18-81-4D-55-00-00-00-C0-82-00-C5-08-18-01-23-60-04-8C-80-11-30-02-46-C0-08-18-01-03-60-00-0C-80-01-30-00-30-00-02-C0-00-48-20-0B-C2-D2-10-98-86-D0-34-04-A7-21-3C-0D-01-6A-08-51-43-90-1A-C2-D4-10-A8-86-50-35-04-AB-21-5C-0D-01-6B-08-59-83-90-00-04-05-20-2E-00-81-01-10-00-20-4E-71-51-13-AF-80-C0-74-57-26-35-F1-0A-50-0A-97-4D-50-13-AF-80-89-DC-52-A8-43-F1-0A-BC-47-BC-01-51-13-AF-00-6E-A2-94-58-43-F1-0A-04-26-D9-A1-4E-13-AF-80-52-E0-54-FF-42-F1-0A-4C-84-FC-19-4F-13-AF-00-37-9A-D6-AB-42-F1-0A-94-22-66-7D-44-15-AF-00-1F-51-96-24-74-B1-20-A2-8A-57-80-8E-19-6B-11-D2-57-1E-51-C5-2B-C0-47-84-25-09-89-2B-85-A8-E2-15-A0-A3-BE-42-84-8A-55-45-54-F1-0A-F0-51-5D-35-42-BE-6B-0D-A1-00-80-96-21-3A-06-00-00-58-C2-D2-02-00-C8-00-50-14-E8-03-4B-84-1C-00-B0-44-BF-02-7F-2A-00-A0-5F-44-8D-00-00-00-DC-E0-26");

            Battle.Add(0);
            Battle.Add(0);
            Battle.Add(50);

            Battle.Add(0);
            Battle.Add(0);
            Battle.Add(0);
            Battle.Add(100);
            
            Battle.AddIntEndian(131077600);

            Battle.AddIntEndian(-1798218240); // X
            Battle.AddIntEndian(10078); // Y

            {
                Battle.AddIntEndian(520360); // Texture
            }
            
            Battle.AddShortEndian(500 * 8);
            Battle.AddShortEndian(662);
            Battle.Add(0);
            Battle.AddIntEndian(0);
            Battle.Add(1);
            Battle.AddIntEndian(0);
            Battle.AddIntEndian(500 * 2); // Amount

            Battle.AddHexa("E0-58-BB-A1-00-00-34-61-C2-F0-07-00-F8-D1-8F-00-00-00-00-01-00-00-00-00-00");
            Battle.AddIntEndian(375);
            Battle.AddHexa("00-00-EC-93-D5-64-06-00-E5-42-A0-0F");

            byte[] Data = Battle.ToArray();

            this.Data.AddInt(Data.Length);
            this.Data.AddRange(Data);
        }
    }
}
