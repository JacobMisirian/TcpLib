using System;
using System.Text;

namespace MisirianSoft.TcpLib
{
    /// <summary>
    /// Tcp connection data recieved event arguments.
    /// </summary>
    public class TcpConnectionDataRecievedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>The data.</value>
        public byte[] Data { get { return ASCIIEncoding.ASCII.GetBytes(DataString); } }
        /// <summary>
        /// Gets or sets the data string.
        /// </summary>
        /// <value>The data string.</value>
        public string DataString { get; set; }
    }
}

