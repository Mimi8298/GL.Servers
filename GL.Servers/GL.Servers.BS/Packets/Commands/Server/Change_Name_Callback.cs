namespace GL.Servers.BS.Packets.Commands.Server
{
    using GL.Servers.BS.Logic;

    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;

    internal class Change_Name_Callback : Command
    {
        private string Name;
        private int State;

        /// <summary>
        /// Initializes a new instance of the <see cref="Change_Name_Callback"/> class.
        /// </summary>
        /// <param name="Reader">The reader.</param>
        /// <param name="Device">The device.</param>
        /// <param name="Identifier">The identifier.</param>
        public Change_Name_Callback(Reader Reader, Device Device, int Identifier) : base(Reader, Device, Identifier)
        {
            // Change_Name_Callback.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Change_Name"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Name">The name.</param>
        /// <param name="State">The state.</param>
        public Change_Name_Callback(Device Device, string Name, int State) : base(Device)
        {
            this.Identifier = 201;
            this.Name       = Name;
            this.State      = State;
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddString(this.Name);
            this.Data.AddVInt(this.State);
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode()
        {
            this.Name   = this.Reader.ReadString();
            this.State  = this.Reader.ReadVInt();
        }
    }
}