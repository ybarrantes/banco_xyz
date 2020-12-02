using BancoXYZ.Sockets.Events;
using System;
using System.Net.Sockets;

namespace BancoXYZ.Sockets
{   
    public interface ISocket
    {
        bool IsConnected { get; }
        Socket Socket { get; }

        event EventHandler<ReceivedMessageEventArgs> ReceivedMessage;

        void Connect();
        void Disconnect();
        void SendMessage(Socket socket, string message);
    }
}
