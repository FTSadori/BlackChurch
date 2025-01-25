using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity.MenuInput;
using UnityEngine;

namespace Client.Runtime.Game.UI.Menu
{
    public sealed class MenuController : MonoBehaviour
    {
        [SerializeField] private NoMenuInputController _noMenuInputController;
        [SerializeField] private CanvasCursorController _cursorController;
        private readonly Stack<IMenuInputController> _menuStack = new();

        private void Awake() 
        {
            _menuStack.Push(_noMenuInputController);
        }

        private void Update() 
        {
            _menuStack.Peek().CheckInput();
        }

        public void Push(IMenuInputController menuInputController)
        {
            _menuStack.Push(menuInputController);
            _cursorController.SetCurrentSlotMenu(menuInputController.GetAssociatedSlotMenu());
        }

        public void Pop()
        {
            _menuStack.Pop();
            _cursorController.SetCurrentSlotMenu(_menuStack.Peek().GetAssociatedSlotMenu());
        }
    }
}