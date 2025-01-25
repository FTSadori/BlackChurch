using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity.MenuInput;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.UI;
using Client.Runtime.Game.UI.Commands;
using Client.Runtime.Game.UI.Menu;
using UnityEngine;

namespace BlackChurch.Assets.Project.Sources.Client.Runtime.Game.UI.Menu
{
    public sealed class EquipmentMenuInputController : MonoBehaviour, IMenuInputController
    {
        [SerializeField] MenuController _menuController;
        [SerializeField] OpenMenuCommand _openItemMenu;
        [SerializeField] CloseMenuCommand _closeEquipmentMenu;
        [SerializeField] ItemMenuInputController _itemMenuInputController;
        [SerializeField] ItemMenuController _itemMenuController;
        [SerializeField] WholeInventoryHandler _wholeInventoryHandler;

        private readonly List<KeyCode> _equipmentCodes = new(){
            KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3,
            KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6,
            KeyCode.Alpha7
        };

        public void CheckInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
            {
                _closeEquipmentMenu.Execute();
                _menuController.Pop();
            }
            else
            {
                foreach (KeyCode keyCode in _equipmentCodes)
                {
                    TryOpenItemMenu(keyCode);
                }
            }
        }

        private void TryOpenItemMenu(KeyCode keyCode)
        {
            if (Input.GetKeyDown(keyCode))
            {
                var slotNum = keyCode - KeyCode.Alpha1;
                if (!_wholeInventoryHandler.GetEqupmentInventory().IsSlotEmpty(slotNum))
                {
                    _itemMenuController.Set(_wholeInventoryHandler.GetFromEquipment(slotNum));
                    _openItemMenu.Execute();
                    _menuController.Push(_itemMenuInputController);
                }
            }
        }

        public SlotMenu GetAssociatedSlotMenu() => SlotMenu.EquipableMenu;
    }
}