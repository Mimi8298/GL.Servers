namespace GL.Servers.BB.Core.Network
{
    using GL.Servers.BB.Packets;

    using GL.Servers.Extensions;

    internal static class Processor
    {
        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="Message">The message.</param>
        internal static void Send(this Message Message)
        {
            Message.Encode();
            Message.Encrypt();

            Logging.Info(typeof(Processor), "Packet " + ConsolePad.Padding(Message.GetType().Name) + "    sent to    " + Message.Device.Socket.RemoteEndPoint + ".");

            Resources.TCPGateway.Send(Message);

            Message.Process();
        }
    }
}