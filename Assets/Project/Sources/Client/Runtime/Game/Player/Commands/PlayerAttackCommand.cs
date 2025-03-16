using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity;
using Client.Runtime.Game.Mechanics;
using Client.Runtime.Game.Mechanics.Attack;
using Client.Runtime.Game.Mechanics.Bullets;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.ScriptableObjects;
using Client.Runtime.Game.ScriptableObjects.Items;
using UnityEngine;

namespace Client.Runtime.Game.Player.Commands
{
    public sealed class PlayerAttackCommand : MonoCommand
    {
        [SerializeField] PlayerMeleeAttackCommand playerMeleeAttackCommand;
        [SerializeField] PlayerRangedAttackCommand playerRangedAttackCommand;
        [SerializeField] WholeInventoryHandler wholeInventoryHandler;
        [SerializeField] ItemListHandler itemListHandler;
        [SerializeField] private MeleeWeaponScriptableObject _baseWeapon;
        [SerializeField] AttackCursorController attackCursorController;
        [SerializeField] PlayerModel _playerModel;
        [SerializeField] PlayerBulletSender _playerBulletSender;

        public override void Execute()
        {
            string id = wholeInventoryHandler.GetEqupmentInventory().GetBySlotNumber(0).id;
            var obj = (id != "") ? itemListHandler.GetObjectById(id) : _baseWeapon;

            if (obj is MeleeWeaponScriptableObject mObject)
            {
                playerMeleeAttackCommand.Execute(new PlayerMeleeAttackDto(_playerModel.currentStats, mObject, attackCursorController.CursorAngle, _playerBulletSender));
            }
            else if (obj is RangedWeaponScriptableObject rObject)
            {
                playerRangedAttackCommand.Execute(new PlayerRangedAttackDto(_playerModel.currentStats, rObject, attackCursorController.CursorDirection, _playerBulletSender));
            }
        }
    }
}