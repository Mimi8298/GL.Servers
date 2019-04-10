namespace GL.Clients.CoC.Core.Network
{
    using System;
    using System.Diagnostics;
    using System.Net.Sockets;

    using GL.Clients.CoC.Logic;
    using GL.Clients.CoC.Packets;

    internal class Gateway
    {
        private Device Device;

        private int Offset;

        /// <summary>
        /// Initializes a new instance of the <see cref="Gateway"/> class.
        /// </summary>
        internal Gateway()
        {
            // Gateway.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Gateway"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        internal Gateway(Device Device) : this()
        {
            this.Device = Device;
            this.Device.Token.Args.Completed += this.OnReceiveCompleted;
            this.Device.Token.Args.SetBuffer(new byte[Constants.ReceiveBuffer], 0, Constants.ReceiveBuffer);
        }

        /// <summary>
        /// Receives this instance.
        /// </summary>
        internal void Receive()
        {
            if (!this.Device.Socket.ReceiveAsync(this.Device.Token.Args))
            {
                this.ProcessReceive(this.Device.Token.Args);
            }
        }

        /// <summary>
        /// Receives data from the specified client.
        /// </summary>
        /// <param name="AsyncEvent">The <see cref="SocketAsyncEventArgs"/> instance containing the event data.</param>
        internal void ProcessReceive(SocketAsyncEventArgs AsyncEvent)
        {
            if (AsyncEvent.BytesTransferred > 0 && AsyncEvent.SocketError == SocketError.Success)
            {
                Token Token = AsyncEvent.UserToken as Token;

                if (!Token.Aborting)
                {
                    Token.SetData();

                    try
                    {
                        if (Token.Device.Socket.Available == 0)
                        {
                            Token.Process();

                            if (!Token.Aborting)
                            {
                                if (!Token.Device.Socket.ReceiveAsync(AsyncEvent))
                                {
                                    this.ProcessReceive(AsyncEvent);
                                }
                            }
                        }
                        else
                        {
                            if (!Token.Device.Socket.ReceiveAsync(AsyncEvent))
                            {
                                this.ProcessReceive(AsyncEvent);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Debug.WriteLine("[*] Warning : We got disconnected by the server !");
                    }
                }
            }
            else
            {
                Debug.WriteLine("[*] Warning : We got disconnected by the server !");
            }
        }

        /// <summary>
        /// Called when [receive completed].
        /// </summary>
        /// <param name="Sender">The sender.</param>
        /// <param name="AsyncEvent">The <see cref="SocketAsyncEventArgs"/> instance containing the event data.</param>
        internal void OnReceiveCompleted(object Sender, SocketAsyncEventArgs AsyncEvent)
        {
            if (AsyncEvent.SocketError == SocketError.Success)
            {
                this.ProcessReceive(AsyncEvent);
            }
            else
            {
                Debug.WriteLine("[*] Warning : We got disconnected by the server !");
            }
        }

        /// <summary>
        /// Sends this instance.
        /// </summary>
        internal void Send(Message Message)
        {
            if (Message != null)
            {
                Message.Encode();
                Message.Encrypt();

                Debug.WriteLine("[*] We are sending the following message : ID " + Message.Identifier + ", Length " + Message.Length + ", Version " + Message.Version + ".");

                byte[] Buffer = Message.ToBytes;

                if (this.Device.Connected)
                {
                    this.Device.Socket.BeginSend(Buffer, 0, Buffer.Length, 0, this.SendCallback, Message);
                }
                else
                {
                    Debug.WriteLine("[*] Warning : Aborting the send process because we are disconnected !");
                }
            }
            else
            {
                Debug.WriteLine("[*] Warning : Message was null at send !");
            }
        }

        private void SendCallback(IAsyncResult AsyncResult)
        {
            Message Message = AsyncResult.AsyncState as Message;

            if (Message != null)
            {
                if (!this.Device.Token.Aborting)
                {
                    int BytesSent = this.Device.Socket.EndSend(AsyncResult);

                    if (BytesSent < Message.Length + 7)
                    {
                        Debug.WriteLine("[*] Warning : We still have bytes to send !");
                    }
                }
            }
            else
            {
                Debug.WriteLine("[*] Warning : Message was null at send callback !");
            }
        }
    }
}