using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.UI.Menu;
using UnityEngine;

namespace Client.Runtime.Game.UI.Commands.InputCommands
{
    public sealed class OpenToolbarSlotInputCommand : MonoCommand
    {
        [SerializeField] WholeInventoryHandler _wholeInventoryHandler;
        [SerializeField] int _slotNum;
        [SerializeField] ItemMenuController _itemMenuController;
        [SerializeField] OpenMenuCommand _openItemMenu;
        [SerializeField] MenuController _menuController;
        [SerializeField] ItemMenuInputController _itemMenuInputController;

        public override void Execute()
        {
            if (!_wholeInventoryHandler.GetToolbarInventory().IsSlotEmpty(_slotNum))
            {
                _itemMenuController.Set(_wholeInventoryHandler.GetFromToolbar(_slotNum), UseButtonVariant.USE);
                _openItemMenu.Execute();
                _menuController.Push(_itemMenuInputController);
            }
        }
    }
}