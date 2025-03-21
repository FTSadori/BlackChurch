using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Runtime.Framework
{
    public interface ICommandMessagesLoader
    {
        public Dictionary<string, ICommandMessage> LoadMessages();
    }
}