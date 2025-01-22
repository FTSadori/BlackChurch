using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public int speedBuff;
        public int jumpBuff;
        public int attackSpeed;
    }
}