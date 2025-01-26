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
    public sealed class UseItemInputCommand : MonoCommand
    {
        [SerializeField] ItemMenuController _itemMenuController;
        [SerializeField] CloseMenuCommand _closeItemMenu;
        [SerializeField] MenuController _menuController;

        public override void Execute()
        {
            if (_itemMenuController.GetItemData().useButtonActive)
            {
                _closeItemMenu.Execute();
                _menuController.Pop();
            }
        }
    }
}