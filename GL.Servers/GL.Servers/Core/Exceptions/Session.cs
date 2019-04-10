namespace GL.Servers.Core.Exceptions
{
    using System;

    public class Session : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Session"/> class.
        /// </summary>
        public Session()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Session"/> class.
        /// </summary>
        /// <param name="Message">The message.</param>
        public Session(string Message) : base(Message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Session"/> class.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="Error">The error.</param>
        public Session(string Message, Exception Error) : base(Message, Error)
        {
        }
    }
}