using BancoXYZ.Sockets.Client.Menu.Executor;
using BancoXYZ.Sockets.Server.Requests;

namespace BancoXYZ.Sockets.Client.Menu
{
    public class TransactionOutMenu : OptionMenu
    {
        public TransactionOutMenu(TransactionRequest transactionRequest)
        {
            Label = "Retiro";
            Executor = new TransactionalOutMenuExecutor(transactionRequest);
        }
    }
}
