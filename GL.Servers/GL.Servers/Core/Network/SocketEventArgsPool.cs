namespace GL.Servers.Core.Network
{
    using System.Collections.Generic;
    using System.Net.Sockets;

    internal class SocketAsyncEventArgsPool
    {
        private readonly Stack<SocketAsyncEventArgs> Pool;
        private readonly object Gate;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SocketAsyncEventArgsPool"/> class.
        /// </summary>
        internal SocketAsyncEventArgsPool(int Capacity)
        {
            this.Pool = new Stack<SocketAsyncEventArgs>(Capacity);
            this.Gate = new object();
        }

        /// <summary>
        ///     Dequeues this instance.
        /// </summary>
        /// <returns>
        ///     <see cref="SocketAsyncEventArgs"/>
        /// </returns>
        internal SocketAsyncEventArgs Dequeue()
        {
            lock (this.Gate)
            {
                if (this.Pool.Count > 0)
                {
                    return this.Pool.Pop();
                }

                return null;
            }
        }

        /// <summary>
        ///     Enqueues the specified item.
        /// </summary>
        /// <param name="AsyncEvent">
        ///     The <see cref="SocketAsyncEventArgs"/> instance containing the event data.
        /// </param>
        internal void Enqueue(SocketAsyncEventArgs AsyncEvent)
        {
            AsyncEvent.AcceptSocket     = null;
            AsyncEvent.RemoteEndPoint   = null;

            lock (this.Gate)
            {
                this.Pool.Push(AsyncEvent);
            }
        }
    }
}