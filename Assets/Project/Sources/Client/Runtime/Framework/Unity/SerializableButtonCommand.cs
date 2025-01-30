using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Command;
using UnityEngine;

namespace Client.Runtime.Framework.Unity
{
    public sealed class SerializableButtonCommand : ButtonCommand
    {
        [SerializeField] public MonoCommand _command;

        public override void Execute()
        {
            _command.Execute();
        }
    }
}