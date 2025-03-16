using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System.Threading;
using Client.Runtime.Framework;

namespace Server.Framework
{
    public sealed class SocketServerHandler
    {
        IPEndPoint localEndPoint;
        IPAddress ipAddress;
        SocketGuard listener;
        Thread listeningThread;
        Dictionary<int, SocketGuard> clientSockets;
        int counter = 0;
        public bool IsActive { get; private set; }

        public Action OnConnect;

        public void EstablishEndpoint(int port)
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            ipAddress = IPAddress.Parse("127.0.0.1"); //ipHost.AddressList[0];
//            Debug.Log(ipHost.AddressList[2]);
            localEndPoint = new (ipAddress, port);
            IsActive = true;
        }

        public void StartListening()
        {
            if (!IsActive) return;

            clientSockets = new();

            listeningThread = new(delegate() {
                listener = new(new(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp));
                listener.Socket.Bind(localEndPoint);
                listener.Socket.Listen(10);
                Debug.Log("Starts to listen");

                while (true)
                {
                    SocketGuard clientSocket = new(listener.Socket.Accept());
                    clientSockets.Add(counter++, clientSocket);
                    //OnConnect?.Invoke();
                    Debug.Log("Someone has connected to the server");
                }
            });
            listeningThread.IsBackground = true;
            listeningThread.Start();
        }

        public void StopListening()
        {
            if (!IsActive) return;

            listeningThread.Abort();
        }

        public void Close()
        {
            IsActive = false;
        }
    }
}