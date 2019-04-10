namespace GL.Servers.BU.Packets.Messages.Client
{
    using System;

    using GL.Servers.BU.Core.Network;
    using GL.Servers.BU.Extensions.Binary;
    using GL.Servers.BU.Logic;
    using GL.Servers.BU.Logic.Enums;
    using GL.Servers.BU.Packets.Messages.Server;

    internal class Authentification_Session : Message
    {
        internal int HighID;
        internal int LowID;

        internal string GamcenterID;

        /// <summary>
        /// Initializes a new instance of the <see cref="Authentification_Session"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Authentification_Session(Device Device, Reader Reader) : base(Device, Reader)
        {
            this.Device.State = State.LOGIN;
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            // 00-00-50-06
            // 00-00-00-01
            // 00-00-00-00
            // 00-00-50-06

            // 00-00-00-00
            // 00-00-00-00
            // 00-00-00-00
            // 00-00-00-00

            // 00-00-00-0B-47-3A-33-32-35-33-37-38-36-37-31

            this.Reader.ReadInt32();

            this.HighID = this.Reader.ReadInt32();
            this.LowID  = this.Reader.ReadInt32();

            this.Reader.ReadInt32();

            this.Reader.ReadInt32();
            this.Reader.ReadInt32();
            this.Reader.ReadInt32();
            this.Reader.ReadInt32();

            this.GamcenterID = this.Reader.ReadString();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            Console.WriteLine(this.GetType().Name + " : " + this.HighID + "-" + this.LowID);

            new Authentification_OK(this.Device).Send();
        }
    }
}