using BancoXYZ.Business.Business;
using BancoXYZ.Business.Entities;
using BancoXYZ.Business.Models;
using Newtonsoft.Json;
using System;

namespace BancoXYZ.Business.Commands
{
    public class ClienteCommand : ICommand
    {
        private readonly ClientBusiness _clientBusiness;

        public Request Request { get; internal set; }

        public ClienteCommand(ClientBusiness clientBusiness, Request request)
        {
            _clientBusiness = clientBusiness;
            Request = request;
        }

        public Response Get()
        {
            throw new NotImplementedException();
        }

        public Response Post()
        {
            Response response = new Response
            {
                Accept = Types.AcceptType.Json,
            };

            try
            {
                Cliente cliente = JsonConvert.DeserializeObject<Cliente>(Request.Body);
                bool result = _clientBusiness.CreateCliente(cliente);

                if (!result)
                    throw new Exception("Falló la creación del cliente!!");

                response.Result = Types.ResultType.Ok;
                response.Message = Types.ResultType.Ok.ToString();
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
