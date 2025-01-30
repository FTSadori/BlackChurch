using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Mechanics.Inventory;
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

        public InventoryData InventoryData => _inventoryData;

        private void Awake() {
            _inventoryData.TryAddItem("StupidSword", 1);
            _inventoryData.TryAddItem("StupidSword", 1);
            _inventoryData.TryAddItem("StupidSword", 1);
            _inventoryData.TryAddItem("Trash", 3);
            _inventoryData.TryAddItem("StupidSword", 1);
            _inventoryData.TryAddItem("StupidSword", 1);
            _inventoryData.TryAddItem("RedPorridge", 4);
            _inventoryData.TryAddItem("StupidSword", 1);
            _inventoryData.TryAddItem("StupidSword", 1);
            _inventoryData.TryAddItem("StupidSword", 1);
        }
    }
}