namespace GL.Servers.SL
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using GL.Servers.SL.Core;

    using GL.Servers.Extensions.Binary;
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

        }

        private static void Uncompress(string _Hexa)
        {
            byte[] Buffer = ZlibStream.UncompressBuffer(_Hexa.HexaToBytes());
            Console.WriteLine("Uncompressed : " + BitConverter.ToString(Buffer));
        }

        private static void IntToVInt(int _Value)
        {
            List<byte> _Writer = new List<byte>();
            _Writer.AddVInt(_Value);
            Console.WriteLine("Int 2 VInt : " + _Value + " = " + BitConverter.ToString(_Writer.ToArray()));
            _Writer = null;
        }

        private static void VIntToInt(string _Value)
        {
            using (Reader _Reader = new Reader(_Value.HexaToBytes()))
            {
                Logging.Info(typeof(Test), "VInt 2 Int : " + _Reader.ReadVInt() + " | " + _Value);
            }
        }
    }
}