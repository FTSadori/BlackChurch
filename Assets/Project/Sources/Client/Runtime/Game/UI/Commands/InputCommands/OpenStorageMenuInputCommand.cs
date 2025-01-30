using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.Mechanics.Inventory.MenuControllers;
using Client.Runtime.Game.UI.Menu;
using UnityEngine;

namespace Client.Runtime.Game.UI.Commands.InputCommands
{
    public sealed class OpenStorageMenuInputCommand : MonoCommand
    {
        [SerializeField] OpenMenuCommand _openStorageMenu;
        [SerializeField] MenuController _menuController;
        [SerializeField] StorageMenuInputController _storageMenuInputController;
        [SerializeField] StorageMenuController _storageMenuController;
        [SerializeField] GameObject _newButtonLayer;
        [NonSerialized] public InventoryData _inventoryDataToOpen = null;
        [NonSerialized] public string _storageName = null;
        [NonSerialized] public bool _canPutIn = true;

        public override void Execute()
        {
            if (_inventoryDataToOpen != null)
            {
                PutInStorageSlotInputCommand._canPutIn = _canPutIn;
                _newButtonLayer.SetActive(true);
                _storageMenuController.ConnectStorageTo(_inventoryDataToOpen);
                _openStorageMenu.Execute();
                _storageMenuController.Set(_inventoryDataToOpen, _storageName);
                _menuController.Push(_storageMenuInputController);
            }
            _inventoryDataToOpen = null;
            _storageName = null;
        }
    }
}