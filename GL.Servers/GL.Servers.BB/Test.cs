namespace GL.Servers.BB
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using GL.Servers.BB.Logic.Enums;
    using GL.Servers.Extensions.List;
    using GL.Servers.Library.ZLib;

    internal class Test
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Test"/> class.
        /// </summary>
        [Conditional("DEBUG")]
        internal static void Initialize()
        {
            List<int> Test = new List<int>(100000000);
            Stopwatch Watch = new Stopwatch();

            Watch.Start();

            for (int i = 0; i < 100000000; i++)
            {
                // Test.Insert(i, i);
                Test.Add(i);
            }

            Watch.Stop();

            Console.WriteLine("Time : " + Watch.Elapsed.Milliseconds);
        }

        private static void Uncompress(string _Hexa)
        {
            byte[] Buffer = ZlibStream.UncompressBuffer(_Hexa.HexaToBytes());
            Console.WriteLine("Uncompressed : " + BitConverter.ToString(Buffer));
        }
    }
}