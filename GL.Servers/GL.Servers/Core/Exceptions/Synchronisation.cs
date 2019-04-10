namespace GL.Servers.Core.Exceptions
{
    using System;

    internal class Synchronisation : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Synchronisation"/> class.
        /// </summary>
        public Synchronisation()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Synchronisation"/> class.
        /// </summary>
        /// <param name="Message">The message.</param>
        public Synchronisation(string Message) : base(Message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Synchronisation"/> class.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="Error">The error.</param>
        public Synchronisation(string Message, Exception Error) : base(Message, Error)
        {
        }
    }
}