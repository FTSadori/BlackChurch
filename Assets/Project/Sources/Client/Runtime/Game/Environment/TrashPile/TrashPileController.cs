using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Environment.TrashPile.UI;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.UI;
using Client.Runtime.Game.UI.AnimationControllers;
using UnityEditor;
using UnityEngine;

namespace Client.Runtime.Game.Environment.TrashPile
{
    public class TrashPileController : MonoBehaviour, ISelectable
    {
        [SerializeField] GameObject _selectionTexture;
        [SerializeField] GameObject _barObject;
        [SerializeField] TrashPileModel _trashPileModel;
        [SerializeField] TimeBarController _barController;
        [SerializeField] GameObject _slotCanvas;
        [SerializeField] ItemSlotController _itemSlotController;
        [SerializeField] ToolbarController _toolbarController;
        [SerializeField] ToolbarModel _toolbarModel;

        public HelpBoxType HelpBoxType => 
            (_trashPileModel._state == TrashPileState.FOUND) 
                ? HelpBoxType.TWO_BUTTONS 
                : HelpBoxType.ONE_BUTTON;

        private void Awake() {
            _trashPileModel._state = TrashPileState.AWAITS;
            _barController.OnComplete += OnTimeUp;
        }

        public void Deselect()
        {
            _selectionTexture.SetActive(false);
        }

        public void Interact(InteractType interactType)
        {
            switch (interactType)
            {
                case InteractType.First:
                    InteractFirst();
                    break;
                case InteractType.Second:
                    InteractSecond();
                    break;
            }
        }

        public void Select()
        {
            _selectionTexture.SetActive(true);
        }

        private void InteractFirst()
        {
            if (_trashPileModel._state == TrashPileState.AWAITS)
            {
                if (_trashPileModel._itemRecords.Count > 0)
                {
                    _trashPileModel._state = TrashPileState.BUSY;
                    _barObject.SetActive(true);
                    _barController.StartTimer(_trashPileModel._trashPileScriptableObject.timeToFindItem);
                }
                else 
                {
                    Debug.Log("Pile is empty");
                }
            }
            else if (_trashPileModel._state == TrashPileState.FOUND)
            {
                var itemInfo = _trashPileModel.GetNextItem();
                if (_toolbarModel.InventoryData.TryAddItem(itemInfo.id, itemInfo.quantity))
                {
                    _toolbarController.UpdateInventory();
                    _trashPileModel.RemoveItem();
                    _trashPileModel._state = TrashPileState.AWAITS;
                    _slotCanvas.SetActive(false);
                }
                else
                {
                    Debug.Log("You don't have enough free space in your inventory");
                }
            }
        }

        private void InteractSecond()
        {
            if (_trashPileModel._state == TrashPileState.FOUND)
            {
                _trashPileModel.SkipItem();
                _trashPileModel._state = TrashPileState.AWAITS;
                _slotCanvas.SetActive(false);
            }
        }

        private void OnTimeUp()
        {
            _barObject.SetActive(false);
            _trashPileModel._state = TrashPileState.FOUND;
            _slotCanvas.SetActive(true);
            var itemInfo = _trashPileModel.GetNextItem();
            _itemSlotController.Set(itemInfo.id, itemInfo.quantity);
        }

        private void OnDestroy() {
            _barController.OnComplete -= OnTimeUp;
        }
    }
}