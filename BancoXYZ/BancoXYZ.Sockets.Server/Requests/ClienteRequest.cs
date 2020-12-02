using BancoXYZ.Business.Business;
using BancoXYZ.Business.Entities;
using BancoXYZ.Business.Models;
using Newtonsoft.Json;
using System;
using System.Net;

namespace BancoXYZ.Sockets.Client.Requests
{
    public class ClienteRequest
    {
        private readonly SocketClientBusiness _socketClientBusiness;

        public ClienteRequest(IPEndPoint endPoint, int bufferSize)
        {
            _socketClientBusiness = new SocketClientBusiness(endPoint, bufferSize);
            _socketClientBusiness.ReceivedResponse += _socketClientBusiness_ReceivedResponse;
        }

        public void CrearCliente(Cliente cliente)
        {
            Request request = new Request
            {
                Accept = Business.Types.AcceptType.Json,
                Method = Business.Types.MethodType.Post,
                Command = Business.Types.CommandType.Client,
                Body = JsonConvert.SerializeObject(cliente)
            };

            _socketClientBusiness.SendMessage(request);
        }

        private void _socketClientBusiness_ReceivedResponse(object sender, Response e)
        {
            Console.WriteLine($"Resultado creación cliente: {e.Result} - {e.Message}");
        }
    }
}
