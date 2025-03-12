using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Mechanics;
using Sources.Client.Runtime.Game.Mechanics;
using UnityEngine;

namespace Client.Runtime.Game.ScriptableObjects
{
    public enum EquipableType
    {
        WEAPON,
        HEAD,
        BODY,
        LEGS,
        ACCESSORY,
    }

    [CreateAssetMenu(fileName = "Equipable", menuName = "ScriptableObjects/Items/Equipable/General")]
    public class EquipableScriptableObject : MaterialScriptableObject
    {
        public EquipableType equipableType;
        public CharacterStats additiveStats;
        public GodType godType;
    }
}