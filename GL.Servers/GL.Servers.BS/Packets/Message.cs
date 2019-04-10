namespace GL.Servers.BS.Packets
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Security.Cryptography;

    using GL.Servers.BS.Core;
    using GL.Servers.BS.Logic;

    using GL.Servers.Extensions;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;
    using GL.Servers.Logic.Enums;

    internal class Message
    {
        internal ushort Identifier;
        internal uint Length;
        internal ushort Version;

        internal int Offset;

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
        internal List<byte> Data;

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        internal Message(Device Device)
        {
            this.Device = Device;
            this.Data   = new List<byte>(Constants.SendBuffer);
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

                Packet.AddUShort(this.Identifier);
                Packet.AddUInt24(this.Length);
                Packet.AddUShort(this.Version);
                Packet.AddRange(this.Data);

                return Packet.ToArray();
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
        /// Decrypts the message, using the Sodium cryptography.
        /// </summary>
        /// <exception cref="CryptographicException">We tried to decrypt an incomplete message.</exception>
        internal virtual void Decrypt()
        {
            if (this.Device.State >= State.LOGIN)
            {
                byte[] Packet       = this.Reader.ReadBytes((int) this.Length);

                this.Device.Crypto.Decrypt(ref Packet);

                this.Reader         = new Reader(Packet);
                this.Length         = (uint) this.Reader.BaseStream.Length;
            }
        }

        /// <summary>
        /// Encrypts the message, using the Sodium cryptography.
        /// </summary>
        internal virtual void Encrypt()
        {
            if (this.Device.State >= State.LOGIN)
            {
                byte[] Packet       = this.Data.ToArray();

                this.Device.Crypto.Encrypt(ref Packet);

                this.Data.Clear();
                this.Data.AddRange(Packet);
            }

            this.Length = (uint) this.Data.Count;
        }

        /// <summary>
        /// Debugs this instance.
        /// </summary>
        internal void ShowBuffer()
        {
            Logging.Error(this.GetType(), BitConverter.ToString(this.Reader.ReadBytes((int) (this.Reader.BaseStream.Length - this.Reader.BaseStream.Position))));
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
                    Logging.Error(this.GetType(), ConsolePad.Padding(Field.Name) + " : " + ConsolePad.Padding(!string.IsNullOrEmpty(Field.Name) ? (Field.GetValue(this) != null ? Field.GetValue(this).ToString() : "(null)") : "(null)", 40));
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