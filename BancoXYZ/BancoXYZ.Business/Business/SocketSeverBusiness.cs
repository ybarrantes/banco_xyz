using BancoXYZ.Business.Commands;
using BancoXYZ.Business.Models;
using BancoXYZ.Sockets;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;

namespace BancoXYZ.Business.Business
{
    public class SocketSeverBusiness
    {
        private readonly ISocket _socketServer;

        public event EventHandler<Request> ReceivedRequest;

        public SocketSeverBusiness(ISocket socketServer)
        {
            _socketServer = socketServer;
            _socketServer.ReceivedMessage += _socketServer_ReceivedMessage;
        }

        private void _socketServer_ReceivedMessage(object sender, Sockets.Events.ReceivedMessageEventArgs e)
        {
            Request request = JsonConvert.DeserializeObject<Request>(e.Message);
            OnReceivedRequest(request);

            ICommand command = CommandFactory.GetCommand(request);
            Response response = CommandExecutor.ExecuteCommand(command);
            SendMessage(e.Socket, response);
        }

        public void Connect()
        {
            _socketServer.Connect();
        }

        public void Disconnect()
        {
            _socketServer.Disconnect();
        }

        private void SendMessage(Socket socket, Response response)
        {
            try
            {
                string message = JsonConvert.SerializeObject(response);
                _socketServer.SendMessage(socket, message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        protected virtual void OnReceivedRequest(Request request)
        {
            EventHandler<Request> handler = ReceivedRequest;
            if (handler != null)
            {
                handler?.Invoke(this, request);
            }
        }
    }
}
