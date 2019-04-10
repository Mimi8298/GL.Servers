namespace GL.Servers.BU.Packets
{
    using System;
    using System.Collections.Generic;

    using GL.Servers.BU.Packets.Messages.Client;

    internal class MessageFactory
    {
        internal static Dictionary<ushort, Type> Messages = new Dictionary<ushort, Type>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageFactory"/> class.
        /// </summary>
        internal MessageFactory()
        {
            MessageFactory.Messages.Add(10102, typeof(Authentification_Session));   // Third  Packet
            MessageFactory.Messages.Add(10103, typeof(Create_Account));             // Second Packet
            MessageFactory.Messages.Add(10105, typeof(Secure_Connection));          // First  Packet
            MessageFactory.Messages.Add(10108, typeof(Keep_Alive));
            MessageFactory.Messages.Add(10200, typeof(Create_Avatar));              // Fourth Packet
        }
    }
}