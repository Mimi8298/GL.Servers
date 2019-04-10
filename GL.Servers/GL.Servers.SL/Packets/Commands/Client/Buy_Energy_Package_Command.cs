namespace GL.Servers.SL.Packets.Commands.Client
{
    using GL.Servers.Extensions.Binary;
    using GL.Servers.SL.Core;
    using GL.Servers.SL.Extensions;
    using GL.Servers.SL.Logic;
    using GL.Servers.SL.Files.CSV_Logic.Logic;
    using GL.Servers.SL.Logic.Avatar.Items;

    internal class Buy_Energy_Package_Command : Command
    {
        internal EnergyPackageData EnergyPackage;

        internal override int Type
        {
            get
            {
                return 524;
            }
        }

        public Buy_Energy_Package_Command(Device Device) : base(Device)
        {
            
        }

        internal override void Decode(Reader Reader)
        {
            this.EnergyPackage = Reader.ReadData<EnergyPackageData>();
            this.ReadHeader(Reader);
        }

        internal override void Process()
        {
            if (this.EnergyPackage != null)
            {
                int AlreadyBuyed = this.Device.Player.EnergyPackages.GetCount(this.EnergyPackage.GlobalID);

                if (this.EnergyPackage.Diamonds.Count > AlreadyBuyed)
                {
                    int Cost = this.EnergyPackage.Diamonds[AlreadyBuyed];

                    if (Cost > 0)
                    {
                        if (this.Device.Player.Diamonds < Cost)
                        {
                            Logging.Error(this.GetType(), "Unable to buy a energy package. You do not enough diamonds. Have:" + this.Device.Player.Diamonds + ", Require:" + Cost + ".");
                            return;
                        }
                    }

                    this.Device.Player.EnergyPackages.AddItem(this.EnergyPackage.GlobalID, 1);

                    this.Device.Player.Diamonds -= Cost;
                    this.Device.Player.Energy    = this.Device.Player.MaxEnergy;
                }
                else Logging.Error(this.GetType(), "Unable to buy a energy package. You have already buy all packages.");
            }
            else Logging.Error(this.GetType(), "Unable to buy a energy package. The package data not exist or not valid.");
        }
    }
}