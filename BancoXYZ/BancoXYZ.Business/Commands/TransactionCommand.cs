using BancoXYZ.Business.Business;
using BancoXYZ.Business.Models;
using BancoXYZ.Business.Models.Transaction;
using Newtonsoft.Json;
using System;

namespace BancoXYZ.Business.Commands
{
    public class TransactionCommand : ICommand
    {
        private readonly TransactionBusiness _transactionBusiness;

        public Request Request { get; internal set; }

        public TransactionCommand(TransactionBusiness transactionBusiness, Request request)
        {
            _transactionBusiness = transactionBusiness;
            Request = request;
        }

        public Response Get()
        {
            Response response = new Response
            {
                Accept = Types.AcceptType.Json,
            };

            try
            {
                double result = _transactionBusiness.GetSaldo(Request.Arguments["accountNumber"]);
                response.Result = Types.ResultType.Ok;
                response.Message = Types.ResultType.Ok.ToString();
                response.Body = $"Su saldo es: {result.ToString("C")}";
            }
            catch (Exception e)
            {
                response.Result = Types.ResultType.InternalServerError;
                response.Message = e.Message;
            }
            return response;
        }

        public Response Post()
        {
            Response response = new Response
            {
                Accept = Types.AcceptType.Json,
            };

            try
            {
                MovimientoDTO movimientoDTO = JsonConvert.DeserializeObject<MovimientoDTO>(Request.Body);
                _transactionBusiness.RegistrarMovimiento(movimientoDTO);
                response.Result = Types.ResultType.Ok;
                response.Message = Types.ResultType.Ok.ToString();
                response.Body = $"La operación fue realizada con éxito.";
            }
            catch (Exception e)
            {
                response.Result = Types.ResultType.InternalServerError;
                response.Message = e.Message;
            }
            return response;
        }

        public Response Put()
        {
            throw new NotImplementedException();
        }

        public Response Patch()
        {
            throw new NotImplementedException();
        }

        public Response Head()
        {
            throw new NotImplementedException();
        }

        public Response Delete()
        {
            throw new NotImplementedException();
        }
    }
}
