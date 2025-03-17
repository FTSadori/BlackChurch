using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System.Threading;
using Client.Runtime.Framework;
using System.Linq;
using System.Text;
using Client.Runtime.Room.AwakeObjects;

namespace Server.Framework
{
    class ClientMessageData
    {
        public int id;
        public string message;

        public ClientMessageData(int id, string message)
        {
            this.id = id;
            this.message = message;
        }
    }

    class ShortPlayerData
    {
        public string name;
        public string characterId;

        public ShortPlayerData(string name, string characterId)
        {
            this.name = name;
            this.characterId = characterId;
        }
    }

    public sealed class SocketServerHandler
    {
        IPEndPoint localEndPoint;
        IPAddress ipAddress;
        SocketGuard listener;
        Dictionary<int, SocketGuard> clientSockets = new();
        Dictionary<int, ShortPlayerData> playerDatas = new();
        const int MaxClients = 7;
        public bool IsActive { get; private set; }

        Thread listeningThread;
        
        Queue<ClientMessageData> messages = new();

        public Action OnConnect;

        public void EstablishEndpoint(int port)
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            ipAddress = IPAddress.Parse("127.0.0.1"); //ipHost.AddressList[0];
//            Debug.Log(ipHost.AddressList[2]);
            localEndPoint = new (ipAddress, port);
            IsActive = true;
        }

        public void SendMessage(int id, string message)
        {
            if (!IsActive) return;

            if (clientSockets.ContainsKey(id))
            {
                lock (clientSockets[id])
                {
                    clientSockets[id].Socket.Send(Encoding.UTF8.GetBytes(message));
                }
            }
        }

        public void SendMessageToAll(string message)
        {
            if (!IsActive) return;

            var bytes = Encoding.UTF8.GetBytes(message);
            foreach (var kvp in clientSockets)
            {
                lock (kvp.Value)
                {
                    kvp.Value.Socket.Send(bytes);
                }
            }
        }

        public void StartListening()
        {
            if (!IsActive) return;

            clientSockets = new();
            playerDatas = new();

            listeningThread = new(delegate() {
                listener = new(new(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp));
                listener.Socket.Bind(localEndPoint);
                listener.Socket.Listen(10);
                Debug.Log("Starts to listen");

                while (true)
                {
                    SocketGuard clientSocket = new(listener.Socket.Accept());
                    
                    // not enough space in the room
                    if (clientSockets.Count >= MaxClients)
                    {
                        clientSocket.Socket.Send(Encoding.UTF8.GetBytes("1"));
                        continue;
                    }
                    clientSocket.Socket.Send(Encoding.UTF8.GetBytes("0"));
                    Debug.Log("Space is enough");

                    try {
                    // wait for nick;chara
                    byte[] data = new byte[1024];
                    clientSocket.Socket.Receive(data);

                    var parts = Encoding.UTF8.GetString(data).Split(";");
                    ShortPlayerData pldata = new(parts[0], parts[1]);
                    Debug.Log("Got nick;chara");

                    // save data
                    int hash = clientSocket.Socket.GetHashCode();
                    clientSockets.Add(hash, clientSocket);
                    playerDatas.Add(hash, pldata);
                    Debug.Log("Saved data");

                    // update everything
                    MainThreadDispatcher.Enqueue(delegate() { 
                        OnConnect?.Invoke();
                        SendMessage(hash, RoomController._currentRoomData);
                        Debug.Log("Send back data");
                    });
                    Debug.Log("Someone has connected to the server");
                    
                    }
                    catch (Exception) 
                    {
                        // some shit occured
                        clientSocket.Socket.Send(Encoding.UTF8.GetBytes("2"));
                    }
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