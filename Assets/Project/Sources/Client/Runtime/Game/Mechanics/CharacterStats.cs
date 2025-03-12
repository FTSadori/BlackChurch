using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sources.Client.Runtime.Game.Mechanics;
using UnityEngine;

namespace Client.Runtime.Game.Mechanics
{
    [Serializable]
    public sealed class CharacterStats
    {
        public int maxHp;
        public int baseAttack;
        public int baseDefence;
        public int pureAttack;
        public int pureDefence;
        public float regenerationRate;
        public float speedBuff;
        public float rangeBuff;
        public float attackSpeedBuff;
        public float ammoSpeedBuff;
        public float meleeBuff;
        public float rangedBuff;

        public Action OnStatsUpdate;


        public void InwokeUpdate()
        {
            OnStatsUpdate?.Invoke();   
        }

        public void Set(CharacterStats other)
        {
            maxHp = other.maxHp;
            baseAttack = other.baseAttack;
            baseDefence = other.baseDefence;
            pureAttack = other.pureAttack;
            pureDefence = other.pureDefence;
            speedBuff = other.speedBuff;
            meleeBuff = other.meleeBuff;
            rangedBuff = other.rangedBuff;
            regenerationRate = other.regenerationRate;
            rangeBuff = other.rangeBuff;
            attackSpeedBuff = other.attackSpeedBuff;
            ammoSpeedBuff = other.ammoSpeedBuff;

            OnStatsUpdate?.Invoke();
        }

        public void AddStats(CharacterStats other, GodBuffs godBuffs, GodType godType)
        {
            var buff = godBuffs.GetBuff(godType);

            maxHp += (int)godBuffs.GetPowerUppedValueFor(godType, other.maxHp);
            baseAttack += (int)godBuffs.GetPowerUppedValueFor(godType, other.baseAttack);
            baseDefence += (int)godBuffs.GetPowerUppedValueFor(godType, other.baseDefence);
            pureAttack += (int)godBuffs.GetPowerUppedValueFor(godType, other.pureAttack);
            pureDefence += (int)godBuffs.GetPowerUppedValueFor(godType, other.pureDefence);
            speedBuff += godBuffs.GetPowerUppedValueFor(godType, other.speedBuff);
            meleeBuff += godBuffs.GetPowerUppedValueFor(godType, other.meleeBuff);
            rangedBuff += godBuffs.GetPowerUppedValueFor(godType, other.rangedBuff);
            regenerationRate += godBuffs.GetPowerUppedValueFor(godType, other.regenerationRate);
            rangeBuff += other.rangeBuff; //godBuffs.GetPowerUppedValueFor(godType, other.rangeBuff);
            attackSpeedBuff += other.attackSpeedBuff; //godBuffs.GetPowerUppedValueFor(godType, other.attackSpeedBuff);
            ammoSpeedBuff += other.ammoSpeedBuff; //godBuffs.GetPowerUppedValueFor(godType, other.ammoSpeedBuff);

            OnStatsUpdate?.Invoke();
        }
    }
}