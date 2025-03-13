using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity;
using Client.Runtime.Game.Mechanics;
using Client.Runtime.Game.Mechanics.AttackInterfaces;
using Client.Runtime.Game.Mechanics.Bullets;
using Client.Runtime.Game.ScriptableObjects.Items;
using Codice.CM.Common;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Client.Runtime.Game.Player.Commands
{
    public class PlayerMeleeAttackDto
    {
        public float angle;
        public CharacterStats currentStats;
        public MeleeWeaponScriptableObject meleeObject;
        public IBulletSender bulletSender;

        public PlayerMeleeAttackDto(CharacterStats _currentStats, MeleeWeaponScriptableObject _meleeObject, float _angle, IBulletSender _bulletSender)
        {
            angle = _angle;
            currentStats = _currentStats;
            meleeObject = _meleeObject;
            bulletSender = _bulletSender;
        }
    }

    public sealed class PlayerMeleeAttackCommand : MonoCommand<PlayerMeleeAttackDto, int>
    {
        [SerializeField] GameObject leftAttack;
        [SerializeField] GameObject rightAttack;
        [SerializeField] GameObject topAttack;
        [SerializeField] GameObject bottomAttack;

        const float baseCooldownTime = 0.2f;
        const float attackAnimationTime = 0.2f;
        float attackAnimationTimer = -1f;
        float cooldownTimer = -1f;

        void Update()
        {
            if (attackAnimationTimer > 0f)
            {
                attackAnimationTimer -= Time.deltaTime;
                if (attackAnimationTimer <= 0f) {
                    HideAll();
                }
            }
            else if (cooldownTimer > 0f)
            {
                cooldownTimer -= Time.deltaTime;
            }
        }

        void HideAll()
        {
            leftAttack.SetActive(false);
            rightAttack.SetActive(false);
            topAttack.SetActive(false);
            bottomAttack.SetActive(false);
        }

        void ShowAndScale(GameObject attack, PlayerMeleeAttackDto dto, float X, float Y, bool swap)
        {
            attack.GetComponent<UndestroyableBullet>().SetValues(dto.currentStats.pureAttack, dto.currentStats.baseAttack, new Vector2(X, Y), dto.bulletSender);
            attack.SetActive(true);
            if (swap)
            {
                attack.transform.localScale = new Vector3(dto.meleeObject.xRange, dto.meleeObject.yRange, 1f);
                attack.transform.localPosition = new Vector2(X * dto.meleeObject.xRange / 2f, Y * dto.meleeObject.yRange / 2f);   
            }
            else
            {
                attack.transform.localScale = new Vector3(dto.meleeObject.yRange, dto.meleeObject.xRange, 1f);
                attack.transform.localPosition = new Vector2(X * dto.meleeObject.yRange / 2f, Y * dto.meleeObject.xRange / 2f);
            }
            attackAnimationTimer = attackAnimationTime;
            cooldownTimer = baseCooldownTime / dto.meleeObject.defaultAttackSpeed / dto.currentStats.attackSpeedBuff;
        }

        public override int Execute(PlayerMeleeAttackDto dto)
        {
            if (cooldownTimer > 0f) return 1;

            if (dto.angle >= -45f && dto.angle <= 45f)
            {
                ShowAndScale(rightAttack, dto, 1f, 0f, false);
            }
            else if (dto.angle >= 45f && dto.angle <= 135f)
            {
                ShowAndScale(topAttack, dto, 0f, 1f, true);
            }
            else if (dto.angle >= 135f && dto.angle <= 225f)
            {
                ShowAndScale(leftAttack, dto, -1f, 0f, false);
            }
            else if (dto.angle >= 255f || dto.angle <= -45f)
            {
                ShowAndScale(bottomAttack, dto, 0f, -1f, true);
            }

            return 0;
        }
    }
}