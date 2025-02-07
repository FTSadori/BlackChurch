using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Sources.Client.Runtime.Game.Mechanics
{
    public enum GodType
    {
        PAST,
        BIRTH,
        PATH,
        DEATH,
        LAND,
        SKY,
        TIME,
        MAX_TYPE,
    }

    [Serializable]
    public class GodBuffs
    {
        const float MIN_POWER_UP = 0.1f;

        [SerializeField] private float[] _buffs = new float[(int)GodType.MAX_TYPE];

        public Action OnUpdate;

        public void Set(GodBuffs other)
        {
            _buffs = other._buffs.ToArray();
            _buffs[(int)GodType.TIME] = 0f;
            _buffs[(int)GodType.TIME] = _buffs.Sum();
            OnUpdate?.Invoke();
        }

        public float GetPowerUppedValueFor(GodType itemType, float value)
        {
            if (itemType == GodType.MAX_TYPE) return value;
        
            if (value > 0f)
            {
                return value * GetPowerUpKoef(itemType);
            }
            return value / GetPowerUpKoef(itemType);
        }

        public float GetPowerUpKoef(GodType godType)
        {
            float buff = Mathf.Max(MIN_POWER_UP, 1f + _buffs[(int)godType]);
            return buff;
        }

        public float GetBuff(GodType godType)
        {
            return _buffs[(int)godType];
        }

        public void ChangeBuff(GodType godType, float amount)
        {
            if (godType == GodType.TIME) return;
            _buffs[(int)GodType.TIME] += amount;
            _buffs[(int)godType] += amount;
            OnUpdate?.Invoke();
        }

        public void SetBuff(GodType godType, float amount)
        {
            if (godType == GodType.TIME) return;
            _buffs[(int)GodType.TIME] += amount - _buffs[(int)godType];            
            _buffs[(int)godType] = amount;
            OnUpdate?.Invoke();
        }
    }
}