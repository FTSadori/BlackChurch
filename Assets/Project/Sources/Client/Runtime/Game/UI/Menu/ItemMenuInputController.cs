using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity.MenuInput;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.UI.Commands;
using UnityEngine;

namespace Client.Runtime.Game.UI.Menu
{
    public sealed class ItemMenuInputController : MonoBehaviour, IMenuInputController
    {
        [SerializeField] MenuController _menuController;
        [SerializeField] CloseMenuCommand _closeItemMenu;
        [SerializeField] OpenMenuCommand _openCraftMenu;
        [SerializeField] OpenMenuCommand _openItemMenu;
        [SerializeField] CraftMenuInputController _craftMenuInputController;
        [SerializeField] CraftMenuController _craftMenuController;
        [SerializeField] ItemMenuController _itemMenuController;
        [SerializeField] ItemListHandler _itemListHandler;

        private readonly List<KeyCode> _usedInCraftCodes = new(){
            KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3,
            KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6,
            KeyCode.Alpha7
        };

        public void CheckInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
            {
                _closeItemMenu.Execute();
                _menuController.Pop();
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                _craftMenuController.Set(_itemMenuController.GetCurrentId());

                _openCraftMenu.Execute();
                _menuController.Push(_craftMenuInputController);
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                _closeItemMenu.Execute();
                _menuController.Pop();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                _closeItemMenu.Execute();
                _menuController.Pop();
            }
            else
            {
                foreach (KeyCode keyCode in _usedInCraftCodes)
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        // update window
                    }
                }
            }
        }
    }
}