using BancoXYZ.Business.Business;
using BancoXYZ.Business.Entities;
using BancoXYZ.Business.Models;
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
            ClienteRequest clienteRequest = new ClienteRequest(endPoint, bufferSize);
            CreateCliente(clienteRequest);
        }

        private static void CreateCliente(ClienteRequest clienteRequest)
        {
            Cliente cliente = new Cliente
            {
                TipoDocumento = Business.Types.TipoDocumentoType.CedulaCiudadania,
                Documento = "111111111",
                Nombres = "Yonathan Gustavo",
                Apellidos = "Barrantes Leon",
                Direccion = "CL 44B # 23 - 12",
                Telefono = "3182539738",
                Genero = Business.Types.GeneroType.Masculino,
                FechaNacimiento = new DateTime(1991, 1, 31),
                Estado = Business.Types.EstadoType.Activo,
                Ciudad = new Ciudad { Id = 1, Nombre = "Bogotá D.C.", Codigo = "11001" }
            };

            clienteRequest.CreateCliente(cliente);
        }
    }
}
