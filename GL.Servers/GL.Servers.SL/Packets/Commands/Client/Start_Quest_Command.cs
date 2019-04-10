namespace GL.Servers.SL.Packets.Commands.Client
{
    using GL.Servers.Extensions.Binary;
    using GL.Servers.SL.Extensions;
    using GL.Servers.SL.Logic;
    using GL.Servers.SL.Files.CSV_Logic.Logic;

    internal class Start_Quest_Command : Command
    {
        internal QuestData QuestData;

        internal override int Type
        {
            get
            {
                return 500;
            }
        }

        public Start_Quest_Command(Device Device) : base(Device)
        {
            
        }

        internal override void Decode(Reader Reader)
        {
            this.QuestData = Reader.ReadData<QuestData>();
            this.ReadHeader(Reader);
        }

        internal override void Process()
        {
            if (this.QuestData != null)
            {
                
            }

            base.Process();
        }
    }
}