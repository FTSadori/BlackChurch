using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.Mechanics.Inventory.MenuControllers;
using UnityEngine;

namespace Client.Runtime.Game.UI.Commands.InputCommands
{
    public sealed class PutInStorageSlotInputCommand : MonoCommand
    {
        static public bool _canPutIn = false;
        public ToolbarController _toolbarController;
        public ToolbarModel _toolbarModel;
        public int _slotNum;
        public StorageMenuController _storageMenuController;
        public InventoryData _storageInventory;

        public override void Execute()
        {
            if (!_canPutIn)
            {
                Debug.Log("You can't put anything in this storage");
                return;
            }
            if (!_toolbarModel.InventoryData.IsSlotEmpty(_slotNum))
            {
                var item = _toolbarModel.InventoryData.GetBySlotNumber(_slotNum);
                var left = _storageInventory.AddItem(item.id, item.quantity);
                if (left == 0)
                {
                    _toolbarModel.InventoryData.ClearSlot(_slotNum);
                }
                else if (left != item.quantity)
                {
                    _toolbarModel.InventoryData.RemoveItemAtSlot(_slotNum, item.id, item.quantity - left, true);
                }
                else
                {
                    Debug.Log("Not enough space in storage");
                }
            }
        }
    }
}