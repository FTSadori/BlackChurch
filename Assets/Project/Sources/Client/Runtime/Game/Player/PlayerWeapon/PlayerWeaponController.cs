using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Mechanics.Attack;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.ScriptableObjects;
using Client.Runtime.Game.ScriptableObjects.Items;
using Client.Runtime.Game.ScriptableObjects.Visuals;
using UnityEngine;

namespace Client.Runtime.Game.Player.PlayerWeapon
{
    public sealed class PlayerWeaponController : MonoBehaviour
    {
        [SerializeField] PlayerController _playerController;
        [SerializeField] EquipmentModel _equipmentModel;
        [SerializeField] ItemListHandler _itemListHandler;
        [SerializeField] SpriteRenderer _spriteRenderer;
        [SerializeField] AttackCursorController _attackCursorController;
        [SerializeField] RarityColorsScriptableObject _rarityColorsScriptableObject;
        
        bool LeftActive => _playerController.LastDirection == -1;

        string currentId;

        public void ShowWeapon(Rarity rarity)
        {
            _spriteRenderer.gameObject.SetActive(true);
            _spriteRenderer.color = _rarityColorsScriptableObject.rarityColors[(int)rarity];
        }

        public void HideWeapon()
        {
            _spriteRenderer.gameObject.SetActive(false);
        }

        private void Update() {
            if (currentId != _equipmentModel.InventoryData.GetBySlotNumber(0).id)
            {
                currentId = _equipmentModel.InventoryData.GetBySlotNumber(0).id;
                if (currentId == "")
                {
                    HideWeapon();
                }
                else if (_itemListHandler.GetObjectById(currentId) is EquipableScriptableObject equipableSO)
                {
                    if (equipableSO is MeleeWeaponScriptableObject)
                    {
                        ShowWeapon(equipableSO.rarity);
                    }
                    else
                    {
                        HideWeapon();
                    }
                }    
            }

            _spriteRenderer.flipX = LeftActive;
        }
    }
}