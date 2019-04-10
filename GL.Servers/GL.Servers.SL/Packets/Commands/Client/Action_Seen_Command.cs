namespace GL.Servers.SL.Packets.Commands.Client
{
    using System;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.SL.Core;
    using GL.Servers.SL.Files;
    using GL.Servers.SL.Files.CSV_Logic.Logic;
    using GL.Servers.SL.Logic;

    internal class Action_Seen_Command : Command
    {
        internal int SeenType;
        internal int Value;

        internal override int Type
        {
            get
            {
                return 800;
            }
        }

        public Action_Seen_Command(Device Device) : base(Device)
        {
            
        }

        internal override void Decode(Reader Reader)
        {
            this.ReadHeader(Reader);
            this.SeenType = Reader.ReadInt32();
            this.Value = Reader.ReadInt32();
        }

        internal override void Process()
        {
            if (this.Value > 0)
            {
                switch (this.SeenType)
                {
                    case 0:
                    {
                        QuestData Quest = CSV.Tables.GetWithGlobalID(this.Value) as QuestData;

                        if (Quest != null)
                        {
                            this.Device.Player.QuestUnlockSeens.Set(Quest.GlobalID, 1);
                        }
                        else Logging.Error(this.GetType(), "QuestData is null or not valid.");

                        break;
                    }
                    case 1:
                    {
                        HeroData Hero = CSV.Tables.GetWithGlobalID(this.Value) as HeroData;

                        if (Hero != null)
                        {
                                this.Device.Player.HeroUnlockSeens.Set(Hero.GlobalID, 1);
                        }
                        else Logging.Error(this.GetType(), "HeroData is null or not valid.");

                        break;
                    }
                    case 3:
                    {
                        this.Device.Player.Variables.Set(26000018, 1);
                        break;
                    }
                    default:
                    {
                            Console.WriteLine("Unknown : " + this.SeenType);
                        break;
                    }
                }
            }   
        }
    }
}