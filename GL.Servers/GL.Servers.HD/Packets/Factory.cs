namespace GL.Servers.HD.Packets
{
    using System;
    using System.Collections.Generic;

    using GL.Servers.Extensions.Binary;

    using GL.Servers.HD.Core;
    using GL.Servers.HD.Logic;
    using GL.Servers.HD.Packets.Messages.Account;

    internal static class Factory
    {
        /// <summary>
        /// The delimiter used to detect if x string is a call-command.
        /// </summary>
        internal const char Delimiter = '/';

        internal static readonly Dictionary<short, Type> Messages = new Dictionary<short, Type>();
        internal static readonly Dictionary<int, Type> Commands   = new Dictionary<int, Type>();
        internal static readonly Dictionary<string, Type> Debugs  = new Dictionary<string, Type>();

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        internal static void Initialize()
        {
            Factory.LoadMessages();
            Factory.LoadCommands();
            Factory.LoadDebugs();
        }

        /// <summary>
        /// Loads the game messages.
        /// </summary>
        private static void LoadMessages()
        {
            Factory.Messages.Add(10100, typeof(ClientHelloMessage));
            Factory.Messages.Add(10101, typeof(LoginMessage));
            Factory.Messages.Add(10108, typeof(KeepAliveMessage));
            Factory.Messages.Add(10113, typeof(Set_Device_Token_Message));
        }

        /// <summary>
        /// Loads the game commands.
        /// </summary>
        private static void LoadCommands()
        {

        }

        /// <summary>
        /// Loads the debug commands.
        /// </summary>
        private static void LoadDebugs()
        {

        }
        
        internal static Message CreateMessage(short Type, Device Device, Reader Reader)
        {
            if (Factory.Messages.TryGetValue(Type, out Type MType))
            {
                return (Message) Activator.CreateInstance(MType, Device, Reader);
            }

            Logging.Info(typeof(Factory), "Can't handle the following message : ID " + Type + ".");

            return null;
        }
    }
}