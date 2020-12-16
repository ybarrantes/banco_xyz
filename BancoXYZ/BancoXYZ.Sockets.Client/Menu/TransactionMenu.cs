using BancoXYZ.Sockets.Server.Requests;

namespace BancoXYZ.Sockets.Client.Menu
{
    public class TransactionMenu : OptionMenu
    {
        public TransactionMenu(TransactionRequest transactionRequest)
        {
            Label = "Transacciones";
            AddSubmenu(new TransactionQueryMenu(transactionRequest));
            AddSubmenu(new TransactionInMenu(transactionRequest));
            AddSubmenu(new TransactionOutMenu(transactionRequest));
        }
    }
}
