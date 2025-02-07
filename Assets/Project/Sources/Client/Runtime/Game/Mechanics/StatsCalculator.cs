using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.Player;
using Client.Runtime.Game.ScriptableObjects;
using Sources.Client.Runtime.Game.Mechanics;
using UnityEngine;

namespace Client.Runtime.Game.Mechanics
{
    public sealed class StatsCalculator
    {
        public static CharacterStats Recalculate(CharacterStats baseStats, InventoryData equipment, ItemListHandler itemListHandler, GodBuffs godBuffs) {
            CharacterStats characterStats = new();
            characterStats.Set(baseStats);

            WeaponType weaponType = WeaponType.MELEE;

            for (int i = 0; i < 7; ++i)
            {
                if (equipment.IsSlotEmpty(i))
                {
                    continue;
                }
                var obj = itemListHandler.GetObjectById(equipment.GetBySlotNumber(i).id);
                if (obj is EquipableScriptableObject eobj)
                {
                    if (i == 0)
                    {
                        weaponType = eobj.weaponType;
                    }
                    characterStats.AddStats(eobj.additiveStats, godBuffs, eobj.godType);
                }
            }

            characterStats.baseAttack = (int)(characterStats.baseAttack * 
                ((weaponType == WeaponType.MELEE)
                ? characterStats.meleeBuff
                : characterStats.rangedBuff));

            return characterStats;
        }
    }
}