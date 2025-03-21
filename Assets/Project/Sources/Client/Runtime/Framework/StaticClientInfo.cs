using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Room.AwakeObjects;
using Client.Runtime.System.Messages;

namespace Client.Runtime.Framework
{
    public static class StaticClientInfo
    {
        public static PlayerData _playerData = new("NewClient", null, null);
        public static SocketClientHandler _socketClientHandler = new();
    }
}