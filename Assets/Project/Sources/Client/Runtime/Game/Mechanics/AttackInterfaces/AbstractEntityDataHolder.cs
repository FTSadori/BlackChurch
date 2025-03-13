using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Client.Runtime.Game.Mechanics.AttackInterfaces
{
    public abstract class AbstractEntityDataHolder : MonoBehaviour, IHpHolder
    {
        public abstract void ChangeHp(float value, float pure);
        public abstract void OnDeath();
        public abstract CharacterStats GetCharacterStats();
        public abstract string GetName();
    }
}