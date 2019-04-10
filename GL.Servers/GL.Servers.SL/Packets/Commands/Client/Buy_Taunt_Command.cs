namespace GL.Servers.SL.Packets.Commands.Client
{
    using GL.Servers.Extensions.Binary;
    using GL.Servers.SL.Core;
    using GL.Servers.SL.Files.CSV_Helpers;
    using GL.Servers.SL.Extensions;
    using GL.Servers.SL.Files;
    using GL.Servers.SL.Files.CSV_Logic.Logic;
    using GL.Servers.SL.Logic;
    using GL.Servers.SL.Logic.Avatar.Items;
    using GL.Servers.SL.Logic.Enums;

    internal class Buy_Taunt_Command : Command
    {
        internal int DataType;
        internal Data Data;

        internal override int Type
        {
            get
            {
                return 513;
            }
        }

        public Buy_Taunt_Command(Device Device) : base(Device)
        {
            
        }

        internal override void Decode(Reader Reader)
        {
            this.DataType = Reader.ReadInt32();
            this.Data = Reader.ReadData();
            Reader.ReadInt32();
            this.ReadHeader(Reader);
        }

        internal override void Process()
        {
            if (this.Data != null)
            {
                if (this.DataType == 0)
                {
                    TauntData Taunt = (TauntData) this.Data;

                    if (!this.Device.Player.Extras.ContainsKey(Taunt.GlobalID))
                    {
                        if (!string.IsNullOrEmpty(Taunt.CharacterUnlock))
                        {
                            HeroData Hero = (HeroData) CSV.Tables.Get(Gamefile.Heroes).GetData(Taunt.CharacterUnlock);

                            if (!this.Device.Player.HeroLevels.ContainsKey(Hero.GlobalID))
                            {
                                Logging.Error(this.GetType(), "Unable to buy the taunt. Avatar doesn't unlock the hero " + Taunt.CharacterUnlock + ". AvatarId:" + this.Device.Player + ".");
                                return;
                            }
                        }

                        if (Taunt.Cost > 0)
                        {
                            ResourceData Resource = (ResourceData) CSV.Tables.Get(Gamefile.Resources).GetData(Taunt.Resource);

                            if (this.Device.Player.Resources.GetCount(Resource.GlobalID) < Taunt.Cost)
                            {
                                Logging.Error(this.GetType(), "Unable to buy the taunt. You do not have enough " + Taunt.Resource + ". Have:" + this.Device.Player.Resources.GetCount(Resource.GlobalID) + ", Require:" + Taunt.Cost + ".");
                                return;
                            }

                            this.Device.Player.Resources.Remove(Resource.GlobalID, Taunt.Cost);
                        }

                        this.Device.Player.Extras.Set(Taunt.GlobalID, 1);
                    }
                }
                else
                {
                    DecoData Deco = (DecoData) this.Data;

                    if (!this.Device.Player.Extras.ContainsKey(Deco.GlobalID))
                    {
                        if (Deco.UnlockLevel > this.Device.Player.ExpLevel)
                        {
                            Logging.Error(this.GetType(), "Unable to buy the deco. You do not have the required level. AvatarId:" + this.Device.Player + ".");
                            return;
                        }

                        if (Deco.Cost > 0)
                        {
                            ResourceData Resource = (ResourceData) CSV.Tables.Get(Gamefile.Resources).GetData(Deco.Resource);

                            if (this.Device.Player.Resources.GetCount(Resource.GlobalID) < Deco.Cost)
                            {
                                Logging.Error(this.GetType(), "Unable to buy the deco. You do not have enough " + Deco.Resource + ". Have:" + this.Device.Player.Resources.GetCount(Resource.GlobalID) + ", Require:" + Deco.Cost + ".");
                                return;
                            }

                            this.Device.Player.Resources.Remove(Resource.GlobalID, Deco.Cost);
                        }

                        this.Device.Player.Extras.Set(Deco.GlobalID, 2);

                        if (this.Device.Player.Extras.Count > 1)
                        {
                            foreach (Item Item in this.Device.Player.Extras.Values)
                            {
                                if (Item.Id / 1000000 == Deco.GetDataType())
                                {
                                    if (Item.Count != 1 && Item.Id != Deco.GlobalID)
                                    {
                                        Item.Count = 1;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (this.Device.Player.Extras.GetCount(Deco.GlobalID) == 1)
                        {
                            this.Device.Player.Extras.Set(Deco.GlobalID, 2);

                            foreach (Item Item in this.Device.Player.Extras.Values)
                            {
                                if (Item.Id / 1000000 == Deco.GetDataType())
                                {
                                    if (Item.Count != 1 && Item.Id != Deco.GlobalID)
                                    {
                                        Item.Count = 1;
                                    }
                                }
                            }
                        }
                        else this.Device.Player.Extras.Set(Deco.GlobalID, 1);
                    }
                }
            }
            else Logging.Error(this.GetType(), "Unable to buy the extra. Data is null.");
        }
    }
}