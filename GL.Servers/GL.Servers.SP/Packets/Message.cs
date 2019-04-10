namespace GL.Servers.SP.Packets
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    using GL.Servers.SP.Core;
    using GL.Servers.SP.Logic;
    using GL.Servers.SP.Packets.Enums;
    using GL.Servers.Extensions;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;

    internal class Message
    {
        internal int Length;
        internal short Version;

        internal int Offset;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal virtual short Type
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// The device, technically called as 'client'.
        /// </summary>
        internal Device Device;

        /// <summary>
        /// The message reader, used to.. read the message.
        /// </summary>
        internal Reader Reader;

        /// <summary>
        /// The message writer, used to.. write the message.
        /// </summary>
        internal ByteWriter Data;

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        internal Message(Device Device)
        {
            this.Device = Device;
            this.Data   = new ByteWriter();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        internal Message(Device Device, Reader Reader)
        {
            this.Device = Device;
            this.Reader = Reader;
        }

        /// <summary>
        /// Gets or sets the packet data, in/from a byte array.
        /// </summary>
        /// <returns>The packet data, in a byte array, header included.</returns>
        internal byte[] ToBytes
        {
            get
            {
                List<byte> Packet = new List<byte>();

                Packet.AddShort(this.Type);
                Packet.AddUInt24(this.Length);
                Packet.AddShort(this.Version);
                Packet.AddRange(this.Data.ToBytes);

                return Packet.ToArray();
            }
        }

        internal virtual ServiceNode Node
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Decodes the <see cref="Message"/>, using the <see cref="Reader"/> instance.
        /// </summary>
        internal virtual void Decode()
        {
            // Trace.WriteLine("[*] " + this.GetType().Name + " : " + "Decode.");
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer"/> instance.
        /// </summary>
        internal virtual void Encode()
        {
            // Trace.WriteLine("[*] " + this.GetType().Name + " : " + "Encode.");
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal virtual void Process()
        {
            // Trace.WriteLine("[*] " + this.GetType().Name + " : " + "Process.");
        }
        
        /// <summary>
        /// Encrypts the message, using the Sodium cryptography.
        /// </summary>
        internal virtual void Encrypt()
        {
            byte[] Packet = this.Data.ToBytes;

            this.Device.RC4.Encrypt(ref Packet);

            this.Data.Clear();
            this.Data.AddRange(Packet);

            this.Length = this.Data.Length;
        }

        /// <summary>
        /// Debugs this instance.
        /// </summary>
        internal void ShowBuffer()
        {
            Logging.Info(this.GetType(), BitConverter.ToString(this.Reader.ReadBytes((int) (this.Reader.BaseStream.Length - this.Reader.BaseStream.Position))));
        }

        /// <summary>
        /// Shows the values.
        /// </summary>
        internal void ShowValues()
        {
            foreach (FieldInfo Field in this.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (Field != null)
                {
                    Logging.Info(this.GetType(), ConsolePad.Padding(Field.Name) + " : " + ConsolePad.Padding(!string.IsNullOrEmpty(Field.Name) ? (Field.GetValue(this) != null ? Field.GetValue(this).ToString() : "(null)") : "(null)", 40));
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
    }
}