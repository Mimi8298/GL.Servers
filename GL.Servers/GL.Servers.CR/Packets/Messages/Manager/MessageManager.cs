namespace GL.Servers.CR.Packets.Messages.Manager
{
    using System;
    using System.Net;

    using GL.Servers.CR.Core;
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Packets.Crypto;
    using GL.Servers.CR.Packets.Crypto.Encrypter;
    using GL.Servers.CR.Packets.Crypto.Init;

    using GL.Servers.DataStream;
    using GL.Servers.Extensions;

    internal class MessageManager
    {
        internal Device Device;

        internal int Ping;
        internal int AccountHighId;
        internal int AccountLowId;

        internal string ConnectionInterface;

        internal DateTime LastKeepAlive;
        internal DateTime LastMessage;
        internal DateTime Session;

        internal PepperInit PepperInit;
        internal IEncrypter SendEncrypter;
        internal IEncrypter ReceiveEncrypter;

        internal IPEndPoint UdpEndPoint;

        /// <summary>
        /// Gets the account identifier.
        /// </summary>
        internal long AccountID
        {
            get
            {
                return (long) this.AccountHighId << 32 | (uint) this.AccountLowId;
            }
        }

        /// <summary>
        /// Gets the time since last keep alive in ms.
        /// </summary>
        internal long TimeSinceLastKeepAliveMs
        {
            get
            {
                return (long) DateTime.UtcNow.Subtract(this.LastKeepAlive).TotalMilliseconds;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageManager"/> class.
        /// </summary>
        internal MessageManager(Device Device)
        {
            this.Device         = Device;

            this.Session        = DateTime.UtcNow;
            this.LastMessage    = DateTime.UtcNow;
            this.LastKeepAlive  = DateTime.UtcNow;
        }

        /// <summary>
        /// Receives the message.
        /// </summary>
        internal void ReceiveMessage(short Type, short Version, byte[] Packet)
        {
            if (this.ReceiveEncrypter == null)
            {
                if (this.PepperInit.State == 0)
                {
                    if (Type == 10101)
                    {
                        this.SendEncrypter = new RC4Encrypter("fhsd6f86f67rt8fw78fw789we78r9789wer6re", "nonce");
                        this.ReceiveEncrypter = new RC4Encrypter("fhsd6f86f67rt8fw78fw789we78r9789wer6re", "nonce");

                        Packet = this.ReceiveEncrypter.Decrypt(Packet);
                    }
                    else if (Type == 10100)
                    {
                        Packet = PepperCrypto.HandlePepperAuthentification(ref this.PepperInit, Packet);
                    }
                    else
                        Packet = null;
                }
                else
                {
                    if (this.PepperInit.State == 2)
                    {
                        if (Type == 10101)
                        {
                            Packet = PepperCrypto.HandlePepperLogin(ref this.PepperInit, Packet);
                        }
                        else
                            Packet = null;
                    }
                    else
                        Packet = null;
                }
            }
            else
                Packet = this.ReceiveEncrypter.Decrypt(Packet);

            if (Packet != null)
            {
                Message Message = Factory.CreateMessage(Type, this.Device, new ByteStream(Packet));

                if (Message != null)
                {
                    try
                    {
                        Message.Decode();
                        Message.Process();
                    }
                    catch (Exception Exception)
                    {
                        Logging.Error(this.GetType(), "ReceiveMessage() - An error has been throwed when the message type " + Message.Type + " has been processed. " + Exception);
                    }
                }
            }
        }

        /// <summary>
        /// Receives a udp message.
        /// </summary>
        internal void ReceiveUdpMessage(byte[] Packet)
        {
            // TODO Implement MessageManager::ReceiveUdpMessage().
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        internal void SendMessage(Message Message)
        {
            if (Message.Device.Connected)
            {
                if (Message.IsServerToClientMessage)
                {
                    byte[] Bytes = Message.Data.ToArray();

                    if (this.SendEncrypter == null)
                    {
                        if (this.PepperInit.State > 0)
                        {
                            if (this.PepperInit.State == 1)
                            {
                                Bytes = PepperCrypto.SendPepperAuthentificationResponse(ref this.PepperInit, Bytes);
                            }
                            else
                            {
                                if (this.PepperInit.State == 3)
                                {
                                    Bytes = PepperCrypto.SendPepperLoginResponse(ref this.PepperInit, out this.SendEncrypter, out this.ReceiveEncrypter, Bytes);
                                }
                            }
                        }
                    }
                    else
                        Bytes = this.SendEncrypter.Encrypt(Bytes);

                    Message.Data.SetByteArray(Bytes);

                    Resources.TCPGateway.Send(Message);
                    Logging.Info(this.GetType(), "Packet " + ConsolePad.Padding(Message.GetType().Name) + "    sent to    " + Message.Device.Socket.RemoteEndPoint + ".");
                }
                else 
                    Logging.Info(this.GetType(), "SendMessage() - Trying to send a client to server message. (" + Message.Type + ")");
            }
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        internal void SendUdpMessage(Message Message)
        {
            // TODO Implement MessageManager::sendUdpMessage().
        }

        /// <summary>
        /// Called when a keep alive message has been received.
        /// </summary>
        internal void KeepAliveMessageReceived()
        {
            this.LastKeepAlive = DateTime.UtcNow;
        }
    }
}