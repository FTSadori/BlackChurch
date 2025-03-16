using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity.SceneCommands;
using UnityEngine;

namespace Client.Runtime.Framework.Unity
{
    public sealed class GeneralButtonSequenceCommnad : ButtonCommand
    {
        [SerializeField] SequenceCommand _sequenceCommand;
        public override void Execute()
        {
            _sequenceCommand.Execute();
        }
    }
}