using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Runtime.Game.Mechanics.AttackInterfaces
{
    public interface IHpHolder
    {
        public void ChangeHp(float value, float pure);
        public void OnDeath();
    }
}