using BancoXYZ.Sockets.Server.Requests;

namespace BancoXYZ.Sockets.Client.Menu
{
    public class MainMenu : OptionMenu
    {
        public MainMenu(TransactionRequest transactionRequest)
        {
            Key = string.Empty;
            Label = "Menú Principal";

            AddSubmenu(new ClientMenu());
            AddSubmenu(new TransactionMenu(transactionRequest));
        }
    }
}
