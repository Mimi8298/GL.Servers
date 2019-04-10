namespace GL.Servers.BB.Logic.Slots.Items
{
    using System.Collections.Generic;
    using System.Timers;

    using GL.Servers.BB.Core;

    internal class Battle
    {
        internal int HighID;
        internal int LowID;

        internal int Tick;

        internal bool Started;
        internal bool Stopped;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Battle"/> class.
        /// </summary>
        internal Battle()
        {

        }
    }
}
 