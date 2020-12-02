using System.Net;
using System;
using BancoXYZ.Business.Business;

namespace BancoXYZ.Sockets.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            string serverIp = Environment.GetEnvironmentVariable("SERVER_IP");
            int serverPort = int.Parse(Environment.GetEnvironmentVariable("SERVER_PORT"));
            int bufferSize = int.Parse(Environment.GetEnvironmentVariable("BUFFER_SIZE"));
            int maxConnections = int.Parse(Environment.GetEnvironmentVariable("SERVER_MAX_CONNECTIONS"));
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);

            ISocket socket = new SocketServer(endPoint, maxConnections, bufferSize);
            SocketSeverBusiness serverSocketBusiness = new SocketSeverBusiness(socket);
            serverSocketBusiness.ReceivedRequest += ServerSocketBusiness_ReceivedRequest;
            serverSocketBusiness.Connect();
        }

        private static void ServerSocketBusiness_ReceivedRequest(object sender, Business.Models.Request request)
        {
            Console.WriteLine($"    Request: [{request.Method}] [{request.Accept}] {request.Body}");
        }
    }
}
