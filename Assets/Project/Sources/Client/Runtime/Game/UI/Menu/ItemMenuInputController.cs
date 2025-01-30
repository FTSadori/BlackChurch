using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity;
using Client.Runtime.Framework.Unity.MenuInput;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.UI.Commands;
using UnityEngine;

namespace Client.Runtime.Game.UI.Menu
{
    public sealed class ItemMenuInputController : MonoBehaviour, IMenuInputController
    {
        [SerializeField] private List<SerializableNotUpdateKeyDownCommand> _keyDownCommands = new();
        public List<SerializableNotUpdateKeyDownCommand> KeyDownCommands => _keyDownCommands;

        private int _counter = 0;
        public void AddToList(SerializableNotUpdateKeyDownCommand command)
        {
            _counter += 1;
            _keyDownCommands.Add(command);
        }

        public void ClearLastCommands()
        {
            _keyDownCommands.RemoveRange(_keyDownCommands.Count - _counter, _counter);
            _counter = 0;
        }
        
        private bool _isInputActive = false;
        public bool IsInputActive { get => _isInputActive; set => _isInputActive = value; }

        public SlotMenu GetAssociatedSlotMenu() => SlotMenu.ItemMenu;
    }
}