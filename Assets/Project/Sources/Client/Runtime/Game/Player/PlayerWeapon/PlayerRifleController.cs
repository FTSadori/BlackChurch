using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Mechanics.Attack;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.ScriptableObjects;
using Client.Runtime.Game.ScriptableObjects.Visuals;
using UnityEngine;

namespace Client.Runtime.Game.Player.PlayerWeapon
{
    public sealed class PlayerRifleController : MonoBehaviour
    {
        [SerializeField] PlayerController _playerController;
        [SerializeField] PlayerView _playerView;
        [SerializeField] EquipmentModel _equipmentModel;
        [SerializeField] ItemListHandler _itemListHandler;
        [SerializeField] SpriteRenderer _spriteRenderer;
        [SerializeField] Transform _transform;
        [SerializeField] AttackCursorController _attackCursorController;
        [SerializeField] RarityColorsScriptableObject _rarityColorsScriptableObject;
        
        bool _rifleActive = false;

        bool LeftActive => _playerController.LastDirection == -1;

        string currentId;

        public void ShowWeapon(Rarity rarity)
        {
            _rifleActive = true;
            _transform.gameObject.SetActive(true);
            _playerController.AutoChangeDirection = false;
            _spriteRenderer.color = _rarityColorsScriptableObject.rarityColors[(int)rarity];
            _playerView.NoHandsSprite.color = Color.white;
            _playerView.SpriteRenderer.color = new Color(0, 0, 0, 0);
        }

        public void HideWeapon()
        {
            _rifleActive = false;
            _transform.gameObject.SetActive(false);
            _playerController.AutoChangeDirection = true;
            _playerView.NoHandsSprite.color = new Color(0, 0, 0, 0);
            _playerView.SpriteRenderer.color = Color.white;
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
                    if (equipableSO.weaponType == WeaponType.RANGED)
                    {
                        ShowWeapon(equipableSO.rarity);
                    }
                    else
                    {
                        HideWeapon();
                    }
                }    
            }

            if (_rifleActive)
            {
                _transform.gameObject.SetActive(true);

                _playerController.LastDirection = (_attackCursorController.CursorAngle < 90f) ? 1 : -1;
                _transform.localPosition = new Vector3(-0.244f * _playerController.LastDirection, 0.406f, 0f);
                _transform.localScale = new Vector3(_playerController.LastDirection, 1f, 1f);

                if (LeftActive)
                {
                    _transform.rotation = Quaternion.Euler(0f, 0f, _attackCursorController.CursorAngle - 180f);
                }
                else
                {
                    _transform.rotation = Quaternion.Euler(0f, 0f, _attackCursorController.CursorAngle);
                }
            }
        }

    }
}