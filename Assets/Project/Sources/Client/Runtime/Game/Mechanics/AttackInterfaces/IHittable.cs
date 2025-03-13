using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Runtime.Game.Mechanics.AttackInterfaces
{
    public interface IHittable
    {
        public void AttackedBy(IBullet bullet);
        public string GetName();
    }
}