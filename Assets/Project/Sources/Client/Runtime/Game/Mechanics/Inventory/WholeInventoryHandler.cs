using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Mechanics.Inventory.Data;
using UnityEngine;

namespace Client.Runtime.Game.Mechanics.Inventory
{
    public sealed class WholeInventoryHandler : MonoBehaviour
    {
        [SerializeField] private ToolbarModel _toolbarModel;
        [SerializeField] private EquipmentModel _equipmentModel;
        [SerializeField] private ItemListHandler _itemListHandler;

        public Action<string> OnItemDiscard;
        public Action<string> OnItemEquipped;
        public Action<string> OnItemUnequipped;
        public Action<string> OnItemCrafted;
        public Action<string> OnItemUsed;
        public Action<string> OnItemGet;

        public ItemData GetItemData(string id)
        {
            return ItemDataCreator.GenerateItemData(_itemListHandler.GetObjectById(id), this);
        }

        public bool Contains(string id, int quantity)
        {
            return _toolbarModel.InventoryData.GetItemCount(id) + _equipmentModel.InventoryData.GetItemCount(id) >= quantity;
        }

        public bool CanBeCrafted(string id)
        {
            var itemObj = _itemListHandler.GetObjectById(id);
            string item1 = itemObj.craftsFromId1;
            string item2 = itemObj.craftsFromId2;
            if (item1 == "" || item2 == "")
                return false;
            if (item1 == item2)
                return Contains(item1, 2);
            return Contains(item1, 1) && Contains(item2, 1);
        }

        public InventoryData GetToolbarInventory()
        {
            return _toolbarModel.InventoryData;
        }

        public InventoryData GetEqupmentInventory()
        {
            return _equipmentModel.InventoryData;
        }

        public ItemData GetFromToolbar(int slotNumber)
        {
            return GetItemData(_toolbarModel.InventoryData.GetBySlotNumber(slotNumber).id);
        }

        public ItemData GetFromEquipment(int slotNumber)
        {
            return GetItemData(_equipmentModel.InventoryData.GetBySlotNumber(slotNumber).id);
        }
    }
}