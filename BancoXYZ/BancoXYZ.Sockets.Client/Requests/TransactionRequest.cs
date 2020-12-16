using BancoXYZ.Business.Business;
using BancoXYZ.Business.Models;
using BancoXYZ.Business.Models.Transaction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace BancoXYZ.Sockets.Server.Requests
{
    public class TransactionRequest
    {
        private readonly SocketClientBusiness _socketClientBusiness;

        public TransactionRequest(IPEndPoint endPoint, int bufferSize)
        {
            _socketClientBusiness = new SocketClientBusiness(endPoint, bufferSize);
            _socketClientBusiness.ReceivedResponse += _socketClientBusiness_ReceivedResponse;
        }

        public void ConsultaSaldo(string accountNumber)
        {
            var request = new Request
            {
                Accept = Business.Types.AcceptType.Json,
                Method = Business.Types.MethodType.Get,
                Command = Business.Types.CommandType.Transaction,
                Body = string.Empty,
                Arguments = new Dictionary<string, string>
                {
                    { "accountNumber", accountNumber }
                }
            };

            _socketClientBusiness.SendMessage(request);
        }

        public void Consignacion(string accountNumber, long valor)
        {
            var consignacion = new MovimientoDTO
            {
                NumeroCuenta = accountNumber,
                Valor = Math.Abs(valor),
                TipoMovimiento = Business.Types.TipoMovimientoType.Consignacion
            };

            var request = new Request
            {
                Accept = Business.Types.AcceptType.Json,
                Method = Business.Types.MethodType.Post,
                Command = Business.Types.CommandType.Transaction,
                Body = JsonConvert.SerializeObject(consignacion),
            };

            _socketClientBusiness.SendMessage(request);
        }

        public void Retiro(string accountNumber, long valor)
        {
            var retiro = new MovimientoDTO
            {
                NumeroCuenta = accountNumber,
                Valor = Math.Abs(valor) * -1,
                TipoMovimiento = Business.Types.TipoMovimientoType.Retiro
            };

            var request = new Request
            {
                Accept = Business.Types.AcceptType.Json,
                Method = Business.Types.MethodType.Post,
                Command = Business.Types.CommandType.Transaction,
                Body = JsonConvert.SerializeObject(retiro),
            };

            _socketClientBusiness.SendMessage(request);
        }

        private void _socketClientBusiness_ReceivedResponse(object sender, Response e)
        {
            Console.WriteLine($"{e.Body}");
        }
    }
}
