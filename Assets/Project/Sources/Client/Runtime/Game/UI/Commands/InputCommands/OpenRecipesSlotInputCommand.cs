using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity;
using Client.Runtime.Game.Mechanics.Inventory;
using UnityEngine;

namespace Client.Runtime.Game.UI.Commands.InputCommands
{
    public sealed class OpenRecipesSlotInputCommand : MonoCommand
    {
        [SerializeField] ItemMenuController _itemMenuController;
        [SerializeField] int _slotNum;
        [SerializeField] WholeInventoryHandler _wholeInventoryHandler;

        public void Set(ItemMenuController itemMenuController, int slotNum, WholeInventoryHandler wholeInventoryHandler)
        {
            _itemMenuController = itemMenuController;
            _slotNum = slotNum;
            _wholeInventoryHandler = wholeInventoryHandler;
        }

        public override void Execute()
        {
            var list = _itemMenuController.GetListOfCraftables();
            if (_slotNum < list.Count)
            {
                _itemMenuController.Set(_wholeInventoryHandler.GetItemData(list[_slotNum]), UseButtonVariant.USE);
            }
        }
    }
}