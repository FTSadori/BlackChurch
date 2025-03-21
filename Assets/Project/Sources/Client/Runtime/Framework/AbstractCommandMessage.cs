using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Client.Runtime.Framework
{
    public abstract class AbstractCommandMessage : MonoBehaviour, ICommandMessage
    {
        public abstract string CommandName { get; }

        public abstract void Execute(string line);
    }
}