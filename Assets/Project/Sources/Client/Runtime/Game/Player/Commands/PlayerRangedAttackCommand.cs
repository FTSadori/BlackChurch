using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity;
using Client.Runtime.Game.Mechanics;
using Client.Runtime.Game.Mechanics.AttackInterfaces;
using Client.Runtime.Game.Mechanics.Bullets;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.ScriptableObjects.Items;
using UnityEngine;

namespace Client.Runtime.Game.Player.Commands
{
    public class PlayerRangedAttackDto
    {
        public Vector2 direction;
        public CharacterStats currentStats;
        public RangedWeaponScriptableObject rangedObject;
        public IBulletSender bulletSender;

        public PlayerRangedAttackDto(CharacterStats _currentStats, RangedWeaponScriptableObject _rangedObject, Vector2 _direction, IBulletSender _bulletSender)
        {
            direction = _direction;
            currentStats = _currentStats;
            rangedObject = _rangedObject;
            bulletSender = _bulletSender;
        }
    }

    public sealed class PlayerRangedAttackCommand : MonoCommand<PlayerRangedAttackDto, int>
    {
        [SerializeField] GameObject bulletPrefab;
        [SerializeField] GameObject center;
        [SerializeField] ToolbarModel toolbarModel;

        const float baseCooldown = 0.2f;
        float cooldownTimer = -1f;

        void Update()
        {
            if (cooldownTimer > 0f)
            {
                cooldownTimer -= Time.deltaTime;
            }
        }

        public override int Execute(PlayerRangedAttackDto data)
        {
            if (cooldownTimer > 0f) return 1;
            if (data.rangedObject.ammoId != "" && toolbarModel.InventoryData.GetItemCount(data.rangedObject.ammoId) == 0) return 1;

            if (data.rangedObject.ammoId != "")
                toolbarModel.InventoryData.RemoveItemAtSlot(0, data.rangedObject.ammoId, 1, true);

            var obj = Instantiate(bulletPrefab);
            obj.GetComponent<DestroyableBullet>().SetValues(data.currentStats.pureAttack, data.currentStats.baseAttack, data.direction * 2f, 3f, data.bulletSender, true);
            obj.transform.position = center.transform.position + new Vector3(data.direction.x, data.direction.y) * 2f;
            var force = data.rangedObject.ammoSpeed + data.currentStats.ammoSpeedBuff;
            obj.GetComponent<Rigidbody2D>().AddForce(data.direction * force, ForceMode2D.Impulse);
            obj.transform.localScale = Vector3.one * data.rangedObject.ammoSize;
            cooldownTimer = baseCooldown / data.rangedObject.shootSpeed / data.currentStats.attackSpeedBuff;

            return 0;
        }
    }
}