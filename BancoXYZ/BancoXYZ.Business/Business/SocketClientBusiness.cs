using BancoXYZ.Business.Models;
using BancoXYZ.Sockets;
using Newtonsoft.Json;
using System;
using System.Net;

namespace BancoXYZ.Business.Business
{
    public class SocketClientBusiness
    {
        private readonly IPEndPoint _endPoint;
        private readonly int _bufferSize;

        public event EventHandler<Response> ReceivedResponse;

        public SocketClientBusiness(IPEndPoint endPoint, int bufferSize)
        {
            _endPoint = endPoint;
            _bufferSize = bufferSize;
        }

        private void _socketClient_ReceivedMessage(object sender, Sockets.Events.ReceivedMessageEventArgs e)
        {
            Response response = JsonConvert.DeserializeObject<Response>(e.Message);
            OnReceivedResponse(response);
        }

        public void SendMessage(Request request)
        {
            ISocket socketClient = new SocketClient(_endPoint, _bufferSize);
            socketClient.ReceivedMessage += _socketClient_ReceivedMessage;
            try
            {
                socketClient.Connect();
                string message = JsonConvert.SerializeObject(request);
                socketClient.SendMessage(socketClient.Socket, message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if (socketClient.IsConnected)
                {
                    socketClient.Disconnect();
                    socketClient.ReceivedMessage -= _socketClient_ReceivedMessage;
                }
            }
        }

        protected virtual void OnReceivedResponse(Response response)
        {
            EventHandler<Response> handler = ReceivedResponse;
            if (handler != null)
            {
                handler?.Invoke(this, response);
            }
        }
    }
}
