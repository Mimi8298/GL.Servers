namespace GL.Servers.GS.Core.Consoles
{
    using System;
    using System.Threading;

    using GL.Servers.Extensions;

    internal class Parser
    {
        internal Thread Thread;

        /// <summary>
        /// Initializes a new instance of the <see cref="Parser"/> class.
        /// </summary>
        internal Parser()
        {
            this.Thread             = new Thread(this.Parse);
            this.Thread.Priority    = ThreadPriority.Lowest;
            this.Thread.Name        = this.GetType().Name;
            this.Thread.Start();
        }

        internal void Parse()
        {
            while (true)
            {
                int CursorTop2 = Console.CursorTop = Console.WindowTop + Console.WindowHeight - 1;
                Console.Write("root@localhost > ");

                string Command = Console.ReadLine();

                Console.SetCursorPosition(0, CursorTop2 - 1);
                Console.WriteLine(new string(' ', Console.BufferWidth));
                Console.SetCursorPosition(0, CursorTop2 - 2);

                switch (Command)
                {
                    case "/stats":
                    {
                        if (Resources.Started)
                        {
                            Console.WriteLine();
                            Console.WriteLine("# " + DateTime.Now.ToString("d") + " ---- STATS ---- " + DateTime.Now.ToString("T") + " #");
                            Console.WriteLine("# ----------------------------------- #");
                            Console.WriteLine("# In-Memory Players # " + ConsolePad.Padding(Resources.Players.Count.ToString(), 15) + " #");
                            Console.WriteLine("# In-Memory Saea    # " + ConsolePad.Padding(Resources.Gateway.ReadPool.Pool.Count + " - " + Resources.Gateway.WritePool.Pool.Count, 15) + " #");
                            Console.WriteLine("# ----------------------------------- #");
                        }

                        break;
                    }

                    case "/test":
                    {
                        break;
                    }

                    case "/clear":
                    {
                        Console.Clear();
                        break;
                    }

                    case "/exit":
                    case "/shutdown":
                    case "/stop":
                    {
                        Resources.Events.ExitHandler();
                        break;
                    }

                    default:
                    {
                        Console.WriteLine();
                        break;
                    }
                }
            }
        }
    }
}