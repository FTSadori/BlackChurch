using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity;
using Client.Runtime.Game.Environment.Storage;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.Mechanics.Inventory.MenuControllers;
using Client.Runtime.Game.UI.Menu;
using UnityEngine;

namespace Client.Runtime.Game.UI.Commands.InputCommands
{
    public sealed class GetFromStorageSlotInputCommand : MonoCommand
    {
        public ToolbarController _toolbarController;
        public ToolbarModel _toolbarModel;
        public int _slotNum;
        public StorageMenuController _storageMenuController;
        public InventoryData _storageInventory;

        public override void Execute()
        {
            if (!_storageInventory.IsSlotEmpty(_slotNum))
            {
                var item = _storageInventory.GetBySlotNumber(_slotNum);
                var left = _toolbarModel.InventoryData.AddItem(item.id, item.quantity);
                _toolbarController.UpdateInventory();

                if (left == 0)
                {
                    _storageInventory.ClearSlot(_slotNum);
                }
                else if (left != item.quantity)
                {
                    _storageInventory.RemoveItemAtSlot(_slotNum, item.id, item.quantity - left, true);
                }
                else
                {
                    Debug.Log("Not enough space in toolbar");
                }
                _storageMenuController.UpdateInventory(_storageInventory);
            }
        }
    }
}