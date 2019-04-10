namespace GL.Servers.Extensions
{
    using System;
    using System.Text;

    internal class Base64
    {
        /// <summary>
        /// Decodes the specified encoded string.
        /// </summary>
        /// <param name="Encoded">The encoded string.</param>
        public static string Decode(string Encoded, int Reiteration = 1)
        {
            string Decoded = Encoding.UTF8.GetString(Convert.FromBase64String(Encoded));

            for (int i = 1; i < Reiteration; i++)
            {
                Decoded = Encoding.UTF8.GetString(Convert.FromBase64String(Decoded));
            }

            return Decoded;
        }

        /// <summary>
        /// Encodes the specified decoded string.
        /// </summary>
        /// <param name="Decoded">The decoded string.</param>
        public static string Encode(string Decoded, int Reiteration = 1)
        {
            string Encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(Decoded));

            for (int i = 1; i < Reiteration; i++)
            {
                Encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(Encoded));
            }

            return Encoded;
        }
    }
}