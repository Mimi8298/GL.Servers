using System.Linq;

namespace GL.Servers.GS
{
    using System;
    using System.Collections.Generic;

    using GL.Servers.GS.Extensions.Binary;
    using GL.Servers.GS.Extensions.List;
    using GL.Servers.Library.ZLib;

    internal class Test
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Test"/> class.
        /// </summary>
        internal Test()
        {

        }

        internal void Uncompress(string _Hexa)
        {
            byte[] Buffer = ZlibStream.UncompressBuffer(_Hexa.HexaToBytes().ToArray());
            Console.WriteLine("Uncompressed : " + BitConverter.ToString(Buffer));
        }

        internal void IntToVInt(int _Value)
        {
            List<byte> _Writer = new List<byte>();
            _Writer.AddVInt(_Value);
            Console.WriteLine("Int 2 VInt : " + _Value + " = " + BitConverter.ToString(_Writer.ToArray()));
            _Writer = null;
        }

        internal void VIntToInt(string _Value)
        {
            using (Reader _Reader = new Reader(_Value.HexaToBytes().ToArray()))
            {
                Console.WriteLine("VInt 2 Int : " + _Reader.ReadVInt());
            }
        }
    }
}