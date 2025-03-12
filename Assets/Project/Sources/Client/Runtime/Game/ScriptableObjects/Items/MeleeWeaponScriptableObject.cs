using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Client.Runtime.Game.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "MeleeWeapon", menuName = "ScriptableObjects/Items/Equipable/Melee")]
    public sealed class MeleeWeaponScriptableObject : EquipableScriptableObject
    {
        public float xRange;
        public float yRange;
        public float defaultAttackSpeed;
    }
}