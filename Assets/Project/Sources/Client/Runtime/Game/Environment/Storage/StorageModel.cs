using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.ScriptableObjects.Storages;
using UnityEngine;

namespace Client.Runtime.Game.Environment.Storage
{
    public sealed class StorageModel : MonoBehaviour
    {
        [SerializeField] private InventoryData _inventoryData = new (new List<SlotType> {
            SlotType.EVERYTHING, SlotType.EVERYTHING, SlotType.EVERYTHING, SlotType.EVERYTHING,
            SlotType.EVERYTHING, SlotType.EVERYTHING, SlotType.EVERYTHING, SlotType.EVERYTHING,
            SlotType.EVERYTHING, SlotType.EVERYTHING, SlotType.EVERYTHING, SlotType.EVERYTHING,
            SlotType.EVERYTHING, SlotType.EVERYTHING, SlotType.EVERYTHING, SlotType.EVERYTHING,
        });

        public StorageScriptableObject _storageScriptableObject;
        public bool CanPutIn => _storageScriptableObject.canPlayerPutItemsIn;

        public InventoryData InventoryData => _inventoryData;

        private void Awake() {
            foreach (var pair in _storageScriptableObject.items)
            {
                _inventoryData.AddItem(pair.key, pair.value);
            }
        }
    }
}