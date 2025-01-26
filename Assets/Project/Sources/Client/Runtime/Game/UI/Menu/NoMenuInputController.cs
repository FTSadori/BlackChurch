using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackChurch.Assets.Project.Sources.Client.Runtime.Game.UI.Menu;
using Client.Runtime.Framework.Unity;
using Client.Runtime.Framework.Unity.MenuInput;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.UI.Commands;
using UnityEngine;

namespace Client.Runtime.Game.UI.Menu
{
    public sealed class NoMenuInputController : MonoBehaviour, IMenuInputController
    {
        [SerializeField] private List<SerializableNotUpdateKeyDownCommand> _keyDownCommands = new();
        public List<SerializableNotUpdateKeyDownCommand> KeyDownCommands => _keyDownCommands;

        private bool _isInputActive = false;
        public bool IsInputActive { get => _isInputActive; set => _isInputActive = value; }

        public SlotMenu GetAssociatedSlotMenu() => SlotMenu.Toolbar;
    }
}