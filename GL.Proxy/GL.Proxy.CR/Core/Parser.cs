namespace GL.Proxy.CR.Core
{
    using System;
    using System.Diagnostics;
    using System.Threading;

    internal class Parser
    {
        private static bool isRunning = true;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        internal static void Initialize()
        {
            new Thread(Parser.Parse).Start();
        }

        /// <summary>
        /// Parses this instance.
        /// </summary>
        private static void Parse()
        {
            while (Parser.isRunning)
            {
                string Command = Console.ReadLine();

                if (Command == string.Empty)
                {
                    Debug.WriteLine("-----------------------------------------------------------------------");
                }
                else if (Command == "exit")
                {
                    Parser.isRunning = false;
                }
            }
        }
    }
}