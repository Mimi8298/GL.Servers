namespace GL.Servers.GS.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Timers;

    using GL.Servers.GS.Logic;

    internal class Timers
    {
        internal readonly List<Timer> LTimers;

        /// <summary>
        /// Initializes a new instance of the <see cref="Timers"/> class.
        /// </summary>
        internal Timers()
        {
            this.LTimers = new List<Timer>(1);
            {
                // this.DeadSockets();
            }

            this.Run();
        }

        /// <summary>
        /// Disconnects dead sockets.
        /// </summary>
        internal void DeadSockets()
        {
            Timer Timer     = new Timer();

            Timer.Interval  = 30000;
            Timer.AutoReset = true;
            Timer.Elapsed  += (UCS, Sucks) =>
            {
                foreach (Player Player in Resources.Players.Values.ToList().FindAll(Player => Player.Device != null && !Player.Device.Connected))
                {
                    Resources.Gateway.Disconnect(Player.Device.Token.Args);
                }
            };

            this.LTimers.Add(Timer);
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        internal void Run()
        {
            foreach (Timer Timer in this.LTimers)
            {
                Timer.Start();
            }
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        internal void Stop()
        {
            foreach (Timer Timer in this.LTimers)
            {
                Timer.Stop();
            }
        }
    }
}