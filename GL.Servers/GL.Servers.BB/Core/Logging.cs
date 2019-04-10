namespace GL.Servers.BB.Core
{
    using System;
    using System.Diagnostics;

    using GL.Servers.BB.Core.Network;
    using GL.Servers.BB.Logic;
    using GL.Servers.BB.Packets.Messages.Server;
    using GL.Servers.BB.Packets.Messages.Server.Base;
    using GL.Servers.Extensions;

    internal static class Logging
    {
        /// <summary>
        /// Logs the specified error.
        /// </summary>
        /// <param name="Type">The type.</param>
        /// <param name="Message">The message.</param>
        internal static void Error(Type Type, string Message)
        {
            Resources.Logger.Error(Type.Name + " : " + Message);
            Debug.WriteLine("[*] " + ConsolePad.Padding(Type.Name, 15) + " : " + Message);
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="Type">The type.</param>
        /// <param name="Message">The message.</param>
        [Conditional("DEBUG")] internal static void Info(Type Type, string Message)
        {
            Resources.Logger.Info(Type.Name + " : " + Message);
            Debug.WriteLine("[*] " + ConsolePad.Padding(Type.Name, 15) + " : " + Message);
        }

        /// <summary>
        /// Logs the specified error.
        /// </summary>
        /// <param name="Type">The type.</param>
        /// <param name="Device">The device.</param>
        /// <param name="Message">The message.</param>
        /// <param name="ServerError">if set to <c>true</c> [server error].</param>
        internal static void Error(Type Type, Device Device, string Message, bool ServerError = true)
        {
            Logging.Error(Type, Message);

            if (ServerError)
            {
                new Server_Error_Message(Device, Message.Length).Send();
            }
        }
    }
}