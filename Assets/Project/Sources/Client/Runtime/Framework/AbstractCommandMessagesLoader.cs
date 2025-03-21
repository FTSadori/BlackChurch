using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Client.Runtime.Framework
{
    public abstract class AbstractCommandMessagesLoader : MonoBehaviour, ICommandMessagesLoader
    {
        public abstract Dictionary<string, ICommandMessage> LoadMessages();
    }
}