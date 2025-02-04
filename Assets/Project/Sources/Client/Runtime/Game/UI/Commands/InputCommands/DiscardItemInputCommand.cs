using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity;
using Client.Runtime.Game.Environment.DroppedItems;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.Player;
using Client.Runtime.Game.UI.Menu;
using UnityEngine;

namespace Client.Runtime.Game.UI.Commands.InputCommands
{
    public sealed class DiscardItemInputCommand : MonoCommand
    {
        [SerializeField] ItemMenuController _itemMenuController;
        [SerializeField] CloseMenuCommand _closeItemMenu;
        [SerializeField] MenuController _menuController;
        [SerializeField] GameObject _droppedItemObject;
        [SerializeField] WholeInventoryHandler _wholeInventoryHandler;
        [SerializeField] PlayerController _playerController;
        [SerializeField] float _throwPowerHor = 10f;
        [SerializeField] float _throwPowerVer = 2f;
        static public bool _inToolbar = true;
        static public int _currentSlot = 0;

        public override void Execute()
        {
            if (_itemMenuController.GetItemData().discardButtonActive)
            {
                string id;
                int quantity;
                bool deleteSlot;
                InventoryData inventory;
                if (_inToolbar && !_wholeInventoryHandler.GetToolbarInventory().IsSlotEmpty(_currentSlot))
                {
                    inventory = _wholeInventoryHandler.GetToolbarInventory();
                    deleteSlot = true;
                }
                else if (!_wholeInventoryHandler.GetEqupmentInventory().IsSlotEmpty(_currentSlot))
                {
                    inventory = _wholeInventoryHandler.GetEqupmentInventory();
                    deleteSlot = false;
                }
                else return;

                id = inventory.GetBySlotNumber(_currentSlot).id;
                quantity = inventory.GetBySlotNumber(_currentSlot).quantity;
                _wholeInventoryHandler.GetToolbarInventory().RemoveItemAtSlot(_currentSlot, id, quantity, deleteSlot);                

                var obj = Instantiate(_droppedItemObject);
                obj.transform.position = _playerController.gameObject.transform.position;
                obj.GetComponentInChildren<DroppedItemController>().Set(id, quantity, inventory);
                obj.GetComponent<Rigidbody2D>().velocity = new Vector2(_playerController.LastDirection * _throwPowerHor, _throwPowerVer);
                _closeItemMenu.Execute();
                _menuController.Pop();
            }
        }
    }
}