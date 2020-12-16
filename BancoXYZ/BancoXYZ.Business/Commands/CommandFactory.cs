using BancoXYZ.Business.Business;
using BancoXYZ.Business.Models;
using BancoXYZ.Business.Repository;
using BancoXYZ.Business.Types;
using System;

namespace BancoXYZ.Business.Commands
{
    public static class CommandFactory
    {
        public static ICommand GetCommand(Request request)
        {
            string dbStrConnection = "server=localhost; database=banco_xyz; uid=root; pwd=root;";
            switch (request.Command)
            {
                case CommandType.Client:
                    var clientRepository = new ClientRepository(dbStrConnection);
                    var clientBusiness = new ClientBusiness(clientRepository);
                    return new ClienteCommand(clientBusiness, request);
                case CommandType.Transaction:
                    var transactionRepository = new TransactionRepository(dbStrConnection);
                    var transactionBusiness = new TransactionBusiness(transactionRepository);
                    return new TransactionCommand(transactionBusiness, request);
                default:
                    throw new NotImplementedException($"Command {request.Command} is not supported!!");
            }
        }
    }
}
