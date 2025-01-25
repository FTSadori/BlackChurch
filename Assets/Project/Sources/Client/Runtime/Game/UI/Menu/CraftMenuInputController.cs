using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity.MenuInput;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.UI.Commands;
using UnityEngine;

namespace Client.Runtime.Game.UI.Menu
{
    public sealed class CraftMenuInputController : MonoBehaviour, IMenuInputController
    {
        [SerializeField] MenuController _menuController;
        [SerializeField] OpenMenuCommand _openItemMenu;
        [SerializeField] CloseMenuCommand _closeCraftMenu;
        [SerializeField] CloseMenuCommand _closeItemMenu;
        [SerializeField] ItemMenuInputController _itemMenuInputController;
        [SerializeField] ItemMenuController _itemMenuController;
        [SerializeField] CraftMenuController _craftMenuController;
        [SerializeField] WholeInventoryHandler _wholeInventoryHandler;

        public void CheckInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
            {
                _closeCraftMenu.Execute();
                _menuController.Pop();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_wholeInventoryHandler.CanBeCrafted(_itemMenuController.GetCurrentId()))
                {
                    _closeCraftMenu.Execute();
                    _menuController.Pop();
                    _closeItemMenu.Execute();
                    _menuController.Pop();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    _closeCraftMenu.Execute();
                    _menuController.Pop();
                    _itemMenuController.Set(_wholeInventoryHandler.GetItemData(_itemMenuController.GetCurrentId()));
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    _closeCraftMenu.Execute();
                    _menuController.Pop();
                    _itemMenuController.Set(_wholeInventoryHandler.GetItemData(_craftMenuController.GetCurrentMaterial1()));
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    _closeCraftMenu.Execute();
                    _menuController.Pop();
                    _itemMenuController.Set(_wholeInventoryHandler.GetItemData(_craftMenuController.GetCurrentMaterial2()));
                }
            }
        }

        public SlotMenu GetAssociatedSlotMenu() => SlotMenu.CraftMenu;
    }
}