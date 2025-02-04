using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.ScriptableObjects.Lists;
using UnityEngine;
using UnityEngine.Assertions;

namespace Client.Runtime.Game.Mechanics.Inventory
{
    [Serializable]
    public sealed class InventoryData
    {
        [SerializeField] private ItemListHandler _itemListHandler = new();
        private List<InventoryDataRecord> _inventory = new();

        public Action OnUpdateInventory;

        public InventoryData(List<SlotType> slotTypes)
        {
            foreach (var slotType in slotTypes)
            {
                AddEmptySlotOfType(slotType);
            }
        }

        public InventoryDataRecord GetBySlotNumber(int number) 
        {
            Assert.IsFalse(number < 0 || number >= _inventory.Count, "InventoryData.GetBySlotNumber: Wrong number of slot");
            return _inventory[number];
        }

        public int Count()
        {
            return _inventory.Count;
        }

        public void RemoveItemAtSlot(int slotNumber, string id, int quantity, bool withClearSlots)
        {
            int totalCount = 0;
            for (int i = slotNumber; i < _inventory.Count; ++i)
            {
                if (_inventory[i].id == id)
                {
                    totalCount += _inventory[i].quantity;
                    _inventory[i].quantity = 0;
                }
            }
            totalCount -= Mathf.Min(quantity, totalCount);
            AddItem(id, totalCount);

            if (withClearSlots)
            {
                int cleared = 0;
                for (int i = 0; i < _inventory.Count - cleared; ++i)
                {
                    if (_inventory[i].id == id && _inventory[i].quantity == 0)
                    {
                        cleared += 1;
                        ClearSlot(i);
                        --i;
                    }
                }
            }

            OnUpdateInventory?.Invoke();

            return;
        }

        public void ClearSlot(int number)
        {
            var t = _inventory[number].type;
            _inventory.Add(new InventoryDataRecord("", 0, t));
            _inventory.RemoveAt(number);

            OnUpdateInventory?.Invoke();
        }

        public bool IsSlotEmpty(int number)
        {
            return _inventory[number].id == "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <returns>The amount that left due to lack of free space</returns>
        public int AddItem(string id, int quantity)
        {
            List<int> indexes = GetIndexesOfSuitableSlotsFor(id, out int maxFreeSpace);
            int validQuantity = Mathf.Min(quantity, maxFreeSpace);
            if (!TryAddItem(id, validQuantity, indexes))
            {
                return quantity;
            }
            OnUpdateInventory?.Invoke();

            return quantity - validQuantity;
        }

        private bool TryAddItem(string id, int quantity, List<int> indexes)
        {
            if (quantity == 0) return false;

            if (indexes != null)
            {
                int maxStack = _itemListHandler.GetObjectById(id).stack;
                foreach (var index in indexes)
                {
                    var freeSpace = maxStack - _inventory[index].quantity;
                    if (freeSpace >= quantity)
                    {
                        _inventory[index].quantity += quantity;
                        _inventory[index].id = id;
                        return true;
                    }
                    _inventory[index].quantity += freeSpace;
                    _inventory[index].id = id;
                    quantity -= freeSpace;
                }
            }
            return false;
        }

        public int GetItemCount(string id)
        {
            int count = 0;
            foreach (var item in _inventory)
            {
                if (item.id == id)
                {
                    count += item.quantity;
                }
            }
            return count;
        }

        private List<int> GetIndexesOfSuitableSlotsFor(string id, out int maxFreeSpace)
        {
            int maxStack = _itemListHandler.GetObjectById(id).stack;
            int freeSpace = 0;
            List<int> indexes = new();
            for (int i = 0; i < _inventory.Count; ++i)
            {
                // if slot can fit this item
                if (_inventory[i].CanFit(_itemListHandler.GetNeededSlotTypeById(id)) && (_inventory[i].id == "" || _inventory[i].id == id))
                {
                    // calculate how much free space is available
                    int delta = maxStack;
                    if (_inventory[i].id == id)
                    {
                        delta -= _inventory[i].quantity;
                    }

                    if (delta > 0)
                    {
                        // save index of needed slot
                        indexes.Add(i);
                        freeSpace += delta;
                    }
                }
            }
            maxFreeSpace = freeSpace;

            return indexes;
        }

        private void AddEmptySlotOfType(SlotType type)
        {
            _inventory.Add(new InventoryDataRecord("", 0, type));
        }
    }
}