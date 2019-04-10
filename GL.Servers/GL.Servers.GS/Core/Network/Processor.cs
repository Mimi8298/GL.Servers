namespace GL.Servers.GS.Core.Network
{
    using GL.Servers.Extensions;
    using GL.Servers.GS.Packets;

    internal static class Processor
    {
        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="Command">The command.</param>
        internal static Command Handle(this Command Command)
        {
            Command.Encode();
            return Command;
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="Message">The message.</param>
        internal static void Send(this Message Message)
        {
            Message.Encode();
            Message.Encrypt();

            Logging.Error(typeof(Processor), "Packet " + ConsolePad.Padding(Message.GetType().Name) + "    sent to    " + Message.Device.Socket.RemoteEndPoint + ".");

            Resources.Gateway.Send(Message);

            Message.Process();
        }
    }
}