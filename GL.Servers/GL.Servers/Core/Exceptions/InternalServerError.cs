namespace GL.Servers.Core.Exceptions
{
    using System;

    public class InternalServerError : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InternalServerError"/> class.
        /// </summary>
        public InternalServerError() : base()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalServerError"/> class.
        /// </summary>
        /// <param name="Message">The message.</param>
        public InternalServerError(string Message) : base(Message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalServerError"/> class.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="Excetion">The excetion.</param>
        public InternalServerError(string Message, Exception Excetion) : base(Message, Excetion)
        {

        }
    }
}
