namespace GL.Servers.GS.Packets
{
    using System;
    using System.Collections.Generic;

    internal class CommandFactory
    {
        internal static Dictionary<int, Type> Commands = new Dictionary<int, Type>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandFactory"/> class.
        /// </summary>
        internal CommandFactory()
        {

        }
    }
}