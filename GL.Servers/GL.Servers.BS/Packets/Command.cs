namespace GL.Servers.BS.Packets
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using GL.Servers.BS.Core;
    using GL.Servers.BS.Logic;
    using GL.Servers.Extensions;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;

    internal class Command
    {
        internal int Identifier;
        internal int CommandID;

        internal int TickWhenGiven = -1;
        internal int ExecuteTick = -1;

        internal int ExecutorHighID;
        internal int ExecutorLowID;

        internal Reader Reader;
        internal Device Device;

        internal List<byte> Data;

        internal bool isServerCommand
        {
            get
            {
                return this.Identifier >= 200 && this.Identifier < 300;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        internal Command(Device Device)
        {
            this.Device     = Device;
            this.Data       = new List<byte>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="Reader">The reader.</param>
        /// <param name="Device">The device.</param>
        /// <param name="Identifier">The identifier.</param>
        internal Command(Reader Reader, Device Device, int Identifier)
        {
            this.Identifier = Identifier;
            this.Device     = Device;
            this.Reader     = Reader;

            this.Data       = new List<byte>();
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal virtual void Decode()
        {
            // Decode.
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal virtual void Encode()
        {
            // Encode.
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal virtual void Process()
        {
            // Process.
        }

        /// <summary>
        /// Reads the header.
        /// </summary>
        internal void ReadHeader()
        {
            if (this.isServerCommand)
            {
                this.CommandID = this.Reader.ReadVInt();
            }

            this.TickWhenGiven = this.Reader.ReadVInt();
            this.ExecuteTick   = this.Reader.ReadVInt();

            this.ExecutorHighID = this.Reader.ReadVInt();
            this.ExecutorLowID  = this.Reader.ReadVInt();


            if (this.TickWhenGiven == -1)
            {
                if (this.Identifier < 200 || this.Identifier >= 500)
                {
                    Logging.Error(this.GetType(), this.Device, $"Command's (type = {this.Identifier}) tickWhenGiven is not set");
                }
            }
        }

        internal void EncodeHeader()
        {
            if (this.isServerCommand)
            {
                this.Data.AddVInt(this.CommandID);
            }

            this.Data.AddVInt(this.TickWhenGiven);
            this.Data.AddVInt(this.ExecuteTick);

            this.Data.AddVInt(this.ExecutorHighID);
            this.Data.AddVInt(this.ExecutorLowID);
        }

        /// <summary>
        /// Debugs this instance.
        /// </summary>
        internal void Debug()
        {
            System.Diagnostics.Debug.WriteLine(this.GetType().Name + " : " + BitConverter.ToString(this.Reader.ReadBytes((int) (this.Reader.BaseStream.Length - this.Reader.BaseStream.Position))));
        }

        /// <summary>
        /// Shows the values.
        /// </summary>
        internal void ShowValues()
        {
            Console.WriteLine(Environment.NewLine);

            foreach (FieldInfo Field in this.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (Field != null)
                {
                    Console.WriteLine(ConsolePad.Padding(this.GetType().Name) + " - " + ConsolePad.Padding(Field.Name) + " : " + ConsolePad.Padding(!string.IsNullOrEmpty(Field.Name) ? (Field.GetValue(this) != null ? Field.GetValue(this).ToString() : "(null)") : "(null)", 40));
                }
            }
        }

        /// <summary>
        /// Logs this instance.
        /// </summary>
        internal void Log()
        {
            File.AppendAllText(Directory.GetCurrentDirectory() + "\\Logs\\" + this.GetType().Name + ".log", BitConverter.ToString(this.Reader.ReadBytes((int)(this.Reader.BaseStream.Length - this.Reader.BaseStream.Position))) + Environment.NewLine);
        }

        internal byte[] ToBytes
        {
            get
            {
                List<byte> Packet = new List<byte>();

                Packet.AddVInt(this.Identifier);
                Packet.AddRange(this.Data);

                return Packet.ToArray();
            }
        }
    }
}