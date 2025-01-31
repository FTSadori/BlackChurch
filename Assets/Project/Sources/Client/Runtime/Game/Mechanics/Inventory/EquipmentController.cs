using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.UI;
using UnityEngine;

namespace Client.Runtime.Game.Mechanics.Inventory
{
    public sealed class EquipmentController : MonoBehaviour
    {
        [SerializeField] private ItemSlotController[] _slots = new ItemSlotController[7];
        [SerializeField] private EquipmentModel _equipmentModel;

        public void UpdateInventory() {
            for (int i = 0; i < _slots.Length; i++)
            {
                var data = _equipmentModel.InventoryData.GetBySlotNumber(i);
                if (data.id != "")
                {
                    _slots[i].Set(data.id, data.quantity);
                }
            }
        }
    }
}