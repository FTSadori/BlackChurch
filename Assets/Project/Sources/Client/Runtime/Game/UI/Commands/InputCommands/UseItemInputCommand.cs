using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.UI.Menu;
using UnityEngine;

namespace Client.Runtime.Game.UI.Commands.InputCommands
{
    public sealed class UseItemInputCommand : MonoCommand
    {
        [SerializeField] ItemMenuController _itemMenuController;
        [SerializeField] CloseMenuCommand _closeItemMenu;
        [SerializeField] MenuController _menuController;
        [SerializeField] WholeInventoryHandler _wholeInventoryHandler;
        [SerializeField] ItemListHandler _itemListHandler;

        public override void Execute()
        {
            if (_itemMenuController.GetItemData().useButtonActive)
            {
                bool deleteSlot;
                InventoryData inventoryFrom;
                InventoryData inventoryTo;

                if (_itemMenuController.InToolbar && !_wholeInventoryHandler.GetToolbarInventory().IsSlotEmpty(_itemMenuController.CurrentSlot))
                {
                    inventoryFrom = _wholeInventoryHandler.GetToolbarInventory();
                    inventoryTo = _wholeInventoryHandler.GetEqupmentInventory();
                    deleteSlot = true;
                }
                else if (!_itemMenuController.InToolbar && !_wholeInventoryHandler.GetEqupmentInventory().IsSlotEmpty(_itemMenuController.CurrentSlot))
                {
                    inventoryFrom = _wholeInventoryHandler.GetEqupmentInventory();
                    inventoryTo = _wholeInventoryHandler.GetToolbarInventory();
                    deleteSlot = false;
                }
                else return;

                string id = inventoryFrom.GetBySlotNumber(_itemMenuController.CurrentSlot).id;

                if (_itemMenuController.GetItemData().typeId == TypeIds.Consumable)
                {
                    // todo consume
                    Debug.Log("Consumed");
                }
                else if (TypeIds.IsEquipable(_itemMenuController.GetItemData().typeId))
                {
                    // todo equip/unequip
                    if (inventoryTo.AddItem(id, 1) != 0)
                    {
                        // unequip + equip
                        if (_itemMenuController.InToolbar)
                        {
                            for (int i = 0; i < inventoryTo.Count(); ++i)
                            {
                                if (inventoryTo.GetBySlotNumber(i).type == _itemListHandler.GetNeededSlotTypeById(id))
                                {
                                    string idwas = inventoryTo.GetBySlotNumber(i).id;
                                    inventoryFrom.RemoveItemAtSlot(_itemMenuController.CurrentSlot, id, 1, deleteSlot);
                                    inventoryFrom.AddItem(idwas, 1);
                                    inventoryTo.RemoveItemAtSlot(i, idwas, 1, false);
                                    inventoryTo.AddItem(id, 1);
                                    Debug.Log("Switched");
                                    _closeItemMenu.Execute();
                                    _menuController.Pop();
                                    return;
                                }
                            }
                        }
                        Debug.Log("Can't equip/unequip");
                        return;
                    }
                    Debug.Log("Equipped/Unequipped");
                }

                inventoryFrom.RemoveItemAtSlot(_itemMenuController.CurrentSlot, id, 1, deleteSlot);

                _closeItemMenu.Execute();
                _menuController.Pop();
            }
        }
    }
}