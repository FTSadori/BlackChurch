using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity.MenuInput;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.Player;
using UnityEngine;

namespace Client.Runtime.Game.UI.Menu
{
    public sealed class MenuController : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private NoMenuInputController _noMenuInputController;
        [SerializeField] private CanvasCursorController _cursorController;
        private readonly Stack<IMenuInputController> _menuStack = new();

        private void Awake() 
        {
            _menuStack.Push(_noMenuInputController);
            _noMenuInputController.IsInputActive = true;
        }

        private void Update() 
        {
            foreach (var command in _menuStack.Peek().KeyDownCommands)
            {
                if (command.Execute()) break;
            }
        }

        public void Push(IMenuInputController menuInputController)
        {
            _playerController.CanMove = false;

            _menuStack.Peek().IsInputActive = false;
            menuInputController.IsInputActive = true;
            _menuStack.Push(menuInputController);
            _cursorController.SetCurrentSlotMenu(menuInputController.GetAssociatedSlotMenu());
        }

        public void Pop()
        {
            _menuStack.Peek().IsInputActive = false;
            _menuStack.Pop();
            _menuStack.Peek().IsInputActive = true;
            _cursorController.SetCurrentSlotMenu(_menuStack.Peek().GetAssociatedSlotMenu());

            if (_menuStack.Count == 1)
            {
                _playerController.CanMove = true;
            }
        }
    }
}