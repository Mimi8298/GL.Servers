namespace GL.Servers.SL.Packets.Commands.Client
{
    using GL.Servers.Extensions.Binary;
    using GL.Servers.SL.Core;
    using GL.Servers.SL.Logic;

    internal class Move_Character_Command : Command
    {
        internal int DirectionX;
        internal int DirectionY;

        internal bool S, F;

        internal override int Type
        {
            get
            {
                return 600;
            }
        } 

        public Move_Character_Command(Device Device) : base(Device)
        {
            
        }

        internal override void Decode(Reader Reader)
        {
            this.DirectionX = Reader.ReadInt32();
            this.DirectionY = Reader.ReadInt32();

            byte Value = Reader.ReadByte();

            this.S = Value == 1 || Value == 3;
            this.F = Value >= 2;

            this.ReadHeader(Reader);
        }

        internal override void Process()
        {
            Logging.Info(this.GetType(), "DirectionX : " + this.DirectionX + ", DirectionY : " + this.DirectionY + ", S : " + this.S + ", F : " + this.F);
            base.Process();
        }
    }
}