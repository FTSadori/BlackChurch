using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Runtime.Framework
{
    public interface ICommandMessage
    {
        public void Execute(string line);
        public string CommandName { get; }
    }
}