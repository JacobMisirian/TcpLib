using System;

namespace MisirianSoft.TcpLib
{
    /// <summary>
    /// Tcp server client connected event arguments.
    /// </summary>
    public class TcpServerClientConnectedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the tcp connection.
        /// </summary>
        /// <value>The tcp connection.</value>
        public TcpConnection TcpConnection { get; private set; }
    }
}

