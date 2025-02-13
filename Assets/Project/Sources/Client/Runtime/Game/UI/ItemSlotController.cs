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
using Sources.Client.Runtime.Game.Mechanics;
using Client.Runtime.Game.Player;

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
        [SerializeField] GodsSigilsScriptableObject _godsSigilsScriptableObject;
        [SerializeField] GameObject _selectionFrame;
        [SerializeField] Image _rarityFrame;
        [SerializeField] Image _itemImage;
        [SerializeField] GameObject _itemImageObject;
        [SerializeField] TMP_Text _countText;
        [SerializeField] SlotMenu _slotMenu;
        [SerializeField] GameObject _helpButtonObj;
        [SerializeField] TMP_Text _helpButtonText;
        [SerializeField] SerializableButtonCommand _buttonCommand;
        [SerializeField] GameObject _statsShadow;
        [SerializeField] TMP_Text _statsText;
        [SerializeField] Image _sigil;

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
            if (id == "" || count == 0)
            {
                Clear();
                return;
            }
            var item = _itemListHandler.GetObjectById(id);

            _itemImageObject.SetActive(true);
            _rarityFrame.color = _rarityColorsScriptableObject.rarityColors[(int)item.rarity];
            _itemImage.sprite = item.sprite;
            _countText.text = (count == 1) ? "" : count.ToString();
            SetStatsText(item);
            SetSigil(item);
        }

        private void SetSigil(MaterialScriptableObject item)
        {
            _sigil.gameObject.SetActive(false);

            if (item.itemType == ItemType.EQUIPABLE && item is EquipableScriptableObject eitem)
            {
                if (eitem.godType != GodType.MAX_TYPE)
                {
                    _sigil.color = _godsSigilsScriptableObject.sigilColors[(int)eitem.godType];
                    _sigil.sprite = _godsSigilsScriptableObject.sigilSprites[(int)eitem.godType];
                    _sigil.gameObject.SetActive(true);
                }
            }
        }

        private void SetStatsText(MaterialScriptableObject item)
        {
            _statsShadow.SetActive(false);
            _statsText.text = "";            

            if (item.itemType == ItemType.CONSUMABLE && item is ConsumableScriptableObject citem)
            {
                if (citem.hpBuff != 0)
                {
                    _statsText.text = $"♥{citem.hpBuff}♥";
                }
                _statsShadow.SetActive(true);
            }
            else if (item.itemType == ItemType.EQUIPABLE && item is EquipableScriptableObject eitem)
            {
                if (eitem.equipableType == EquipableType.WEAPON)
                {
                    var value = PlayerModel.MainPlayerGodBuffs.GetPowerUppedValueFor(eitem.godType, eitem.additiveStats.baseAttack);
                    _statsText.text = $"♠{value:0.0}♠";
                }
                else
                {
                    var value = PlayerModel.MainPlayerGodBuffs.GetPowerUppedValueFor(eitem.godType, eitem.additiveStats.baseDefence);
                    _statsText.text = $"♦{value:0.0}♦";
                }
                _statsShadow.SetActive(true);
            }
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
            _statsShadow.SetActive(false);
            _statsText.text = "";
            _sigil.gameObject.SetActive(false);
        }
    }
}