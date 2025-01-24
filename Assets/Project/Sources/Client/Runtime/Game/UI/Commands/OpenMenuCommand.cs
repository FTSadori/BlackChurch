using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity;
using UnityEngine;

namespace Client.Runtime.Game.UI.Commands
{
    public sealed class OpenMenuCommand : MonoCommand
    {
        [SerializeField] GameObject _menuObject;

        public override void Execute()
        {
            _menuObject.SetActive(true);
        }
    }
}