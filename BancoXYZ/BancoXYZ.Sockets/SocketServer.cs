using BancoXYZ.Sockets.Events;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace BancoXYZ.Sockets
{
    public class SocketServer : ISocket
    {
        private readonly IPEndPoint _endPoint;
        private readonly int _maxConnections;
        private readonly int _bufferSize;
        private readonly IDictionary<string, Socket> _clientSockets;
        private readonly ManualResetEvent _allDone;

        public Socket Socket { get; internal set; }
        public bool IsConnected { get; internal set; } = false;

        public event EventHandler<ReceivedMessageEventArgs> ReceivedMessage;
        protected virtual void OnReceivedMessage(ReceivedMessageEventArgs e)
        {
            EventHandler<ReceivedMessageEventArgs> handler = ReceivedMessage;
            if (handler != null)
            {
                handler?.Invoke(this, e);
            }
        }

        public SocketServer(IPEndPoint endPoint, int maxConnections, int bufferSize)
        {
            _endPoint = endPoint;
            _maxConnections = maxConnections;
            _bufferSize = bufferSize;
            _clientSockets = new Dictionary<string, Socket>();
            _allDone = new ManualResetEvent(false);
        }

        public void Connect()
        {
            try
            {
                SetupServerSocket();
                Console.WriteLine("Waiting for a connection...");
                while (IsConnected)
                {
                    _allDone.Reset();
                    Socket.BeginAccept(new AsyncCallback(AcceptCallback), Socket);
                    _allDone.WaitOne();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Waiting for a connection... {ex.Message}");
            }
        }

        private void SetupServerSocket()
        {
            Socket = new Socket(_endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            Socket.Bind(_endPoint);
            Socket.Listen(_maxConnections);
            IsConnected = true;
        }

        public void AcceptCallback(IAsyncResult asyncResult)
        {
            _allDone.Set();

            Socket listener = (Socket)asyncResult.AsyncState;
            Socket clientSocket = listener.EndAccept(asyncResult);
            AddClientSocketToDictionary(clientSocket);
            Console.WriteLine($"  - Client connected... {clientSocket.RemoteEndPoint}");

            var receivedMessage = new ReceivedMessageEventArgs(_bufferSize);
            receivedMessage.Socket = clientSocket;
            clientSocket.BeginReceive(receivedMessage.Buffer, 0, receivedMessage.BufferSize, SocketFlags.None,
                new AsyncCallback(ReadCallback), receivedMessage);
        }

        private void AddClientSocketToDictionary(Socket socket)
        {
            string key = socket.RemoteEndPoint.ToString();
            if (!_clientSockets.ContainsKey(key))
                _clientSockets.Add(key, socket);
            else
                _clientSockets[key] = socket;
        }

        public void ReadCallback(IAsyncResult asyncResult)
        {
            ReceivedMessageEventArgs receivedMessage = (ReceivedMessageEventArgs)asyncResult.AsyncState;
            Socket clientSocket = receivedMessage.Socket;
            int bytesRead = clientSocket.EndReceive(asyncResult);

            receivedMessage.DateTime = DateTime.Now;
            receivedMessage.Message = (bytesRead > 0)
                ? Encoding.ASCII.GetString(receivedMessage.Buffer, 0, bytesRead) : string.Empty;

            OnReceivedMessage(receivedMessage);
        }

        public void Disconnect()
        {
            CloseAllSockets();
        }

        private void CloseAllSockets()
        {
            foreach (KeyValuePair<string, Socket> keyValuePair in _clientSockets)
            {
                CloseSocket(keyValuePair.Value);
            }

            if (Socket.Connected)
                Socket.Close();
            
            IsConnected = false;
        }

        public void CloseSocket(Socket socket)
        {
            if (socket.Connected)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }

        public void SendMessage(Socket socket, string message)
        {
            try
            {
                byte[] byteData = Encoding.ASCII.GetBytes(message);
                socket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                handler.EndSend(ar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
