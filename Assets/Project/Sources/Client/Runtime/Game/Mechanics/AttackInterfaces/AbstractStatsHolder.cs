using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Runtime.Game.Mechanics.AttackInterfaces
{
    public abstract class AbstractStatsHolder
    {
        public abstract CharacterStats GetCharacterStats();
    }
}