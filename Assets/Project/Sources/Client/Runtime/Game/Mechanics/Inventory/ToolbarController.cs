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
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (_toolbarModel.InventoryData.TryAddItem("RedPorridge", 1))
                    UpdateInventory();
                else
                    Debug.Log("Not enough space");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (_toolbarModel.InventoryData.TryAddItem("RedPorridge", 3))
                    UpdateInventory();
                else
                    Debug.Log("Not enough space");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (_toolbarModel.InventoryData.TryAddItem("StupidSword", 1))
                    UpdateInventory();
                else
                    Debug.Log("Not enough space");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (_toolbarModel.InventoryData.TryAddItem("Trash", 3))
                    UpdateInventory();
                else
                    Debug.Log("Not enough space");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Debug.Log(_toolbarModel.InventoryData.GetItemCount("RedPorridge"));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                Debug.Log(_toolbarModel.InventoryData.GetItemCount("StupidSword"));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                Debug.Log(_toolbarModel.InventoryData.GetItemCount("Trash"));
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