namespace GL.Servers.CR.Core.Network
{
    using System.Collections.Generic;
    using System.Net.Sockets;

    internal class SocketAsyncEventArgsPool
    {
        internal readonly Stack<SocketAsyncEventArgs> Pool;

        internal readonly object Gate = new object();

        internal readonly int Capacity;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SocketAsyncEventArgsPool"/> class.
        /// </summary>
        internal SocketAsyncEventArgsPool(int Capacity)
        {
            this.Capacity   = Capacity;
            this.Pool       = new Stack<SocketAsyncEventArgs>(Capacity);
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
                if (this.Pool.Count < this.Capacity)
                {
                    this.Pool.Push(AsyncEvent);
                }
                else
                {
                    Logging.Error(this.GetType(), "This error should NEVER occur, if you see this message, please throw GL.Servers.CR in fucking trash.");
                }
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        internal void Dispose()
        {
            lock (this.Gate)
            {
                this.Pool.Clear();
            }
        }
    }
}