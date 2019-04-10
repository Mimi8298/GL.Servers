namespace GL.Servers.GS.Packets.Messages.Server
{
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;

    using GL.Servers.GS.Logic;
    using GL.Servers.GS.Logic.Enums;

    internal class Login_Failed : Message
    {
        internal ErrorCode Error;

        /// <summary>
        /// Initializes a new instance of the <see cref="Login_Failed"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Error">The error.</param>
        public Login_Failed(Device Device, ErrorCode Error) : base(Device)
        {
            this.Identifier = 20103;
            this.Node       = ServiceNode.SERVICE_NODE_TYPE_ACCOUNT_DIRECTORY;
            this.Error      = Error;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Login_Failed"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Login_Failed(Device Device, Reader Reader) : base(Device, Reader)
        {
            this.Identifier = 20103;
            this.Node       = ServiceNode.SERVICE_NODE_TYPE_ACCOUNT_DIRECTORY;
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Error = (ErrorCode) this.Reader.ReadInt32();
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt((int) this.Error);
        }

        public enum ErrorCode
        {
            ERROR_CODE_WRONG_PASSWORD,
            ERROR_CODE_NO_SUCH_USER,
            ERROR_CODE_ALREADY_LOGGED_IN,
            ERROR_CODE_INVALID_SESSION_KEY,
            ERROR_CODE_SESSION_EXPIRED
        }
    }
}
