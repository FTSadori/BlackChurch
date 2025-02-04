using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.ScriptableObjects.Visuals;
using Client.Runtime.Game.UI.AnimationControllers;
using UnityEngine;

namespace Client.Runtime.Game.Environment.DroppedItems
{
    public sealed class DroppedItemController : MonoBehaviour, ISelectable
    {
        [SerializeField] private GameObject _selection;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private SpriteRenderer _backImageSpriteRenderer;
        [SerializeField] private RarityColorsScriptableObject _rarityColorsScriptableObject;
        [SerializeField] private GameObject _wholeObject;
        [SerializeField] private ItemListHandler _itemListHandler;

        private string _id;
        private int _quantity;
        private InventoryData _connectedInventoryData;

        public HelpBoxType HelpBoxType => HelpBoxType.ONE_BUTTON;

        public void Set(string id, int quantity, InventoryData connectedInventoryData)
        {
            var itemObj = _itemListHandler.GetObjectById(id);
            var rarityColor = _rarityColorsScriptableObject.rarityColors[(int)itemObj.rarity];
            var transparentVersion = rarityColor;
            transparentVersion.a = 0f;

            _spriteRenderer.sprite = itemObj.sprite;
            _lineRenderer.startColor = rarityColor;
            _lineRenderer.endColor = transparentVersion;
            _backImageSpriteRenderer.color = rarityColor;
            _id = id;
            _quantity = quantity;
            _connectedInventoryData = connectedInventoryData;
        }

        public void Deselect()
        {
            _selection.SetActive(false);
        }

        public void Interact(InteractType type)
        {
            if (type == InteractType.First)
            {
                _quantity = _connectedInventoryData.AddItem(_id, _quantity);
                if (_quantity == 0)
                {
                    Destroy(_wholeObject);
                }
            }
        }

        public void Select()
        {
            _selection.SetActive(true);            
        }
    }
}