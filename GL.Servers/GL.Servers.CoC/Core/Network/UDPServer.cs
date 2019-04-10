namespace GL.Servers.CoC.Core.Network
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    public class UDPServer
    {
        internal UdpClient Server;
        internal Thread Thread;
        internal IPEndPoint Endpoint;

        internal ManualResetEvent Event;

        /// <summary>
        /// Initializes a new instance of the <see cref="UDPServer"/> class.
        /// </summary>
        public UDPServer()
        {
            this.Thread             = new Thread(this.Listen);
            this.Endpoint           = new IPEndPoint(IPAddress.Any, 9339);
            this.Server             = new UdpClient(this.Endpoint);
            this.Event              = new ManualResetEvent(false);

            Console.WriteLine("UDP Server is listening on " + this.Endpoint + ".");

            this.Thread.Start();
        }

        /// <summary>
        /// Listens this instance.
        /// </summary>
        internal void Listen()
        {
            while (true)
            {
                this.Event.Reset();
                this.Server.BeginReceive(this.ProcessAccept, null);
                this.Event.WaitOne();
            }
        }

        /// <summary>
        /// Begins to accept the client.
        /// </summary>
        /// <param name="AsyncResult">The asynchronous result.</param>
        internal void ProcessAccept(IAsyncResult AsyncResult)
        {
            IPEndPoint Device   = null;
            byte[] Buffer       = this.Server.EndReceive(AsyncResult, ref Device);

            this.Event.Set();

            Console.WriteLine("[*] " + this.GetType().Name + " : " + BitConverter.ToString(Buffer));

            this.ProcessPacket(Buffer, Device);
        }

        /// <summary>
        /// Processes the packet.
        /// </summary>
        /// <param name="Buffer">The buffer.</param>
        internal void ProcessPacket(byte[] Buffer, IPEndPoint Socket)
        {
            /* using (Reader Reader = new Reader(Buffer))
            {
                long BattleID     = (long)Reader.ReadByte() << 32 | (long)(uint)Reader.ReadInt32();
                long PlayerID     = (long)Reader.ReadByte() << 32 | (long)(uint)Reader.ReadInt32();

                ushort PacketID   = (ushort) Reader.ReadVInt();
                uint Length       = Reader.ReadVUInt();

                if (Resources.Battles.ContainsKey(BattleID))
                {
                    if (Resources.Battles[BattleID].Equip.ContainsKey(PlayerID))
                    {
                        Player Sender = Resources.Battles[BattleID].Equip[PlayerID].Player;

                        if (Sender.Device != null)
                        {
                            Sender.Device.UDPSocket = Socket;

                            if (Factory.Messages.ContainsKey(PacketID))
                            {
                                Message Message = Activator.CreateInstance(Factory.Messages[PacketID], Sender.Device, new Reader(Reader.ReadBytes((int)Length))) as Message;

                                Message.Identifier = PacketID;
                                Message.Length = Length;

                                try
                                {
                                    Message.Decode();
                                    Message.Process();
                                }
                                catch (Exception Exception)
                                {
                                    Resources.Sentry.Catch(Exception, Sender.Device.Model, Sender.Device.OS, Sender.Device.OSVersion);
                                    Resources.Logger.Error(Exception, "We got an error (" + Exception.GetType().Name + ") when handling the following message : ID " + PacketID + ", Length " + Length + ".");

                                    System.Diagnostics.Debug.WriteLine(ConsolePad.Padding(Exception.GetType().Name, 15) + " : " + Exception.Message + ". [" + (Sender.Device.Player != null ? Sender.Device.Player.HighID + ":" + Sender.Device.Player.LowID : "---") + ']' + Environment.NewLine + Exception.StackTrace);
                                }
                            }
                            else
                            {
                                Resources.Logger.Debug(this.GetType().Name + " : " + "We can't handle the following message : ID " + PacketID + ", Length " + Length + ".");
                            }
                        }
                    }
                    else Console.WriteLine($"Player {PlayerID} not exist");
                }
                else Console.WriteLine($"Battle {BattleID} not exist");

                if (Reader.BaseStream.Length > 10)
                {
                    System.Diagnostics.Debug.WriteLine("[*] " + this.GetType().Name + " : " + BitConverter.ToString(Reader.ReadBytes((int) (Reader.BaseStream.Length - Reader.BaseStream.Position))));
                }
            } */
        }
    }
}