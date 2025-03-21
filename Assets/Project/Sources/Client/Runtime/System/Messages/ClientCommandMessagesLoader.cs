using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework;
using Client.Runtime.System.Messages.Client;
using UnityEngine;

namespace Client.Runtime.System.Messages
{
    public class ClientCommandMessagesLoader : AbstractCommandMessagesLoader
    {
        [SerializeField] List<AbstractCommandMessage> abstractCommandMessages = new();

        public override Dictionary<string, ICommandMessage> LoadMessages()
        {
            Dictionary<string, ICommandMessage> messages = new();
            
            foreach (var message in abstractCommandMessages)
            {
                messages[message.CommandName] = message;
            }

            return messages;
        }
    }
}