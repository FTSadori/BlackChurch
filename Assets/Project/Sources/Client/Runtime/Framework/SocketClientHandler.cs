using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client.Runtime.Framework
{
    public sealed class SocketClientHandler
    {
        SocketGuard clientSocket;

        object socketLock = new();
        public bool IsConnected { get; set; } = false;

        public void Create()
        {
            clientSocket = new(new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp));
        }

        public void Connect(string ip, int port)
        {
            lock (socketLock)
            {
                clientSocket.Socket.Connect(IPAddress.Parse(ip), port);
                IsConnected = true;
            }
        }

        public int Send(string message)
        {
            lock (socketLock)
            {
                return clientSocket.Socket.Send(Encoding.ASCII.GetBytes(message));
            }
        }

        public int Receive(byte[] bytes)
        {
            lock (socketLock)
            {
                return clientSocket.Socket.Receive(bytes);
            }
        }

        public void Close()
        {
            lock (socketLock)
            {
                clientSocket.Socket.Close();
            }
        }
    }
}