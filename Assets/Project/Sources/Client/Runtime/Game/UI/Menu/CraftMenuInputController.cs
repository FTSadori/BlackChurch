using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity.MenuInput;
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

        private readonly List<KeyCode> _craftSlotsCodes = new(){
            KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3,
        };

        public void CheckInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
            {
                _closeCraftMenu.Execute();
                _menuController.Pop();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                _closeCraftMenu.Execute();
                _menuController.Pop();
                _closeItemMenu.Execute();
                _menuController.Pop();
            }
            else
            {
                foreach (KeyCode keyCode in _craftSlotsCodes)
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        _menuController.Pop();
                    }
                }
            }
        }
    }
}