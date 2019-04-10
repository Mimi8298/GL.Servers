namespace GL.Proxy.BB.Core.Network
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;

    using GL.Proxy.BB.Logic;
    using GL.Proxy.BB.Logic.Enums;

    using GL.Servers.Extensions;
    using GL.Servers.Extensions.Binary;

    public class Packet
    {
        public Device Device;
        public Socket Client;

        public Destination Destination;

        public int Identifier;
        public int Version;
        public int Length;

        public byte[] Payload;
        public byte[] Encrypted_Data;
        public byte[] Decrypted_Data;

        public string Name;

        /// <summary>
        /// Initializes a new instance of the <see cref="Packet"/> class.
        /// </summary>
        /// <param name="Buffer">The buffer.</param>
        /// <param name="Destination">The destination.</param>
        /// <param name="Client">The client.</param>
        public Packet(byte[] Buffer, Destination Destination, Socket Client)
        {
            this.Client             = Client;
            this.Device             = Resources.Devices[Client.Handle];

            using (Reader PacketReader = new Reader(Buffer))
            {
                this.Destination    = Destination;

                this.Identifier     = PacketReader.ReadUInt16();
                this.Length         = PacketReader.ReadInt24();
                this.Version        = PacketReader.ReadUInt16();
                
                this.Payload        = PacketReader.ReadBytes(this.Length);
            }

            this.Name               = PacketType.GetName(this.Identifier);

            Logging.Info(this.GetType(), "Processing packet (" + ConsolePad.Padding(this.Name, 20) + " | " + this.Identifier + ") " + Destination.ToString().Replace("_", " ").ToLower() + ", with version " + this.Version + ".");

            this.Decrypted_Data     = this.Device.EnDecrypt.Decrypt(this);
            this.Encrypted_Data     = this.Device.EnDecrypt.Encrypt(this);

            File.AppendAllText("Logs\\" + ((IPEndPoint) this.Client.RemoteEndPoint).Address + "\\" + this.Name + "_" + this.Identifier + ".bin", BitConverter.ToString(this.RebuiltDecrypted) + Environment.NewLine);
        }

        /// <summary>
        /// Raw, re-encrypted packet (header included) 7 byte header + n byte payload Reverse()
        /// because of little endian byte order
        /// </summary>
        public byte[] Rebuilt
        {
            get
            {
                List<byte> Packet = new List<byte>();

                Packet.AddRange(BitConverter.GetBytes(this.Identifier).Reverse().Skip(2));
                Packet.AddRange(BitConverter.GetBytes(this.Payload.Length).Reverse().Skip(1));
                Packet.AddRange(BitConverter.GetBytes(this.Version).Reverse().Skip(2));
                Packet.AddRange(this.Payload);

                return Packet.ToArray();
            }
        }

        /// <summary>
        /// Raw, re-encrypted packet (header included) 7 byte header + n byte payload Reverse()
        /// because of little endian byte order
        /// </summary>
        public byte[] RebuiltEncrypted
        {
            get
            {
                List<byte> Packet = new List<byte>();

                Packet.AddRange(BitConverter.GetBytes(this.Identifier).Reverse().Skip(2));
                Packet.AddRange(BitConverter.GetBytes(this.Encrypted_Data.Length).Reverse().Skip(1));
                Packet.AddRange(BitConverter.GetBytes(this.Version).Reverse().Skip(2));
                Packet.AddRange(this.Encrypted_Data);

                return Packet.ToArray();
            }
        }

        /// <summary>
        /// Raw, decrypted packet (header included) 7 byte header + n byte payload Reverse()
        /// because of little endian byte order
        /// </summary>
        public byte[] RebuiltDecrypted
        {
            get
            {
                List<byte> Packet = new List<byte>();

                Packet.AddRange(BitConverter.GetBytes(this.Identifier).Reverse().Skip(2));
                Packet.AddRange(BitConverter.GetBytes(this.Decrypted_Data.Length).Reverse().Skip(1));
                Packet.AddRange(BitConverter.GetBytes(this.Version).Reverse().Skip(2));
                Packet.AddRange(this.Decrypted_Data);

                return Packet.ToArray();
            }
        }
    }
}