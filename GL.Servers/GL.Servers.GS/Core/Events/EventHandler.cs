namespace GL.Servers.GS.Core.Events
{
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    
    using GL.Servers.GS.Logic;
    using GL.Servers.Logic.Enums;

    internal class EventsHandler
    {
        internal static EventHandler EHandler;

        internal delegate void EventHandler(Exits Type = Exits.CTRL_CLOSE_EVENT);

        /// <summary>
        /// Initializes a new instance of the <see cref="EventsHandler"/> class.
        /// </summary>
        internal EventsHandler()
        {
            EventsHandler.EHandler += this.Handler;
            EventsHandler.SetConsoleCtrlHandler(EventsHandler.EHandler, true);
        }

        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler Handler, bool Enabled);

        internal void ExitHandler()
        {
            Console.WriteLine("- SHUTDOWN -");

            Resources.Logger.Info("Server is shutting down, warning player about the maintenance...");

            Resources.Checker.Stop();

            Task WTask = Task.Run(() =>
            {
                foreach (Player Player in Resources.Players.Values.ToArray())
                {
                    if (Player.Device != null && Player.Device.State >= State.LOGGED)
                    {

                    }
                }
            });

            Resources.Logger.Info(this.GetType().Name + " : " + "Warned every players about the maintenance.");

            bool SavePlayers    = false;
            bool SaveClans      = false;
            bool Faster         = true;

            if (Resources.Players.Count > 0)
            {
                SavePlayers     = true;
            }

            if (SavePlayers || SaveClans)
            {
                Task PTask = new Task(() => Resources.Players.Save());

                if (SavePlayers)
                    PTask.Start();
                
                if (SavePlayers)
                    PTask.Wait();
                
                Faster          = false;
            }
            else
            {
                Console.WriteLine("Server has nothing to save, shutting down immediatly.");
            }

            WTask.Wait(5000);

            Thread.Sleep(5000 - (Faster ? 2000 : 0));

            Environment.Exit(0);
        }

        internal void Handler(Exits Type = Exits.CTRL_CLOSE_EVENT)
        {
            switch (Type)
            {
                case Exits.CTRL_CLOSE_EVENT:
                {
                    this.ExitHandler();
                    break;
                }

                default:
                {
                    break;
                }
            }
        }
    }
}