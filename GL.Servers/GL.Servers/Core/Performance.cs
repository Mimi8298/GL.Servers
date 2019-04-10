namespace GL.Servers.Core
{
    using System;
    using System.Diagnostics;

    internal class Performance : Stopwatch
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Performance"/> class.
        /// </summary>
        internal Performance()
        {
            this.Start();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        internal new TimeSpan Stop()
        {
            base.Stop();
            return this.Elapsed;
        }
    }
}