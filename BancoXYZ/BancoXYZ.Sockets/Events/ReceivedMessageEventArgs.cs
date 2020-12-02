using System;
using System.Net.Sockets;

namespace BancoXYZ.Sockets.Events
{
    public class ReceivedMessageEventArgs : EventArgs
    {
        public Socket Socket { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
        public int BufferSize { get; }
        public byte[] Buffer { get; }

        public ReceivedMessageEventArgs(int bufferSize)
        {
            BufferSize = bufferSize;
            Buffer = new byte[bufferSize];
        }
    }
}
