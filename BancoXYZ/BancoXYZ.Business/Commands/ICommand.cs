using BancoXYZ.Business.Models;

namespace BancoXYZ.Business.Commands
{
    public interface ICommand
    {
        Request Request { get; }

        Response Get();
        Response Post();
        Response Put();
        Response Patch();
        Response Head();
        Response Delete();
    }
}
