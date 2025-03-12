using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Client.Runtime.Game.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "RangedWeapon", menuName = "ScriptableObjects/Items/Equipable/Ranged")]
    public sealed class RangedWeaponScriptableObject : EquipableScriptableObject
    {
        public string ammoId;
        public float shootSpeed;
        public float ammoSpeed;
        public float ammoSize;
    }
}