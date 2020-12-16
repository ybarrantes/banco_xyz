using BancoXYZ.Sockets.Client.Menu.Executor;
using BancoXYZ.Sockets.Server.Requests;

namespace BancoXYZ.Sockets.Client.Menu
{
    public class TransactionInMenu : OptionMenu
    {
        public TransactionInMenu(TransactionRequest transactionRequest)
        {
            Label = "Consignación";
            Executor = new TransactionalInMenuExecutor(transactionRequest);
        }
    }
}
