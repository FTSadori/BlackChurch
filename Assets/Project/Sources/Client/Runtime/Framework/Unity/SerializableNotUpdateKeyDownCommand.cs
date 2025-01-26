using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Command;
using UnityEditor;
using UnityEngine;

namespace Client.Runtime.Framework.Unity
{
    public sealed class SerializableNotUpdateKeyDownCommand : MonoCommand<bool>
    {
        [SerializeField] KeyCode _keyCode;
        [SerializeField] MonoCommand _command;

        public override bool Execute()
        {
            if (Input.GetKeyDown(_keyCode))
            {
                _command.Execute();
                return true;
            }
            return false;
        }

        public void Set(KeyCode keyCode, MonoCommand command)
        {
            _keyCode = keyCode;
            _command = command;
        }
    }
}