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
    public sealed class OpenMaterialSlotInputCommand : MonoCommand
    {
        [SerializeField] CloseMenuCommand _closeCraftMenu;
        [SerializeField] MenuController _menuController;
        [SerializeField] ItemMenuController _itemMenuController;
        [SerializeField] WholeInventoryHandler _wholeInventoryHandler;
        [SerializeField] CraftMenuController _craftMenuController;
        [SerializeField] int _slotNum;

        public override void Execute()
        {
            _closeCraftMenu.Execute();
            _menuController.Pop();
            _itemMenuController.Set(_wholeInventoryHandler.GetItemData(_craftMenuController.GetCurrentMaterial(_slotNum)));
        }
    }
}