using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.ScriptableObjects.Visuals;
using Client.Runtime.Game.ScriptableObjects;
using Client.Runtime.Game.ScriptableObjects.Lists;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using TMPro;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Framework.Unity;

namespace Client.Runtime.Game.UI
{
    public enum SlotMenu
    {
        Toolbar,
        EquipableMenu,
        ItemMenu,
        CraftMenu,
        StorageMenu,
    }

    public sealed class ItemSlotController : MonoBehaviour
    {
        [SerializeField] ItemListHandler _itemListHandler;
        [SerializeField] RarityColorsScriptableObject _rarityColorsScriptableObject;
        [SerializeField] GameObject _selectionFrame;
        [SerializeField] Image _rarityFrame;
        [SerializeField] Image _itemImage;
        [SerializeField] GameObject _itemImageObject;
        [SerializeField] TMP_Text _countText;
        [SerializeField] SlotMenu _slotMenu;
        [SerializeField] GameObject _helpButtonObj;
        [SerializeField] TMP_Text _helpButtonText;
        [SerializeField] SerializableButtonCommand _buttonCommand;

        public SlotMenu GetSlotMenu() => _slotMenu;

        public void SetNewButtonCommand(MonoCommand command)
        {
            _buttonCommand._command = command;
        }

        public void SetHelpButtonText(string text)
        {
            _helpButtonObj.SetActive(text != "");
            _helpButtonText.text = text;
        }

        public void Set(string id, int count)
        {
            if (id == "")
            {
                Clear();
                return;
            }
            var item = _itemListHandler.GetObjectById(id);

            _itemImageObject.SetActive(true);
            _rarityFrame.color = _rarityColorsScriptableObject.rarityColors[(int)item.rarity];
            _itemImage.sprite = item.sprite;
            _countText.text = (count == 1) ? "" : count.ToString();
        }

        public void Select()
        {
            _selectionFrame.SetActive(true);
        }

        public void Deselect()
        {
            _selectionFrame.SetActive(false);
        }

        public void Clear()
        {
            _itemImageObject.SetActive(false);
            _rarityFrame.color = new Color(0, 0, 0, 0);
            _countText.text = "";
        }
    }
}