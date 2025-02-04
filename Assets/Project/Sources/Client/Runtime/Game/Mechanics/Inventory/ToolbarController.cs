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

        private void Awake() {
            _toolbarModel.InventoryData.OnUpdateInventory += UpdateInventory;
        }

        public void SetHelpNumberVisibility(bool visible)
        {
            for (int i = 0; i < _slots.Length; ++i)
            {
                _slots[i].SetHelpButtonText(visible ? (i+1).ToString() : "");
            }
        }

        private void UpdateInventory() {
            for (int i = 0; i < _slots.Length; i++)
            {
                var data = _toolbarModel.InventoryData.GetBySlotNumber(i);
                _slots[i].Set(data.id, data.quantity);
            }
        }

        private void OnDestroy() {
            _toolbarModel.InventoryData.OnUpdateInventory -= UpdateInventory;            
        }
    }
}