using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.UI;
using UnityEngine;
using TMPro;
using Client.Runtime.Game.Environment.Storage;
using Client.Runtime.Game.UI.Commands.InputCommands;

namespace Client.Runtime.Game.Mechanics.Inventory.MenuControllers
{
    public sealed class StorageMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _slotPrefab;
        [SerializeField] private RectTransform _menuBox;
        [SerializeField] private TMP_Text _storageTitle;
        [SerializeField] private ToolbarModel _toolbarModel;
        [SerializeField] private ToolbarController _toolbarController;

        private InventoryData _currentInventory = null;
        
        public List<PutInStorageSlotInputCommand> _commands = new();
        private List<ItemSlotController> _slotControllers = new();

        private void Awake() {
            float startX = -210f;
            Vector3 currentPoint = new (-210f, 170f);
            float delta = 140f;

            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    var obj = Instantiate(_slotPrefab, _menuBox);
                    obj.GetComponent<RectTransform>().anchoredPosition = currentPoint;
                    var slot = obj.GetComponentInChildren<ItemSlotController>();
                    var command = slot.gameObject.AddComponent<GetFromStorageSlotInputCommand>();
                    command._slotNum = i * 4 + j;
                    command._toolbarModel = _toolbarModel;
                    command._storageMenuController = this;
                    command._toolbarController = _toolbarController;
                    slot.SetNewButtonCommand(command);
                    slot.SetHelpButtonText("");
                    _slotControllers.Add(slot);
                    currentPoint.x += delta;
                }
                currentPoint.x = startX;
                currentPoint.y -= delta;
            }
        }

        public void ConnectStorageTo(InventoryData inventoryData)
        {
            foreach (var command in _commands)
            {
                command._storageInventory = inventoryData;
            }

            foreach (var slot in _slotControllers)
            {
                slot.gameObject.GetComponent<GetFromStorageSlotInputCommand>()._storageInventory = inventoryData;
            }
            Reset();
        }
        
        public void Reset() {
            foreach (var slot in _slotControllers)
            {
                slot.Set("", 0);
            }
        }

        public void Set(InventoryData inventoryData, string name) {
            if (_currentInventory != null)
            {
                _currentInventory.OnUpdateInventory -= UpdateInventory;
            }

            _currentInventory = inventoryData;
            _currentInventory.OnUpdateInventory += UpdateInventory;
            
            _storageTitle.text = name;
            for (int i = 0; i < inventoryData.Count(); ++i)
            {
                var item = inventoryData.GetBySlotNumber(i);
                if (item.id == "") return;
                _slotControllers[i].Set(item.id, item.quantity);
            }
        }

        private void UpdateInventory()
        {
            for (int i = 0; i < _slotControllers.Count; i++)
            {
                var data = _currentInventory.GetBySlotNumber(i);
                _slotControllers[i].Set(data.id, data.quantity);
            }
        }

        private void OnDestroy() {
            if (_currentInventory != null)
            {
                _currentInventory.OnUpdateInventory -= UpdateInventory;
            }
        }
    }
}