using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.UI;
using UnityEngine;

namespace Client.Runtime.Game.Mechanics.Inventory
{
    public sealed class ToolbarController : MonoBehaviour
    {
        [SerializeField] private ItemSlotController[] _slots = new ItemSlotController[7];
        [SerializeField] private ToolbarModel _toolbarModel;

        private void Update() {
            if (Input.GetKeyDown(KeyCode.N))
            {
                if (_toolbarModel.InventoryData.TryAddItem("Thing", 1))
                    UpdateInventory();
                if (_toolbarModel.InventoryData.TryAddItem("Chair", 1))
                    UpdateInventory();
                if (_toolbarModel.InventoryData.TryAddItem("StupidSword", 1))
                    UpdateInventory();
                if (_toolbarModel.InventoryData.TryAddItem("Trash", 3))
                    UpdateInventory();
            }
        }

        public void UpdateInventory() {
            for (int i = 0; i < _slots.Length; i++)
            {
                var data = _toolbarModel.InventoryData.GetBySlotNumber(i);
                if (data.id != "")
                {
                    _slots[i].Set(data.id, data.quantity);
                }
            }
        }
    }
}