using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.ScriptableObjects.Lists;
using UnityEngine;

namespace Client.Runtime.Game.Mechanics.Inventory
{
    [Serializable]
    public sealed class InventoryData
    {
        [SerializeField] private ItemListHandler _itemListHandler = new();
        private List<InventoryDataRecord> _inventory = new();
        private int _maxInventorySize;

        public InventoryData(List<SlotType> slotTypes)
        {
            _maxInventorySize = slotTypes.Count;
            foreach (var slotType in slotTypes)
            {
                AddEmptySlotOfType(slotType);
            }
        }

        public bool TryAddItem(string id, uint quantity)
        {
            return true;
        }

        private void AddEmptySlotOfType(SlotType type)
        {
            _inventory.Add(new InventoryDataRecord("", 0, type));
        }
    }
}