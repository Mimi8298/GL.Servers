namespace GL.Patcher
{
    using System;
    using System.Linq;

    internal class Test
    {
        /// <summary>
        /// Turn a hexa string into a byte array.
        /// </summary>
        /// <param name="_Value">The value.</param>
        /// <returns></returns>
        internal byte[] HexaToBytes(string Value)
        {
            string _Tmp = Value.Replace("-", string.Empty);
            return Enumerable.Range(0, _Tmp.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(_Tmp.Substring(x, 2), 16)).ToArray();
        }
    }
}