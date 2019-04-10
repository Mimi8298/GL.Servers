namespace GL.Servers.Core.Consoles
{
    using System;
    using System.IO;
    using System.Text;

    public class Prefixed : TextWriter
    {
        internal readonly TextWriter Original;

        /// <summary>
        /// Initializes a new instance of the <see cref="Prefixed"/> class.
        /// </summary>
        public Prefixed()
        {
            this.Original = Console.Out;
            this.Original.WriteLine(Environment.NewLine);
        }

        /// <summary>
        /// En cas de substitution dans une classe dérivée, retourne l'encodage de caractères dans lequel la sortie est écrite.
        /// </summary>
        public override Encoding Encoding
        {
            get
            {
                return new ASCIIEncoding();
            }
        }

        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="Message">The message.</param>
        public override void WriteLine(string Message)
        {
            if (Message.Length <= Console.WindowWidth)
            {
                Console.SetCursorPosition((Console.WindowWidth - Message.Length) / 2, Console.CursorTop);
            }

            this.Original.WriteLine("{0}", Message);
        }

        /// <summary>
        /// Écrit une marque de fin de ligne dans la chaîne ou le flux de texte.
        /// </summary>
        public override void WriteLine()
        {
            this.Original.WriteLine();
        }

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="Message">The message.</param>
        public override void Write(string Message)
        {
            this.Original.Write(Message);
        }
    }
}