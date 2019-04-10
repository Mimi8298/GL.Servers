namespace GL.Servers.CR
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using GL.Servers.CR.Core;
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
            // VIntToInt("86-E1-F0-BE-07");
        }

        private static void ReadHexa(string _Hexa)
        {
            using (Reader Reader = new Reader(_Hexa.HexaToBytes()))
            {
                Console.WriteLine(Reader.ReadVInt());
                Console.WriteLine(Reader.ReadVInt());
                Console.WriteLine(Reader.ReadVInt());
                Console.WriteLine(Reader.ReadVInt());
            }
        }

        private static void Uncompress(string _Hexa)
        {
            byte[] Buffer = ZlibStream.UncompressBuffer(_Hexa.HexaToBytes());
            Logging.Info(typeof(Test), "Uncompressed : " + BitConverter.ToString(Buffer));
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
                Console.WriteLine("VInt 2 Int : " + _Reader.ReadVInt());
            }
        }
    }

    internal class TestInterlocked
    {
        internal int MegaNigger;
    }
}