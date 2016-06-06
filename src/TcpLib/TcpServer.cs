using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MisirianSoft.TcpLib
{
    public class TcpServer
    {
        public List<TcpConnection> Connections { get; private set; }
        /// <summary>
        /// Gets the I.
        /// </summary>
        /// <value>The I.</value>
        public string IP { get; private set; }
        /// <summary>
        /// Gets the port.
        /// </summary>
        /// <value>The port.</value>
        public int Port { get; private set; }
        /// <summary>
        /// Gets the tcp listener.
        /// </summary>
        /// <value>The tcp listener.</value>
        public TcpListener TcpListener { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="TcpLib.TcpServer"/> class.
        /// </summary>
        public TcpServer() 
        {
            Connections = new List<TcpConnection>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TcpLib.TcpServer"/> class.
        /// </summary>
        /// <param name="ip">Ip.</param>
        /// <param name="port">Port.</param>
        public TcpServer(string ip, int port)
        {
            Connections = new List<TcpConnection>();
            Start(ip, port);
        }
        /// <summary>
        /// Occurs when client connected.
        /// </summary>
        public event EventHandler<TcpServerClientConnectedEventArgs> ClientConnected;
        /// <summary>
        /// Listens for client recieved event.
        /// </summary>
        public void ListenForClientRecievedEvent()
        {
            new Thread(() => listenForClientRecievedEvent()).Start();
        }
        private void listenForClientRecievedEvent()
        {
            while (true)
            {
                TcpConnection connection = new TcpConnection(TcpListener.AcceptTcpClient());
                Connections.Add(connection);
                OnClientConnected(new TcpServerClientConnectedEventArgs { TcpConnection = connection });
            }
        }
        /// <summary>
        /// Raises the client connected event.
        /// </summary>
        /// <param name="e">E.</param>
        protected virtual void OnClientConnected(TcpServerClientConnectedEventArgs e)
        {
            var handler = ClientConnected;
            if (handler != null)
                handler(this, e);
        }
        /// <summary>
        /// Start the specified ip and port.
        /// </summary>
        /// <param name="ip">Ip.</param>
        /// <param name="port">Port.</param>
        public void Start(string ip, int port)
        {
            TcpListener = new TcpListener(IPAddress.Parse(ip), port);
            TcpListener.Start();
        }
    }
}

