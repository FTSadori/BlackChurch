using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackChurch.Assets.Project.Sources.Client.Runtime.Game.UI.Menu;
using Client.Runtime.Framework.Unity.MenuInput;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.UI.Commands;
using UnityEngine;

namespace Client.Runtime.Game.UI.Menu
{
    public sealed class NoMenuInputController : MonoBehaviour, IMenuInputController
    {
        [SerializeField] MenuController _menuController;
        [SerializeField] OpenMenuCommand _openItemMenu;
        [SerializeField] OpenMenuCommand _openEquipmentMenu;
        [SerializeField] EquipmentMenuInputController _equipmentMenuInputContoller;
        [SerializeField] ItemMenuInputController _itemMenuInputController;
        [SerializeField] ItemMenuController _itemMenuController;
        [SerializeField] ToolbarModel _toolbarModel;

        private readonly List<KeyCode> _toolbarCodes = new(){
            KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3,
            KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6,
            KeyCode.Alpha7
        };

        public void CheckInput()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _openEquipmentMenu.Execute();
                _menuController.Push(_equipmentMenuInputContoller);
            }
            else
            {
                foreach (KeyCode keyCode in _toolbarCodes)
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
                if (!_toolbarModel.InventoryData.IsSlotEmpty(slotNum))
                {
                    _itemMenuController.Set(_toolbarModel.InventoryData.GetBySlotNumber(slotNum).id);
                    _openItemMenu.Execute();
                    _menuController.Push(_itemMenuInputController);
                }
            }
        }
    }
}