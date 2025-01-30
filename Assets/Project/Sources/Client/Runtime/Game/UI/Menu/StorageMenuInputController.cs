using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity;
using Client.Runtime.Framework.Unity.MenuInput;
using UnityEngine;

namespace Client.Runtime.Game.UI.Menu
{
    public sealed class StorageMenuInputController : MonoBehaviour, IMenuInputController
    {
        [SerializeField] private List<SerializableNotUpdateKeyDownCommand> _keyDownCommands = new();
        public List<SerializableNotUpdateKeyDownCommand> KeyDownCommands => _keyDownCommands;

        private bool _isInputActive = false;
        public bool IsInputActive { get => _isInputActive; set => _isInputActive = value; }

        public SlotMenu GetAssociatedSlotMenu() => SlotMenu.Toolbar;
    }
}