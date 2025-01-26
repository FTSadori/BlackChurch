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
    public sealed class CraftItemInputCommand : MonoCommand
    {
        [SerializeField] WholeInventoryHandler _wholeInventoryHandler;
        [SerializeField] ItemMenuController _itemMenuController;
        [SerializeField] MenuController _menuController;
        [SerializeField] CloseMenuCommand _closeCraftMenu;
        [SerializeField] CloseMenuCommand _closeItemMenu;

        public override void Execute()
        {
            if (_wholeInventoryHandler.CanBeCrafted(_itemMenuController.GetCurrentId()))
            {
                _closeCraftMenu.Execute();
                _menuController.Pop();
                _closeItemMenu.Execute();
                _menuController.Pop();
                // todo Craft logic
            }
        }
    }
}