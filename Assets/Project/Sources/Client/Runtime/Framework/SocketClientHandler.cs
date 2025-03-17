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

                byte[] buffer = new byte[1024];
                clientSocket.Socket.Receive(buffer);

                string ans = Encoding.UTF8.GetString(buffer);
                if (ans == "0")
                {
                    return;
                }
                else if (ans == "1")
                {
                    throw new Exception("Not enough space in the room");
                }
                else if (ans == "2")
                {
                    throw new Exception("Some unknown error occured");
                }
            }
        }

        public int Send(string message)
        {
            lock (socketLock)
            {
                return clientSocket.Socket.Send(Encoding.UTF8.GetBytes(message));
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