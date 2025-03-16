using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Framework
{
    public static class StaticServerInfo
    {
        public static SocketServerHandler _socketServerHandler = new();
        public static int _openedPort = 0;
    }
}