namespace GL.Servers.SL.Packets.Commands.Client
{
    using System;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.SL.Core;
    using GL.Servers.SL.Extensions;
    using GL.Servers.SL.Logic;
    using GL.Servers.SL.Files.CSV_Logic.Logic;

    internal class Upgrade_Hero_Command : Command
    {
        internal HeroData Hero;

        internal override int Type
        {
            get
            {
                return 501;
            }
        }

        public Upgrade_Hero_Command(Device Device) : base(Device)
        {
            
        }

        internal override void Decode(Reader Reader)
        {
            this.Hero = Reader.ReadData<HeroData>();
            this.ReadHeader(Reader);
        }

        internal override void Process()
        {
            if (this.Hero != null)
            {
                if (this.Device.Player.HeroUpgrade.CanUpgrade(this.Hero))
                {
                    if (this.Device.Player.ExpLevel < this.Hero.RequiredXpLevel)
                    {
                        Logging.Error(this.GetType(), "Unable to upgrade the hero. You do not have the required level. Have:" + this.Device.Player.ExpLevel + ", Require:" + this.Hero.RequiredXpLevel + ".");
                        return;
                    }

                    if (!string.IsNullOrEmpty(this.Hero.RequiredQuest))
                    {
                        if (!this.Device.Player.NpcProgress.ContainsKey(this.Hero.RequiredQuestData.GlobalID))
                        {
                            Logging.Error(this.GetType(), "Unable to upgrade the hero. You do not have unlock the required quest.");
                            return;
                        }
                    }

                    string[] Cost = this.Hero.Cost[this.Device.Player.HeroLevels.GetCount(this.Hero.GlobalID)].Split(',');

                    if (Cost.Length >= 7)
                    {
                        int Diamonds = int.Parse(Cost[0]);
                        int Gold     = int.Parse(Cost[1]);
                        int Energy   = int.Parse(Cost[2]);
                        int Orb1     = int.Parse(Cost[3]);
                        int Orb2     = int.Parse(Cost[4]);
                        int Orb3     = int.Parse(Cost[5]);
                        int Orb4     = int.Parse(Cost[6]);

                        if (Diamonds != 0)
                        {
                            if (this.Device.Player.Diamonds < Diamonds)
                            {
                                Logging.Error(this.GetType(), "Unable to upgrade the hero. You do not have enough diamonds. Have:" + this.Device.Player.Diamonds + ", Require:" + Diamonds + ".");
                                return;
                            }
                        }

                        if (Gold != 0)
                        {
                            if (this.Device.Player.Gold < Gold)
                            {
                                Logging.Error(this.GetType(), "Unable to upgrade the hero. You do not have enough gold. Have:" + this.Device.Player.Gold + ", Require:" + Gold + ".");
                                return;
                            }
                        }

                        if (Energy != 0)
                        {
                            if (this.Device.Player.Energy < Energy)
                            {
                                Logging.Error(this.GetType(), "Unable to upgrade the hero. You do not have enough energy. Have:" + this.Device.Player.Energy + ", Require:" + Energy + ".");
                                return;
                            }
                        }
                        
                        if (Orb1 != 0)
                        {
                            if (this.Device.Player.Orb1 < Orb1)
                            {
                                Logging.Error(this.GetType(), "Unable to upgrade the hero. You do not have enough orb1. Have:" + this.Device.Player.Orb1 + ", Require:" + Orb1 + ".");
                                return;
                            }
                        }

                        if (Orb2 != 0)
                        {
                            if (this.Device.Player.Orb2 < Orb2)
                            {
                                Logging.Error(this.GetType(), "Unable to upgrade the hero. You do not have enough orb2. Have:" + this.Device.Player.Orb2 + ", Require:" + Orb2 + ".");
                                return;
                            }
                        }

                        if (Orb3 != 0)
                        {
                            if (this.Device.Player.Orb3 < Orb3)
                            {
                                Logging.Error(this.GetType(), "Unable to upgrade the hero. You do not have enough orb3. Have:" + this.Device.Player.Orb3 + ", Require:" + Orb3 + ".");
                                return;
                            }
                        }

                        if (Orb4 != 0)
                        {
                            if (this.Device.Player.Orb4 < Orb4)
                            {
                                Logging.Error(this.GetType(), "Unable to upgrade the hero. You do not have enough orb4. Have:" + this.Device.Player.Orb4 + ", Require:" + Orb4 + ".");
                                return;
                            }
                        }

                        this.Device.Player.Diamonds -= Diamonds;
                        this.Device.Player.Gold     -= Gold;
                        this.Device.Player.Energy   -= Energy;
                        this.Device.Player.Orb1     -= Orb1;
                        this.Device.Player.Orb2     -= Orb2;
                        this.Device.Player.Orb3     -= Orb3;
                        this.Device.Player.Orb4     -= Orb4;
                    }

                    this.Device.Player.HeroUpgrade.StartUpgrade(this.Hero);
                }
            }
        }
    }
}