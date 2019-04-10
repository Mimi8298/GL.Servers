namespace GL.Servers.CoC.Packets.Commands.Client
{
    using GL.Servers.Extensions.Binary;

    internal class New_Shop_Seen_Command : Command
    {
        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 532;
            }
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(Reader Reader)
        {
            Reader.ReadInt32();
            Reader.ReadInt32();
            Reader.ReadInt32();

            base.Decode(Reader);
        }
    }
}