using BancoXYZ.Business.Entities;
using BancoXYZ.Business.Repository;
using System;

namespace BancoXYZ.Business.Business
{
    public class ClientBusiness
    {
        private readonly ClientRepository _clientRepository;

        public ClientBusiness(ClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public bool CreateCliente(Cliente cliente)
        {
            try
            {
                cliente.Estado = Types.EstadoType.Activo;
                _clientRepository.CreateClient(cliente);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
    }
}
