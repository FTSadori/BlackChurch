using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Client.Runtime.Framework
{
    public sealed class SocketGuard
    {
        public Socket Socket { get; private set; }

        public SocketGuard(Socket socket)
        {
            Socket = socket ?? throw new ArgumentNullException(nameof(socket));
        }
        
        ~SocketGuard()
        {
            Socket.Close(); 
        }
    }
}