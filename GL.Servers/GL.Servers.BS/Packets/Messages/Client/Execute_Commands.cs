namespace GL.Servers.BS.Packets.Messages.Client
{
    using System;
    using System.Collections.Generic;

    using GL.Servers.BS.Core;
    using GL.Servers.BS.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Execute_Commands : Message
    {
        private int Checksum;
        private int Tick;
        private int Count;

        private byte[] Commands;

        private List<Command> Historic;

        /// <summary>
        /// Initializes a new instance of the <see cref="Execute_Commands"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Execute_Commands(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Execute_Commands.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Reader.ReadVInt();

            this.Tick       = this.Reader.ReadVInt();
            this.Checksum   = this.Reader.ReadVInt();
            this.Count      = this.Reader.ReadVInt();

            if (this.Count > 0)
            {
                if (this.Count <= 512)
                {
                    this.Commands = this.Reader.ReadBytes((int)(this.Reader.BaseStream.Length - this.Reader.BaseStream.Position));
                    this.Historic = new List<Command>(this.Count);
                }
                else
                {
                    Logging.Error(this.GetType(), this.Device, "Error when executing commands, the limit has been reached.");
                }
            }
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            if (this.Count > 0 && this.Count <= 512)
            {
                using (Reader Reader = new Reader(this.Commands))
                {
                    for (int i = 0; i < this.Count; i++)
                    {
                        ushort CommandID = (ushort) Reader.ReadVInt();

                        if (Factory.Commands.ContainsKey(CommandID))
                        {
                            Command Command = Activator.CreateInstance(Factory.Commands[CommandID], Reader, this.Device, CommandID) as Command;

                            if (Command != null)
                            {
                                Command.Decode();
                                Command.Process();

                                this.Historic.Add(Command);
                            }
                        }
                        else
                        {
                            Logging.Error(this.GetType(), "Command " + CommandID + " is not handled. [" + (i + 1) + "/" + this.Count + "] " + (this.Historic.Count > 0 ? "Last command was " + this.Historic[i - 1].GetType().Name : string.Empty));
                            break;
                        }
                    }
                }

                this.Historic = null;
            }
        }
    }
}