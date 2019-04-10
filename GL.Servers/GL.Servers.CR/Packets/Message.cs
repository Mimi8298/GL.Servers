namespace GL.Servers.CR.Packets
{
    using System;
    using System.Reflection;
    using System.Collections.Generic;

    using GL.Servers.CR.Core;
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Enums;

    using GL.Servers.DataStream;
    using GL.Servers.Extensions;
    using GL.Servers.Extensions.List;
    using GL.Servers.Extensions.Binary;

    internal class Message
    {
        internal short Version;
        internal int Offset;

        /// <summary>
        /// The device, technically called as 'client'.
        /// </summary>
        internal Device Device;
        
        /// <summary>
        /// The message stream, used to.. read or write the message.
        /// </summary>
        internal ByteStream Data;

        /// <summary>
        /// Gets a value indicating whether this message is server to client message.
        /// </summary>
        internal bool IsServerToClientMessage
        {
            get
            {
                return this.Type >= 20000;
            }
        }

        /// <summary>
        /// Gets the encoding length.
        /// </summary>
        internal int Length
        {
            get
            {
                return this.Data.Length;
            }
        }

        /// <summary>
        /// The type of this message.
        /// </summary>
        internal virtual short Type
        {
            get
            {
                Logging.Info(this.GetType(), "Type must be overridden.");
                return 0;
            }
        }

        /// <summary>
        /// The service node of this message.
        /// </summary>
        internal virtual Node ServiceNode
        {
            get
            {
                Logging.Info(this.GetType(), "Service node must be overridden.");
                return 0;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        internal Message()
        {
            // Message.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        internal Message(Device Device) : this()
        {
            this.Device = Device;
            this.Data   = new ByteStream();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        internal Message(Device Device, ByteStream Reader) : this()
        {
            this.Device = Device;
            this.Data   = Reader;
        }

        /// <summary>
        /// Decodes the <see cref="Message"/>, using the <see cref="Reader"/> instance.
        /// </summary>
        internal virtual void Decode()
        {
            // Decode.
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer"/> instance.
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

                Packet.AddRange(this.Data.ToArray());

                return Packet.ToArray();
            }
        }

        /// <summary>
        /// Debugs this instance.
        /// </summary>
        internal void ShowBuffer(ByteStream Stream)
        {
            Logging.Info(this.GetType(), BitConverter.ToString(Stream.ToArray(Stream.Offset, Stream.Length)));
        }

        /// <summary>
        /// Shows the values.
        /// </summary>
        internal void ShowValues()
        {
            Logging.Info(this.GetType(), string.Empty);

            foreach (FieldInfo Field in this.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (Field != null)
                {
                    Logging.Info(this.GetType(), ConsolePad.Padding(Field.Name) + " = " + ConsolePad.Padding(!string.IsNullOrEmpty(Field.Name) ? (Field.GetValue(this) != null ? Field.GetValue(this).ToString() : "(null)") : "(null)", 40));
                }
            }
        }
    }
}