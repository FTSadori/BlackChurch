using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.UI.AnimationControllers;
using Client.Runtime.Game.UI.Commands;
using Client.Runtime.Game.UI.Commands.InputCommands;
using UnityEngine;

namespace Client.Runtime.Game.Environment.Storage
{
    public sealed class StorageController : MonoBehaviour, ISelectable
    {
        [SerializeField] GameObject _selectionTexture;
        [SerializeField] public OpenStorageMenuInputCommand _openStorageMenuInputCommand;
        [SerializeField] StorageModel _storageModel;
        [SerializeField] public string _name;
        bool CanPutIn => _storageModel.CanPutIn;

        public HelpBoxType HelpBoxType => HelpBoxType.ONE_BUTTON;

        public void Deselect()
        {
            _selectionTexture.SetActive(false);
        }

        public void Interact(InteractType interactType)
        {
            if (interactType == InteractType.First)
            {
                _openStorageMenuInputCommand._inventoryDataToOpen = _storageModel.InventoryData;
                _openStorageMenuInputCommand._storageName = _name;
                _openStorageMenuInputCommand._canPutIn = CanPutIn;
                _openStorageMenuInputCommand.Execute();
            }
        }

        public void Select()
        {
            _selectionTexture.SetActive(true);
        }
    }
}