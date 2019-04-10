namespace GL.Servers.Core.Consoles.Colorful
{
    using System;

    /// <summary>
    /// Encapsulates information relating to exceptions thrown while making calls to the console via the Win32 API.
    /// </summary>
    public sealed class ConsoleAccessException : Exception
    {
        /// <summary>
        /// Encapsulates information relating to exceptions thrown while making calls to the console via the Win32 API.
        /// </summary>
        public ConsoleAccessException()
            : base(String.Format("Color conversion failed because a handle to the actual windows console was not found."))
        {
        }
    }
}
