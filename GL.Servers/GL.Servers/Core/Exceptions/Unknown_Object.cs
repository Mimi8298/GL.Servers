namespace GL.Servers.Core.Exceptions
{
    using System;

    internal class Unknown_Object : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Unknown_Object"/> class.
        /// </summary>
        public Unknown_Object()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Unknown_Object"/> class.
        /// </summary>
        /// <param name="Message">The message.</param>
        public Unknown_Object(string Message) : base(Message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Unknown_Object"/> class.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="Error">The error.</param>
        public Unknown_Object(string Message, Exception Error) : base(Message, Error)
        {
        }
    }
}