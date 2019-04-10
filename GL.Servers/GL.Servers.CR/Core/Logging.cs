namespace GL.Servers.CR.Core
{
    using System;
    using System.Diagnostics;

    using GL.Servers.CR.Core.Network;
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Packets.Messages.Server.Home;
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
        internal static void Info(Type Type, string Message)
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
                new Server_Error_Message(Device, Type.Name + " : " + Message).Send();
            }
        }
    }
}