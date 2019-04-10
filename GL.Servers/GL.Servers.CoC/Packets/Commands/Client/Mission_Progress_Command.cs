namespace GL.Servers.CoC.Packets.Commands.Client
{
    using GL.Servers.CoC.Core;
    using GL.Servers.Extensions.Binary;

    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Extensions.Helper;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;

    internal class Mission_Progress_Command : Command
    {
        internal MissionData Data;

        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 519;
            }
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(Reader Reader)
        {
            this.Data = Reader.ReadData<MissionData>();
            base.Decode(Reader);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(Level Level)
        {
            if (this.Data != null)
            {
                if (Level.MissionManager.CurrentMission != null)
                {
                    Level.MissionManager.CurrentMission.StateChangeConfirmed();
                }
            }
        }
    }
}