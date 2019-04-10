namespace GL.Clients.CR.Core.Consoles
{
    using System;
    using System.Threading;

    internal class Parser
    {
        internal Thread Thread;

        /// <summary>
        /// Initializes a new instance of the <see cref="Parser"/> class.
        /// </summary>
        internal Parser()
        {
            this.Thread = new Thread(this.Parse);
            this.Thread.Priority = ThreadPriority.Lowest;
            this.Thread.Name = this.GetType().Name;
            this.Thread.Start();
        }

        internal void Parse()
        {
            while (true)
            {
                ConsoleKeyInfo Command = Console.ReadKey(false);

                switch (Command.Key)
                {
                    case ConsoleKey.C:
                    {
                        Console.Clear();
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