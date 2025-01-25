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
using Client.Runtime.Game.Mechanics.Inventory.Data;

namespace Client.Runtime.Game.Mechanics.Inventory
{
    public sealed class ItemMenuController : MonoBehaviour
    {
        [SerializeField] private ItemListHandler _itemListHandler;
        [SerializeField] private RarityColorsScriptableObject _rarityColorsScriptableObject;
        [SerializeField] private CraftMenuController _craftMenuController;


        [Header("MenuComponents")]
        [SerializeField] private ItemSlotController _bigItemSlotController;
        [SerializeField] private TMP_Text _nameField;
        [SerializeField] private TMP_Text _typeField;
        [SerializeField] private TMP_Text _rarityField;
        [SerializeField] private TMP_Text _descriptionField;

        [SerializeField] private Button _craftButton;
        [SerializeField] private Button _useOrEquipButton;
        [SerializeField] private Button _discardButton;

        [SerializeField] private RectTransform _contentOfCraftField;
        [SerializeField] private GameObject _prefabOfSlot;

        private string currentId = "";

        private List<string> listOfCraftables = new();

        private ItemData currentItemData;

        public string GetCurrentId() => currentId;

        public List<string> GetListOfCraftables() => listOfCraftables;

        public ItemData GetItemData() => currentItemData;

        public void Set(ItemData itemData)
        {
            currentId = itemData.id;
            currentItemData = itemData;

            _bigItemSlotController.Set(itemData.id, 1);
            _nameField.text = itemData.nameId;
            _nameField.color = _rarityColorsScriptableObject.rarityColors[(int)_itemListHandler.GetObjectById(itemData.id).rarity];
            _rarityField.text = "Rarity:\n" + itemData.rarityId;
            _typeField.text = "Type:\n" + itemData.typeId;
            _descriptionField.text = itemData.descriptionId;

            _discardButton.interactable = itemData.discardButtonActive;
            _craftButton.interactable = itemData.craftButtonActive;
            _useOrEquipButton.interactable = itemData.useButtonActive;

            var list = _itemListHandler.GetWhatCanBeCraftedFrom(itemData.id);
            listOfCraftables = list;
            float width = Mathf.Max(856f, 100f + 90f * list.Count);

            for (int i = 0; i < _contentOfCraftField.childCount; ++i)
            {
                Destroy(_contentOfCraftField.GetChild(i).gameObject);
            }

            for (int i = 0; i < list.Count; ++i)
            {
                var obj = Instantiate(_prefabOfSlot, _contentOfCraftField);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(50f + 90f * i, 0);
                var controller = obj.GetComponentInChildren<ItemSlotController>();
                controller.Set(list[i], 1);
                controller.SetHelpButtonText((i + 1 <= 9) ? (i + 1).ToString() : "");
            }

            _contentOfCraftField.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);

            if (itemData.craftButtonActive)
            {   
                _craftMenuController.Set(itemData);
            }
        }
    }
}