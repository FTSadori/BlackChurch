using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Mechanics.Attack;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.ScriptableObjects;
using UnityEngine;

namespace Client.Runtime.Game.Player.PlayerWeapon
{
    public sealed class PlayerWeaponController : MonoBehaviour
    {
        [SerializeField] GameObject _leftSprite;
        [SerializeField] GameObject _rightSprite;
        [SerializeField] AttackCursorController _attackCursorController; 
        [SerializeField] PlayerController _playerController;
        [SerializeField] EquipmentModel _equipmentModel;
        [SerializeField] WeaponType _weaponType;
        [SerializeField] ItemListHandler _itemListHandler;

        bool showWeapon = false;
        bool LeftActive => _playerController.LastDirection == -1;

        string currentId;

        public void ShowWeapon(Sprite sprite)
        {
            showWeapon = true;
            // set sprite
            Debug.LogError("Add sprite changing");
            _playerController.AutoChangeDirection = _weaponType != WeaponType.RANGED;
            
            //_leftSprite.sprite = sprite;
            //_rightSprite.sprite = sprite;
        }

        public void HideWeapon()
        {
            showWeapon = false;
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
                    if (equipableSO.weaponType == _weaponType)
                    {
                        ShowWeapon(null);
                    }
                    else
                    {
                        HideWeapon();
                    }
                }    
            }

            _leftSprite.SetActive(showWeapon && LeftActive);
            _rightSprite.SetActive(showWeapon && !LeftActive);
            
            if (showWeapon && _weaponType == WeaponType.RANGED)
            {
                _playerController.LastDirection = (_attackCursorController.CursorAngle < 90f) ? 1 : -1;
                
                if (LeftActive)
                {
                    _leftSprite.transform.rotation = Quaternion.Euler(0f, 0f, _attackCursorController.CursorAngle - 180f);
                }
                else
                    _rightSprite.transform.rotation = Quaternion.Euler(0f, 0f, _attackCursorController.CursorAngle);
            }
        }
    }
}