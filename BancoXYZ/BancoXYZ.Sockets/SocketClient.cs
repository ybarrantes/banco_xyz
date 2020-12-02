using BancoXYZ.Sockets.Events;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace BancoXYZ.Sockets
{
    public class SocketClient : ISocket
    {
        private readonly IPEndPoint _endPoint;
        private readonly int _bufferSize;

        private ManualResetEvent _connectDone;
        private ManualResetEvent _sendDone;
        private ManualResetEvent _receiveDone;

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

        public SocketClient(IPEndPoint endPoint, int bufferSize)
        {
            _endPoint = endPoint;
            _bufferSize = bufferSize;
            _connectDone = new ManualResetEvent(false);
            _sendDone = new ManualResetEvent(false);
            _receiveDone = new ManualResetEvent(false);
        }

        public void Connect()
        {
            try
            {
                Socket = new Socket(_endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                Socket.BeginConnect(_endPoint, new AsyncCallback(ConnectCallback), Socket);
                _connectDone.WaitOne();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void ConnectCallback(IAsyncResult asyncResult)
        {
            try
            {
                Socket socket = (Socket)asyncResult.AsyncState;
                socket.EndConnect(asyncResult);
                IsConnected = true;
                Console.WriteLine($"Conected to {socket.RemoteEndPoint}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                _connectDone.Set();
            }
        }

        public void Disconnect()
        {
            IsConnected = false;
            Socket.Shutdown(SocketShutdown.Both);
            Socket.Close();
        }

        public void SendMessage(Socket socket, string message)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(message);
            socket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
            _sendDone.WaitOne();

            Receive(socket);
            _receiveDone.WaitOne();
        }

        private void SendCallback(IAsyncResult asyncResult)
        {
            try
            {
                Socket handler = (Socket)asyncResult.AsyncState;
                handler.EndSend(asyncResult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                _sendDone.Set();
            }
        }

        private void Receive(Socket socket)
        {
            try
            {
                var receivedMessage = new ReceivedMessageEventArgs(_bufferSize);
                receivedMessage.Socket = socket;
                receivedMessage.Message = string.Empty;

                BeginReceive(receivedMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void BeginReceive(ReceivedMessageEventArgs receivedMessage)
        {
            receivedMessage.Socket.BeginReceive(receivedMessage.Buffer, 0, receivedMessage.BufferSize,
                SocketFlags.None, new AsyncCallback(ReceiveCallback), receivedMessage);
        }

        private void ReceiveCallback(IAsyncResult asyncResult)
        {
            try
            {
                ReceivedMessageEventArgs receivedMessage = (ReceivedMessageEventArgs)asyncResult.AsyncState;
                Socket socket = receivedMessage.Socket;
                int bytesRead = socket.EndReceive(asyncResult);

                receivedMessage.Message += (bytesRead > 0)
                    ? Encoding.ASCII.GetString(receivedMessage.Buffer, 0, bytesRead) : string.Empty;

                if (bytesRead == receivedMessage.BufferSize)
                {                    
                    BeginReceive(receivedMessage);
                }
                else
                {
                    receivedMessage.DateTime = DateTime.Now;
                    OnReceivedMessage(receivedMessage);
                    _receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
