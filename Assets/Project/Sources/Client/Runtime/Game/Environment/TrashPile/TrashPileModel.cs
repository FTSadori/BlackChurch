using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Client.Runtime.Framework.Base;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.ScriptableObjects.Piles;
using UnityEngine;

namespace Client.Runtime.Game.Environment.TrashPile
{
    public enum TrashPileState
    {
        AWAITS,
        BUSY,
        FOUND,
    }

    public sealed class TrashPileModel : MonoBehaviour
    {
        public ItemListHandler _itemListHandler;
        public TrashPileScriptableObject _trashPileScriptableObject;
        public TrashPileState _state = TrashPileState.AWAITS;
        public List<InventoryDataRecord> _itemRecords = new();
        private void Awake() {
            foreach (string id in _trashPileScriptableObject.itemIds)
            {
                int quantity = _itemListHandler.GetObjectById(id).quantity;
                _itemRecords.Add(new InventoryDataRecord(id, quantity, SlotType.EVERYTHING));
            }
            _itemRecords = ArrayRandom.Shuffle(_itemRecords);
        }

        public InventoryDataRecord GetNextItem() {
            return _itemRecords.First() ?? null;
        }

        public void SkipItem() {
            if (_itemRecords.Count < 2)
            {
                return;
            }
            var item = _itemRecords.First();
            _itemRecords.Add(new InventoryDataRecord(item.id, item.quantity, SlotType.EVERYTHING));
            _itemRecords.RemoveAt(0);
        }

        public void RemoveItem() {
            if (_itemRecords.Count == 0)
            {
                return;
            }
            _itemRecords.RemoveAt(0);
        }
        
        public void AddItem(InventoryDataRecord item) {
            _itemRecords.Add(item);
        }
    }
}