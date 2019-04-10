namespace GL.Servers.GS.Packets
{
    using System;
    using System.Collections.Generic;

    using GL.Servers.GS.Packets.Messages.Client;

    internal class MessageFactory
    {
        internal static Dictionary<ushort, Type> Messages = new Dictionary<ushort, Type>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageFactory"/> class.
        /// </summary>
        internal MessageFactory()
        {
            MessageFactory.Messages.Add(10101, typeof(Authentification));
            MessageFactory.Messages.Add(10200, typeof(Avatar_Create));
        }
    }
}