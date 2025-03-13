using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Mechanics;
using Client.Runtime.Game.Mechanics.AttackInterfaces;
using TMPro;
using UnityEngine;

namespace Client.Runtime.Game.Mechanics.Bullets
{
    public class HittableEntityController : MonoBehaviour, IHittable
    {
        public SpriteRenderer _spriteRenderer;
        public Rigidbody2D _rigidbody2d;
        public AbstractEntityDataHolder _dataHolder;

        const float redishCooldown = 1f;
        float redishCooldownTimer = -1f;

        void Update() {
            if (redishCooldownTimer > 0f)
            {
                redishCooldownTimer -= Time.deltaTime;
                if (redishCooldownTimer <= 0f)
                    _spriteRenderer.color = Color.white;
            }
        }

        public void AttackedBy(IBullet bullet)
        {
            _rigidbody2d.AddForce(bullet.GetForce() * 2f, ForceMode2D.Impulse);
            _spriteRenderer.color = Color.red;
            redishCooldownTimer = redishCooldown;
            _dataHolder.ChangeHp(-bullet.GetDamage(), bullet.GetPureDamage());
        }

        public string GetName()
        {
            return _dataHolder.GetName();
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<IBullet>() != null)
            {
                AttackedBy(collision.gameObject.GetComponent<IBullet>());
            }
        }
    }
}