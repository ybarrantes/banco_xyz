using BancoXYZ.Sockets.Server.Requests;
using System;

namespace BancoXYZ.Sockets.Client.Menu.Executor
{
    public class TransactionalQueryMenuExecutor : IExecutor
    {
        private readonly TransactionRequest _transactionRequest;

        public TransactionalQueryMenuExecutor(TransactionRequest transactionRequest)
        {
            _transactionRequest = transactionRequest;
        }

        public void Execute()
        {
            Console.Write("Por favor ingresa el número de cuenta: ");
            string accountNumber = Console.ReadLine();
            _transactionRequest.ConsultaSaldo(accountNumber);

            Console.WriteLine("Oprima una tecla para continuar...");
            Console.ReadKey();
        }
    }
}
