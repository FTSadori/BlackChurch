using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.ScriptableObjects.Visuals;
using Client.Runtime.Game.UI;
using UnityEngine;
using TMPro;
using Client.Runtime.Game.ScriptableObjects;
using UnityEngine.UI;

namespace Client.Runtime.Game.Mechanics.Inventory
{
    public sealed class ItemMenuController : MonoBehaviour
    {
        [SerializeField] private ItemListHandler _itemListHandler;
        [SerializeField] private RarityColorsScriptableObject _rarityColorsScriptableObject;


        [Header("MenuComponents")]
        [SerializeField] private ItemSlotController _bigItemSlotController;
        [SerializeField] private TMP_Text _nameField;
        [SerializeField] private TMP_Text _typeField;
        [SerializeField] private TMP_Text _rarityField;
        [SerializeField] private TMP_Text _descriptionField;

        [SerializeField] private Button _craftButton;
        [SerializeField] private Button _useOrEquipButton;
        [SerializeField] private TMP_Text _useOrEquipField;
        [SerializeField] private Button _discardButton;

        [SerializeField] private RectTransform _contentOfCraftField;
        [SerializeField] private GameObject _prefabOfSlot;

        private string currentId = "";

        public string GetCurrentId()
        {
            return currentId;
        }

        public void Set(string id)
        {
            currentId = id;

            var itemObj = _itemListHandler.GetObjectById(id);
            
            _bigItemSlotController.Set(id, 1);
            _nameField.text = itemObj.id;
            _nameField.color = _rarityColorsScriptableObject.rarityColors[(int)itemObj.rarity];
            _rarityField.text = "Rarity:\n" + itemObj.rarity.ToString();
            switch (itemObj.itemType)
            {
                case ItemType.CONSUMABLE:
                    _useOrEquipField.text = "Use";
                    _useOrEquipButton.interactable = true;
                    _typeField.text = "Type:\nConsumable";
                    break;
                case ItemType.MATERIAL:
                    _typeField.text = "Type:\nMaterial";
                    break;
                case ItemType.EQUIPABLE:
                    _useOrEquipField.text = "Equip";
                    _useOrEquipButton.interactable = true;
                    if (itemObj is EquipableScriptableObject equipableObj)
                    {
                        _typeField.text = "Type:\n" + equipableObj.equipableType.ToString();
                    }
                    else
                        _typeField.text = "Type:\nError";
                    break;
            }
            _descriptionField.text = itemObj.id + "_desc";
            _discardButton.interactable = true;
            if (itemObj.craftsFromId1 != "" && itemObj.craftsFromId2 != "")
            {
                _craftButton.interactable = true;
            }

            var list = _itemListHandler.GetWhatCanBeCraftedFrom(id);
            float width = Mathf.Max(856f, 100f + 90f * list.Count);

            for (int i = 0; i < _contentOfCraftField.childCount; ++i)
            {
                Destroy(_contentOfCraftField.GetChild(i).gameObject);
            }

            for (int i = 0; i < list.Count; ++i)
            {
                var obj = Instantiate(_prefabOfSlot, _contentOfCraftField);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(50f + 90f * i, 0);
                obj.GetComponentInChildren<ItemSlotController>().Set(list[i], 1);
            }

            _contentOfCraftField.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        }
    }
}