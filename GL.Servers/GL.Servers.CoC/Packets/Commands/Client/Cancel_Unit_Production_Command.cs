namespace GL.Servers.CoC.Packets.Commands.Client
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Extensions.Helper;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;

    using GL.Servers.Extensions.Binary;

    internal class Cancel_Unit_Production_Command : Command
    {
        internal CharacterData Unit;

        internal int Count;
        internal int Index;

        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 509;
            }
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(Reader Reader)
        {
            Reader.ReadInt32();
            Reader.ReadInt32();

            this.Unit  = Reader.ReadData<CharacterData>(); // Data

            this.Count = Reader.ReadInt32(); // Count
            this.Index = Reader.ReadInt32(); // Index

            this.Decode(Reader);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(Level Level)
        {
            if (this.Unit != null)
            {
                if (this.Count > 0)
                {
                    // TODO Process the command.
                }
            }
        }
    }
}