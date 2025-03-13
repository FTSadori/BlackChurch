using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Mechanics.AttackInterfaces;
using UnityEngine;

namespace Client.Runtime.Game.Mechanics.Bullets
{
    public class DestroyableBullet : MonoBehaviour, IBullet
    {
        float damage = 0;
        float pureDamage = 0;
        Vector2 force = new();
        float timeToDestroy = 3f;
        IBulletSender bulletSender = null;
        bool destroyOnHit = false;

        public void SetValues(float damage, float pureDamage, Vector2 force, float timeToDestroy, IBulletSender bulletSender, bool destroyOnHit)
        {
            this.damage = damage;
            this.pureDamage = pureDamage;
            this.force = force;
            this.timeToDestroy = timeToDestroy;
            this.bulletSender = bulletSender;
            this.destroyOnHit = destroyOnHit;
        }

        public void Update()
        {
            timeToDestroy -= Time.deltaTime;
            if (timeToDestroy < 0f)
                Destroy(this.gameObject);
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
            if (destroyOnHit) 
                Destroy(this.gameObject);
        }

        public float GetPureDamage()
        {
            return pureDamage;
        }
    }
}