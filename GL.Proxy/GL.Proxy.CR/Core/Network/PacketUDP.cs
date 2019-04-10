namespace GL.Proxy.CR.Core.Network
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using GL.Proxy.CR.Logic;
    using GL.Servers.Extensions;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;

    public class PacketUDP
    {
        private int Type;
        private byte[] Data;

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketUDP"/> class.
        /// </summary>
        public PacketUDP(byte[] Buffer, Device Device)
        {
            if (Buffer.Length > 0)
            {
                using (Reader Reader = new Reader(Buffer))
                {
                    this.Type = Reader.ReadVInt();
                    this.Data = Reader.ReadBytes((int)(Reader.BaseStream.Length - Reader.BaseStream.Position));
                }

                Device.Receive(this.Type, ref this.Data);

                string Name = PacketType.GetName(this.Type);

                Logging.Info(this.GetType(), "Processing packet (" + ConsolePad.Padding(Name, 20) + " | " + this.Type + ") from UDP socket.");
                File.AppendAllText("Logs\\" + ((IPEndPoint) Device.Client.RemoteEndPoint).Address + "\\UDP\\" + Name + "_" + this.Type + ".bin", BitConverter.ToString(this.ToBytes) + Environment.NewLine);
            }
        }

        internal byte[] ToBytes
        {
            get
            {
                List<byte> Packet = new List<byte>(this.Data.Length + 5);
                
                Packet.AddVInt(this.Type);
                Packet.AddRange(this.Data);

                return Packet.ToArray();
            }
        }
    }
}