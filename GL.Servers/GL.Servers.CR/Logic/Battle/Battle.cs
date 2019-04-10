namespace GL.Servers.CR.Logic
{
    using System.Collections.Generic;

    using GL.Servers.CR.Extensions.Helper;
    using GL.Servers.CR.Files;
    using GL.Servers.CR.Files.Logic;
    using GL.Servers.CR.Logic.Enums;

    using GL.Servers.DataStream;
    using GL.Servers.Extensions.List;

    internal class Battle
    {
        internal List<Player> Players;

        /// <summary>
        /// Initializes a new instance of the <see cref="Battle"/> class.
        /// </summary>
        public Battle()
        {
            this.Players = new List<Player>(4);
        }

        /// <summary>
        /// Adds the player to battle.
        /// </summary>
        internal void AddPlayer(Player Player)
        {
            this.Players.Add(Player);
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
            
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal void Encode(ChecksumEncoder Packet)
        {
            Packet.EncodeLogicData(CSV.Tables.Get(Gamefile.Location).GetWithInstanceID<LocationData>(2), 15);
            Packet.AddVInt(this.Players.Count);
            Packet.EncodeLogicData(null, 18);
            Packet.EncodeLogicData(CSV.Tables.Get(Gamefile.Arena).GetWithInstanceID<ArenaData>(1), 54);
            
            
            this.Players.ForEach(Player =>
            {
                Packet.AddLogicLong(Player.HighID, Player.LowID);
                Packet.AddVInt(0);
            });

            Packet.EncodeConstantSizeIntArray(new int[8], 8);

            {
                Packet.AddVInt(1);
                Packet.AddVInt(1);
                Packet.AddVInt(0);
                Packet.AddVInt(0);

                Packet.AddVInt(7); // EncodeLogicData (type=72)
                Packet.AddVInt(0); // EncodeLogicData (type=79)
                Packet.AddVInt(0); // EncodeLogicData (type=81)
            }

            Packet.AddBoolean(false); // isFinished
            Packet.AddBoolean(false);
            Packet.AddBoolean(false); 
            Packet.AddBoolean(false);
            Packet.AddBoolean(false); // InExtraTime
            Packet.AddBoolean(false); // Live

            Packet.AddVInt(0); // Type

            Packet.AddVInt(0);
            Packet.AddVInt(0);

            // LogicGameObjectManager::encode().
            {
                // Packet.EncodeConstantSizeIntArray(new int[7], 7);
                Packet.AddRange("00-B9-03-C7-7C-00-00-06-7A".HexaToBytes());

                Packet.AddVInt(6); // Count
                Packet.AddRange("23-01-23-01-23-01-23-01-23-00-23-00".HexaToBytes());
                Packet.AddRange("01-00-01-00-00-01".HexaToBytes()); // Is Enemy GameObject
                Packet.AddRange("05-00-05-01-05-02-05-03-05-04-05-05".HexaToBytes()); // ID

                Packet.AddRange("00-0D-A4-E2-01-9C-8E-03-00-00-7F-00-C0-7C-00-00-02-00-00-00-00-00".HexaToBytes());
                Packet.AddRange("00-0D-AC-36-A4-65-00-00-7F-00-80-04-00-00-01-00-00-00-00-00".HexaToBytes());
                Packet.AddRange("00-0D-AC-36-9C-8E-03-00-00-7F-00-C0-7C-00-00-01-00-00-00-00-00".HexaToBytes());
                Packet.AddRange("00-0D-A4-E2-01-A4-65-00-00-7F-00-80-04-00-00-02-00-00-00-00-00".HexaToBytes());
                Packet.AddRange("00-0D-A8-8C-01-B8-2E-00-00-7F-00-80-04-00-00-00-00-00-00-00".HexaToBytes());
                Packet.AddRange("00-0C-00-00-05-00-00-00-00-00-7F-7F-7F-7F-7F-7F-7F-7F-00-00-00-00".HexaToBytes());

                Packet.AddRange("00-0D-A8-8C-01-88-C5-03-00-00-7F-00-C0-7C-00-00-00-00-00-00-00".HexaToBytes());
                Packet.AddRange("00-0D-04-07-7D-7E-01-04-05-06-01-00-00-7F-7F-00-01-04-00-00-00-00".HexaToBytes());
            }

            Packet.AddRange("00-00-00-14-51-75-C3-AA-74-65-20-63-6F-6D-62-61-74-20-6D-69-72-6F-69-72-00-00-00-3A-50-61-72-74-69-63-69-70-65-7A-20-C3-A0-20-32-30-C2-A0-63-6F-6D-62-61-74-73-20-6D-69-72-6F-69-72-20-65-6E-20-6D-6F-64-65-20-31-63-31-2C-20-32-63-32-20-6F-75-20-64-C3-A9-66-69-00-00-00-08-73-63-2F-75-69-2E-73-63-00-00-00-16-71-75-65-73-74-5F-69-74-65-6D-5F-73-70-65-63-69-61-6C-5F-70-76-70-14-14-B8-12-00-00-01-00-00-00-1C-69-63-6F-6E-5F-71-75-65-73-74-5F-74-79-70-65-5F-73-70-65-63-69-61-6C-65-76-65-6E-74-01-05-00-14-94-01-00-00-00-00-11-00-00-00-00-05-00-00-00-00-00-7F-7F-7F-7F-7F-7F-7F-7F-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-B8-15-00-B8-15-00-B8-15-00-B8-15-00-A0-25-00-A0-25-00-00-00-00-00-00-00-00-A4-01-A4-01-00-00-00-00-00-00-00-00-A4-01-A4-01-00-00-00-00-00-00-00-00-A4-01-A4-01-00-00-00-00-00-00-00-00-A4-01-A4-01-00-00-00-00-00-00-00-00-A4-01-A4-01-00-00-00-00-00-00-00-00-A4-01-A4-01-00-FE-03-01-01-02-00-0E-00-8F-01-00-8E-01-00-04-00-0F-00-14-00-00-00-05-06-02-02-04-02-01-03-00-00-00-00-00-00-00-00".HexaToBytes());
        }
    }
}