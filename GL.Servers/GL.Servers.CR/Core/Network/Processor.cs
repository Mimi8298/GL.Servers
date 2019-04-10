namespace GL.Servers.CR.Core.Network
{
    using GL.Servers.CR.Packets;
    
    internal static class Processor
    {
        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="Message">The message.</param>
        internal static void Send(this Message Message)
        {
            if (Message.Device?.MessageManager != null)
            {
                Message.Encode();
                Message.Process();

                Message.Device.MessageManager.SendMessage(Message);
            }
        }
    }
}