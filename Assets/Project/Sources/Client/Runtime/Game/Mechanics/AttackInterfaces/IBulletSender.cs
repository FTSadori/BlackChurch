using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Runtime.Game.Mechanics.AttackInterfaces
{
    public interface IBulletSender
    {
        public void OnAttack(IHittable entity);
    }
}