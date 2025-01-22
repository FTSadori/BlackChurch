using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Client.Runtime.Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Consumable", menuName = "ScriptableObjects/Items/Consumable")]
    public sealed class ConsumableScriptableObject : MaterialScriptableObject
    {
        public float useTime;
        public int hpBuff;
    }
}