using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Client.Runtime.Game.Mechanics.Inventory
{
    public sealed class EquipmentModel : MonoBehaviour
    {
        [SerializeField] private InventoryData _inventoryData = new(new List<SlotType>{
            SlotType.WEAPON_ONLY, SlotType.HEAD_ONLY, SlotType.BODY_ONLY, 
            SlotType.LEGS_ONLY, SlotType.ACCESSORY_ONLY, SlotType.ACCESSORY_ONLY,
            SlotType.ACCESSORY_ONLY,
        });

        public InventoryData InventoryData => _inventoryData;
    }
}