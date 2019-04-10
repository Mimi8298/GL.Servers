namespace GL.Clients.BB.Core.Network
{
    using GL.Clients.BB.Packets;

    internal static class Processor
    {
        /// <summary>
        /// Recepts the specified message.
        /// </summary>
        /// <param name="Message">The message.</param>
        internal static void Recept(this Message Message)
        {
            Message.Decrypt();
            Message.Decode();
            Message.Process();
        }

        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="Command">The command.</param>
        internal static Command Handle(this Command Command)
        {
            Command.Encode();

            return Command;
        }
    }
}