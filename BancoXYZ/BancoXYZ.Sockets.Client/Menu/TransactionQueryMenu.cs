using BancoXYZ.Sockets.Client.Menu.Executor;
using BancoXYZ.Sockets.Server.Requests;

namespace BancoXYZ.Sockets.Client.Menu
{
    public class TransactionQueryMenu : OptionMenu
    {
        public TransactionQueryMenu(TransactionRequest transactionRequest)
        {
            Label = "Consulta de saldo";
            Executor = new TransactionalQueryMenuExecutor(transactionRequest);
        }
    }
}
