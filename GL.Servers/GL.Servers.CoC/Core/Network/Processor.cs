namespace GL.Servers.CoC.Core.Network
{
    using GL.Servers.CoC.Packets;
    using GL.Servers.Extensions;

    internal static class Processor
    {
        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="Message">The message.</param>
        internal static void Send(this Message Message)
        {
            if (Message.Device.Connected)
            {
                Message.Encode();
                Message.Encrypt();
                Message.Process();

                Logging.Info(typeof(Processor), "Packet " + ConsolePad.Padding(Message.GetType().Name) + "    sent to    " + Message.Device.Socket.RemoteEndPoint + ".");

                Resources.TCPGateway.Send(Message);
            }
        }
    }
}