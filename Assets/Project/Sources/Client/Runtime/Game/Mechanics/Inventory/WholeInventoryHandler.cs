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
        [SerializeField] private ToolbarController _toolbarController;
        [SerializeField] private EquipmentController _equipmentController;

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

        private bool CanFitItemAfterCraft(string id)
        {
            for (int i = 0; i < GetToolbarInventory().Count(); ++i)
            {
                if (GetToolbarInventory().IsSlotEmpty(i))
                    return true;
            }

            var itemObj = _itemListHandler.GetObjectById(id);
            int craftQuantity = itemObj.quantity;
            int stack = itemObj.stack;

            int itemCount = GetToolbarInventory().GetItemCount(id);
            if (itemCount != 0)
            {
                if (itemCount % stack + craftQuantity <= stack)
                {
                    return true;
                }
            }

            string item1 = itemObj.craftsFromId1;
            string item2 = itemObj.craftsFromId2;
            if (item1 == item2)
            {
                if (GetToolbarInventory().GetItemCount(item1) == 2)
                {
                    return true;
                }
            }
            else
            {
                foreach (var idToFind in new List<string>(){item1, item2})
                {
                    if (GetToolbarInventory().GetItemCount(idToFind) == 1)
                        return true;
                }
            }

            return false;
        }

        public bool TryCraftItem(string id)
        {
            if (!CanBeCrafted(id))
            {
                return false;
            }

            if (!CanFitItemAfterCraft(id))
            {
                return false;
            }

            var itemObj = _itemListHandler.GetObjectById(id);

            foreach (var idToFind in new List<string>(){itemObj.craftsFromId1, itemObj.craftsFromId2})
            {
                if (GetToolbarInventory().GetItemCount(idToFind) > 0)
                {
                    GetToolbarInventory().RemoveItemAtSlot(0, idToFind, 1, true);
                }
                else
                {
                    GetEqupmentInventory().RemoveItemAtSlot(0, idToFind, 1, false);
                }
            }

            GetToolbarInventory().AddItem(itemObj.id, 1);
            return true;
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