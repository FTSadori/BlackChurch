using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Mechanics.AttackInterfaces;
using UnityEngine;

namespace Client.Runtime.Game.Mechanics.Bullets
{
    public class UndestroyableBullet : MonoBehaviour, IBullet
    {
        float damage = 0;
        float pureDamage = 0;
        Vector2 force = new();
        IBulletSender bulletSender = null;

        public void SetValues(float pureDamage, float damage, Vector2 force, IBulletSender bulletSender)
        {
            this.pureDamage = pureDamage;
            this.damage = damage;
            this.force = force;
            this.bulletSender = bulletSender;
        }

        public float GetDamage()
        {
            return damage;
        }

        public Vector2 GetForce()
        {
            return force;
        }

        public void OnHit(IHittable hittable)
        {
            bulletSender.OnAttack(hittable);
        }

        public float GetPureDamage()
        {
            return pureDamage;
        }
    }
}