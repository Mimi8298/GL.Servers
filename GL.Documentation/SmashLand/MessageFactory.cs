namespace GL.Servers.SL.Packets
{
    using System;
    using System.Collections.Generic;

    using GL.Servers.SL.Packets.Messages.Client;

    internal class MessageFactory
    {
        internal static Dictionary<ushort, Type> Messages = new Dictionary<ushort, Type>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageFactory"/> class.
        /// </summary>
        internal MessageFactory()
        {
            
        }
    }
}