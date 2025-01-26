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
    public sealed class OpenCraftMenuInputCommand : MonoCommand
    {
        [SerializeField] ItemMenuController _itemMenuController;
        [SerializeField] OpenMenuCommand _openCraftMenu;
        [SerializeField] MenuController _menuController;
        [SerializeField] CraftMenuInputController _craftMenuInputController;

        public override void Execute()
        {
            if (_itemMenuController.GetItemData().craftButtonActive)
            {
                _openCraftMenu.Execute();
                _menuController.Push(_craftMenuInputController);
            }
        }
    }
}