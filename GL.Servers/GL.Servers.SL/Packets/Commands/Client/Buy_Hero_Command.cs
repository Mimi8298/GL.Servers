namespace GL.Servers.SL.Packets.Commands.Client
{
    using GL.Servers.Extensions.Binary;
    using GL.Servers.SL.Extensions;
    using GL.Servers.SL.Logic;
    using GL.Servers.SL.Files.CSV_Logic.Logic;

    internal class Buy_Hero_Command : Command
    {
        internal HeroData Hero;

        internal override int Type
        {
            get
            {
                return 501;
            }
        }

        public Buy_Hero_Command(Device Device) : base(Device)
        {
            
        }

        internal override void Decode(Reader Reader)
        {
            this.Hero = Reader.ReadData<HeroData>();
            this.ReadHeader(Reader);
        }

        internal override void Process()
        {
            base.Process();
        }
    }
}