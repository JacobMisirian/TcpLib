using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MisirianSoft.TcpLib
{
    /// <summary>
    /// Tcp connection.
    /// </summary>
    public class TcpConnection
    {
        /// <summary>
        /// Gets the IP.
        /// </summary>
        /// <value>The IP.</value>
        public string IP { get; private set; }
        /// <summary>
        /// Gets the port.
        /// </summary>
        /// <value>The port.</value>
        public int Port { get; private set; }
        /// <summary>
        /// Gets the tcp client.
        /// </summary>
        /// <value>The tcp client.</value>
        public TcpClient TcpClient { get; private set; }
        /// <summary>
        /// Gets the stream reader.
        /// </summary>
        /// <value>The stream reader.</value>
        public StreamReader StreamReader { get; private set; }
        /// <summary>
        /// Gets the stream writer.
        /// </summary>
        /// <value>The stream writer.</value>
        public StreamWriter StreamWriter { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="TcpLib.TcpConnection"/> class.
        /// </summary>
        public TcpConnection() {}
        /// <summary>
        /// Initializes a new instance of the <see cref="TcpLib.TcpConnection"/> class.
        /// </summary>
        /// <param name="client">Client.</param>
        public TcpConnection(TcpClient client)
        {
            dataMembers(client);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TcpLib.TcpConnection"/> class.
        /// </summary>
        /// <param name="ip">Ip.</param>
        /// <param name="port">Port.</param>
        /// <param name="waitForConnection">If set to <c>true</c> wait for connection.</param>
        public TcpConnection(string ip, int port, bool waitForConnection = false)
        {
            Connect(ip, port, waitForConnection);
        }
        /// <summary>
        /// Connect the specified ip, port and waitForConnection.
        /// </summary>
        /// <param name="ip">Ip.</param>
        /// <param name="port">Port.</param>
        /// <param name="waitForConnection">If set to <c>true</c> wait for connection.</param>
        public void Connect(string ip, int port, bool waitForConnection = false)
        {
            TcpClient client = new TcpClient(ip, port);
            while (!client.Connected)
                Thread.Sleep(10);
            dataMembers(client);
        }

        private void dataMembers(TcpClient client)
        {
            IP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
            Port = ((IPEndPoint)client.Client.RemoteEndPoint).Port;
            StreamReader = new StreamReader(client.GetStream());
            StreamWriter = new StreamWriter(client.GetStream());
            TcpClient = client;
        }
        /// <summary>
        /// Occurs when data recieved.
        /// </summary>
        public event EventHandler<TcpConnectionDataRecievedEventArgs> DataRecieved;
        /// <summary>
        /// Listens for data recieved event.
        /// </summary>
        public void ListenForDataRecievedEvent()
        {
            new Thread(() => listenForDataRecievedEvent()).Start();
        }
        private void listenForDataRecievedEvent()
        {
            while (true)
                OnDataRecieved(new TcpConnectionDataRecievedEventArgs { DataString = StreamReader.ReadLine() });
        }
        /// <summary>
        /// Raises the data recieved event.
        /// </summary>
        /// <param name="e">E.</param>
        protected virtual void OnDataRecieved(TcpConnectionDataRecievedEventArgs e)
        {
            var handler = DataRecieved;
            if (handler != null)
                handler(this, e);
        }
        /// <summary>
        /// Read this instance.
        /// </summary>
        public int Read()
        {
            return StreamReader.Read();
        }
        /// <summary>
        /// Reads the line.
        /// </summary>
        /// <returns>The line.</returns>
        public string ReadLine()
        {
            return StreamReader.ReadLine();
        }
        /// <summary>
        /// Send the specified data.
        /// </summary>
        /// <param name="data">Data.</param>
        public void Send(byte[] data)
        {
            foreach (byte b in data)
                StreamWriter.Write(b);
        }
        /// <summary>
        /// Send the specified data.
        /// </summary>
        /// <param name="data">Data.</param>
        public void Send(string data)
        {
            Send(ASCIIEncoding.ASCII.GetBytes(data));
        }
        /// <summary>
        /// Sends the line.
        /// </summary>
        /// <param name="data">Data.</param>
        public void SendLine(string data)
        {
            Send(ASCIIEncoding.ASCII.GetBytes(data) + "\n");
        }
    }
}