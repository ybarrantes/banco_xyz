using BancoXYZ.Business.Entities;
using BancoXYZ.Sockets.Client.Menu;
using BancoXYZ.Sockets.Server.Requests;
using System;
using System.Net;

namespace BancoXYZ.Sockets.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string serverIp = Environment.GetEnvironmentVariable("SERVER_IP");
            int serverPort = int.Parse(Environment.GetEnvironmentVariable("SERVER_PORT"));
            int bufferSize = int.Parse(Environment.GetEnvironmentVariable("BUFFER_SIZE"));
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);

            var transactionRequest = new TransactionRequest(endPoint, bufferSize);
            var clienteRequest = new ClienteRequest(endPoint, bufferSize);

            var mainMenu = new MainMenu(transactionRequest);
            mainMenu.ExecuteMenu();

            //ClienteRequest clienteRequest = new ClienteRequest(endPoint, bufferSize);
            //CreateCliente(clienteRequest);
        }

        private static void CreateCliente(ClienteRequest clienteRequest)
        {
            Cliente cliente = new Cliente
            {
                TipoDocumento = Business.Types.TipoDocumentoType.CedulaCiudadania,
                Documento = "22222222",
                Nombres = "Sebastian",
                Apellidos = "Mejia",
                Direccion = "CL 44B # 23 - 12",
                Telefono = "3182539738",
                Genero = Business.Types.GeneroType.Masculino,
                FechaNacimiento = new DateTime(1993, 1, 20),
                Estado = Business.Types.EstadoType.Activo,
                Ciudad = new Ciudad { Id = 2, Nombre = "Bogotá D.C.", Codigo = "11001" }
            };

            clienteRequest.CreateCliente(cliente);
        }
    }
}
