using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.ScriptableObjects.Lists;
using UnityEngine;

namespace Client.Runtime.Game.Mechanics.Inventory
{
    public sealed class ToolbarModel : MonoBehaviour
    {
        [SerializeField] private InventoryData _inventoryData = new(new List<SlotType>{
            SlotType.EVERYTHING, SlotType.EVERYTHING, SlotType.EVERYTHING, 
            SlotType.EVERYTHING, SlotType.EVERYTHING, SlotType.EVERYTHING,
            SlotType.EVERYTHING,
        });

        public InventoryData InventoryData => _inventoryData;
    }
}