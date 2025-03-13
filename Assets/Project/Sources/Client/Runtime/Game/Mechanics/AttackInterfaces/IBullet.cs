using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Client.Runtime.Game.Mechanics.AttackInterfaces
{
    public interface IBullet
    {
        public float GetDamage();
        public float GetPureDamage();
        public Vector2 GetForce();
        public void OnHit(IHittable hittable);
    }
}