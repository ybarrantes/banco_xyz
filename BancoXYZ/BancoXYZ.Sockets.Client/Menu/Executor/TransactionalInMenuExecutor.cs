using BancoXYZ.Sockets.Server.Requests;
using System;

namespace BancoXYZ.Sockets.Client.Menu.Executor
{
    public class TransactionalInMenuExecutor : IExecutor
    {
        private readonly TransactionRequest _transactionRequest;

        public TransactionalInMenuExecutor(TransactionRequest transactionRequest)
        {
            _transactionRequest = transactionRequest;
        }

        public void Execute()
        {
            Console.Write("Por favor ingresa el número de cuenta: ");
            string accountNumber = Console.ReadLine();
            int value = GetUserValue();
            _transactionRequest.Consignacion(accountNumber, value);

            Console.WriteLine("Oprima una tecla para continuar...");
            Console.ReadKey();
        }

        private int GetUserValue()
        {
            while (true)
            {
                try
                {
                    Console.Write("Por favor ingresa el valor a consignar: ");
                    string value = Console.ReadLine();
                    int convert = Convert.ToInt32(value);
                    return convert;
                } catch (Exception)
                {
                    Console.WriteLine("  **** El valor ingresado es inválido!!!, inténtalo nuevamente...");
                }
            }
        }
    }
}
